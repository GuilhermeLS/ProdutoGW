using Moq;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Infrastructure.Interfaces;

namespace ProdutoGW.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task AutenticarAsync_ValidCredentials_ShouldReturnUsuario()
        {
            // Arrange
            var senha = "senha123";
            var usuario = new Usuario { Email = "teste@teste.com", SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha) };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailAsync(usuario.Email))
                .ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.AutenticarAsync(usuario.Email, senha);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.Email, result.Email);
        }

        [Fact]
        public async Task AutenticarAsync_InvalidPassword_ShouldReturnNull()
        {
            // Arrange
            var usuario = new Usuario { Email = "teste@teste.com", SenhaHash = BCrypt.Net.BCrypt.HashPassword("senha123") };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailAsync(usuario.Email))
                .ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.AutenticarAsync(usuario.Email, "senhaErrada");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ExistingEmail_ShouldThrowException()
        {
            // Arrange
            var usuario = new Usuario { Email = "teste@teste.com" };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailAsync(usuario.Email))
                .ReturnsAsync(usuario);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _usuarioService.CreateAsync(usuario));
        }

        [Fact]
        public async Task CreateAsync_ValidUsuario_ShouldCreateUsuario()
        {
            // Arrange
            var usuario = new Usuario { Email = "novo@teste.com" };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailAsync(usuario.Email))
                .ReturnsAsync((Usuario)null);
            _usuarioRepositoryMock
                .Setup(r => r.CreateAsync(usuario))
                .ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.CreateAsync(usuario);

            // Assert
            Assert.Equal(usuario, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProdutosList()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Guid = Guid.NewGuid(), Nome = "Usuario 1" },
                new Usuario { Guid = Guid.NewGuid(), Nome = "Usuario 2" }
            };

            _usuarioRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(usuarios);

            // Act
            var result = await _usuarioService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByGuidAsync_ExistingGuid_ShouldReturnUsuario()
        {
            // Arrange
            var usuario = new Usuario { Guid = Guid.NewGuid(), Nome = "Usuario Teste" };
            _usuarioRepositoryMock
                .Setup(r => r.GetByGuidAsync(usuario.Guid))
                .ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetByGuidAsync(usuario.Guid);

            // Assert
            Assert.Equal(usuario, result);
        }

        [Fact]
        public async Task GetByGuidAsync_NonExistingGuid_ShouldReturnNull()
        {
            // Arrange
            _usuarioRepositoryMock
                .Setup(r => r.GetByGuidAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Usuario)null);

            // Act
            var result = await _usuarioService.GetByGuidAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
