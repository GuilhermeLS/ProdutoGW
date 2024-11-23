using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnJwtToken()
    {
        // Arrange
        var loginRequest = new { Email = "admin@dominio.com", Senha = "admin" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var loginResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        loginResponse.Should().ContainKey("token");
        loginResponse!["token"].Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
    {
        // Arrange
        var loginRequest = new { Email = "invalido@dominio.com", Senha = "senhaerrada" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}