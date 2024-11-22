using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            // Inicializar o banco
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProdutoContext>();

                db.Database.EnsureCreated();
            }
        });
    }
    //protected override void ConfigureWebHost(IWebHostBuilder builder)
    //{
    //    builder.UseEnvironment("Development");

    //    builder.ConfigureServices(services =>
    //    {
    //        // Remover a configuração do banco de dados em memória
    //        var serviceDescriptor = services.SingleOrDefault(
    //            d => d.ServiceType == typeof(DbContextOptions<ProdutoContext>));
    //        if (serviceDescriptor != null)
    //        {
    //            services.Remove(serviceDescriptor);
    //        }

    //        // Acessar as configurações do appsettings.json
    //        var configuration = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory()) // Define o diretório base
    //            .AddJsonFile("appsettings.json") // Adiciona o arquivo appsettings.json
    //            .Build();

    //        // Obter a string de conexão diretamente do arquivo de configurações
    //        var connectionString = configuration.GetConnectionString("DefaultConnection"); // Nome da chave da string de conexão

    //        // Configurar o banco de dados real
    //        services.AddDbContext<ProdutoContext>(options =>
    //            options.UseSqlServer(connectionString));

    //        // Criação do banco real ao iniciar (se necessário)
    //        var sp = services.BuildServiceProvider();
    //        using var scope = sp.CreateScope();
    //        var db = scope.ServiceProvider.GetRequiredService<ProdutoContext>();
    //        db.Database.Migrate(); // Aplica migrações automaticamente
    //    });
    //}
}