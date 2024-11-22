using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProdutoGW.Domain.Entities;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class UsuarioControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

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
    public async Task DeleteUsuario_ShouldReturnNoContent()
    {
        // Arrange
        var guid = Guid.NewGuid(); // Use um guid válido se já houver usuários no banco.

        // Act
        var response = await _client.DeleteAsync($"/api/usuario/{guid}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}