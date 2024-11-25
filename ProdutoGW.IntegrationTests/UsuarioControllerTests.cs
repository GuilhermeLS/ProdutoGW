using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProdutoGW.Domain.Entities;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class UsuarioControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private const string VALID_GUID = "{8F8F2756-3C1D-4D87-BCC2-782CE38EEFC1}";
    private const string INVALID_GUID = "{C6D29E83-FF4D-4C3F-BAF0-773201BD23CF}";

    public UsuarioControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task CreateUsuario_ShouldReturnCreatedUsuario()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        var usuarioRequest = new
        {
            Nome = "Usuário Teste",
            Email = "teste@dominio.com",
            Senha = "Senha123",
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/usuario", usuarioRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var usuarioResponse = await response.Content.ReadFromJsonAsync<Usuario>();
        usuarioResponse.Should().NotBeNull();
        usuarioResponse!.Nome.Should().Be(usuarioRequest.Nome);
    }

    [Fact]
    public async Task GetAllUsuarios_ShouldReturnUsuariosList()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.GetAsync("/api/usuario");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();
        usuarios.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUsuario_WithValidGuid_ShouldReturnUsuario()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.GetAsync($"/api/usuario/{VALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
        usuario.Guid.Should().Be(VALID_GUID);
    }

    [Fact]
    public async Task GetUsuario_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeScheme");

        // Act
        var response = await _client.GetAsync($"/api/usuario/{INVALID_GUID}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}
