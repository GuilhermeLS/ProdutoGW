using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProdutoGW.API.AutoMappers;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Validation;
using ProdutoGW.Infrastructure.Data;
using ProdutoGW.Infrastructure.Repositories;
using ProdutoGW.Infrastructure.Repositories.ProdutoGW.Infrastructure.Repositories;
using System.Text;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configurações do FluentValidation
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        services.AddTransient<IValidator<Produto>, ProdutoValidator>();

        // Repositórios e serviços
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ITokenService, TokenService>();

        // Configuração de banco de dados
        services.AddDbContext<ProdutoContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        Console.WriteLine($"Current Directory: {AppDomain.CurrentDomain.BaseDirectory}");

        // Configuração de JWT
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters 
            { 
                ValidateIssuerSigningKey = true, 
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddControllers();
        services.AddAuthorization();

        services.AddAutoMapper(typeof(MappingProfile));

        // Configuração de Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Teste Globalweb - API de Produtos",
                    Version = "v1",
                    Description = "Documentação da API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Guilherme Lima Silva",
                        Email = "guilherme.spw@hotmail.com"
                    }
                });

            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Produtos v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Configuração de endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}