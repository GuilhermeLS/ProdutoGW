using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProdutoGW.Infrastructure.Repositories;
using ProdutoGW.Infrastructure.Data;
using FluentValidation.AspNetCore;
using ProdutoGW.Domain.Validation;
using FluentValidation;
using ProdutoGW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}