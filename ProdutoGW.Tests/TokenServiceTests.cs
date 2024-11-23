using Microsoft.Extensions.Configuration;
using Moq;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly Mock<IConfiguration> _configurationMock;

    public TokenServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(c => c["Jwt:SecretKey"]).Returns("{3C2AEBAE-8D97-45E7-8A83-1D489E85811B}");

        _tokenService = new TokenService(_configurationMock.Object);
    }

    [Fact]
    public void GerarToken_ValidUsuario_ShouldReturnToken()
    {
        // Arrange
        var usuario = new Usuario
        {
            Guid = Guid.NewGuid(),
            Nome = "Teste",
            Role = "Admin"
        };

        // Act
        var token = _tokenService.GerarToken(usuario);

        // Assert
        Assert.False(string.IsNullOrEmpty(token));
    }
}
