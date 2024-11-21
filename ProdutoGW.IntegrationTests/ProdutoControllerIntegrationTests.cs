using Microsoft.AspNetCore.Mvc.Testing;
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
        // Arrange
        var requestUri = "/api/produto";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content); // Verifica se o conteúdo retornado não está vazio
    }

    [Fact]
    public async Task PostProduto_CreatesNewProduto()
    {
        // Arrange
        var novoProduto = new
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 19.99
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/produto", novoProduto);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduto_RemovesProduto()
    {
        // Arrange
        int id = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/produto/{id}");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
}