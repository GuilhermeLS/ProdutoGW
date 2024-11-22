using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProdutoGW.Domain.Entities;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class ProdutoControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProdutoControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetProdutos_ReturnsOkResponse()
    {
        var loginRequest = new
        {
            Email = "admin@dominio.com",
            Senha = "admin"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Arrange
        var requestUri = "/api/produto";

        // Act
        var response2 = await _client.GetAsync(requestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content); // Verifica se o conteúdo retornado não está vazio
    }

    [Fact]
    public async Task CreateProduto_ShouldReturnCreatedProduto()
    {
        // Arrange
        var loginRequest = new
        {
            Email = "admin@dominio.com",
            Senha = "admin"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Arrange
        var produtoRequest = new
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Categoria = "Categoria Teste",
            Marca = "Marca Teste",
            Preco = 100.00m,
            QuantidadeEmEstoque = 1
        };

        // Act
        var response2 = await _client.PostAsJsonAsync("/api/produto", produtoRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var produtoResponse = await response.Content.ReadFromJsonAsync<Produto>();
        produtoResponse.Should().NotBeNull();
        produtoResponse!.Nome.Should().Be(produtoRequest.Nome);
    }

    [Fact]
    public async Task GetProdutos_ShouldReturnListOfProdutos()
    {

        // Arrange
        var loginRequest = new
        {
            Email = "admin@dominio.com",
            Senha = "admin"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Act
        var response2 = await _client.GetAsync("/api/produto");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();
        produtos.Should().NotBeNull();
    }
}