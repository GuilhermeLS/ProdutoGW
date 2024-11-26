using FluentAssertions;
using ProdutoGW.Domain.Entities;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class ProdutoControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;
    private const string VALID_GUID = "{EA9E2A4A-216D-49BC-B6FD-3527F444AB44}";
    private const string INVALID_GUID = "{DE9E1D24-216D-49BC-B6FD-3527F444d4AA}";


    public ProdutoControllerTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduto_WithValidData_ShouldReturnCreatedProduto()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var produtoRequest = new
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto",
            Categoria = "Categoria Teste",
            Marca = "Marca Teste",
            Preco = 100,
            QuantidadeEmEstoque = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/produto", produtoRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateProduto_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var produtoRequest = new { Descricao = "Faltando campos obrigatórios" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/produto", produtoRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAllProdutos_ShouldReturnProdutosList()
    {
        // Act
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var response = await _client.GetAsync("/api/produto");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();  
        produtos.Should().NotBeNull();  
    }

    [Fact]
    public async Task GetProduto_WithValidGuid_ShouldReturnProduto()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.GetAsync($"/api/produto/{VALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var produto = await response.Content.ReadFromJsonAsync<Produto>();
        produto.Guid.Should().Be(VALID_GUID);
    }

    [Fact]
    public async Task GetProduto_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.GetAsync($"/api/produto/{INVALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateProduto_WithValidData_ShouldReturnUpdatedProduto()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var produtoRequest = new
        {
            Guid = new Guid(VALID_GUID),
            Nome = "Produto Teste Alterado",
            Descricao = "Descrição Teste Alterado",
            Categoria = "Categoria 1 Alterado",
            Marca = "Marca 1 Alterado",
            Preco = 200,
            QuantidadeEmEstoque = 20
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/produto", produtoRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateProduto_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var produtoRequest = new
        {
            Guid = new Guid(INVALID_GUID),
            Nome = "Produto Teste Não Alterado",
            Descricao = "Descrição Teste Não Alterado",
            Categoria = "Categoria 1 Não Alterado",
            Marca = "Marca 1 Não Alterado",
            Preco = 300,
            QuantidadeEmEstoque = 30
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/produto", produtoRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteProduto_WithValidGuid_ShouldReturnNoContent()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.DeleteAsync($"/api/produto/{VALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteProduto_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.DeleteAsync($"/api/produto/{INVALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}