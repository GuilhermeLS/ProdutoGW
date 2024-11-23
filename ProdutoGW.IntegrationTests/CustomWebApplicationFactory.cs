using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Infrastructure.Data;

[CollectionDefinition("Non-Parallel Tests", DisableParallelization = true)]
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Substituir o contexto para usar banco em memória
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ProdutoContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ProdutoContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "FakeScheme";
                options.DefaultChallengeScheme = "FakeScheme";
            }).AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("FakeScheme", _ => { });


            // Inicializar o banco
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProdutoContext>();

                db.Database.EnsureCreated();

                if (!db.Produtos.Any())
                {
                    db.Produtos.Add(new Produto
                    {
                        Guid = new Guid("{EA9E2A4A-216D-49BC-B6FD-3527F444AB44}"),
                        Nome = "Produto Teste",
                        Descricao = "Descrição Teste",
                        Categoria = "Categoria 1",
                        Marca = "Marca 1",
                        Preco = 100,
                        QuantidadeEmEstoque = 10
                    });

                    db.SaveChanges();
                }
            }



        });
        builder.UseEnvironment("Development");
    }
}