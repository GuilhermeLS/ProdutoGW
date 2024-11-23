using Moq;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Application.Interfaces;
using FluentValidation;
using ProdutoGW.Domain.Validation;
using FluentValidation.Results;

namespace ProdutoGW.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<IValidator<Produto>> _validatorMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _validatorMock = new Mock<IValidator<Produto>>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task CreateAsync_InvalidProduto_ShouldThrowValidationException()
        {
            // Arrange
            var produto = new Produto(); // Produto inválido
            _validatorMock
                .Setup(v => v.ValidateAsync(produto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Nome", "Nome é obrigatório.") }));

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _produtoService.CreateAsync(produto));
        }

        [Fact]
        public async Task CreateAsync_ExistingProduto_ShouldThrowException()
        {
            // Arrange
            var produto = new Produto { Guid = Guid.NewGuid(), Nome = "Produto Teste" };
            _validatorMock
                .Setup(v => v.ValidateAsync(produto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(produto.Guid))
                .ReturnsAsync(produto);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _produtoService.CreateAsync(produto));
        }

        [Fact]
        public async Task CreateAsync_ValidProduto_ShouldReturnCreatedProduto()
        {
            // Arrange
            var produto = new Produto { Guid = Guid.NewGuid(), Nome = "Produto Teste" };
            _validatorMock
                .Setup(v => v.ValidateAsync(produto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(produto.Guid))
                .ReturnsAsync((Produto)null);
            _produtoRepositoryMock
                .Setup(r => r.CreateAsync(produto))
                .ReturnsAsync(produto);

            // Act
            var result = await _produtoService.CreateAsync(produto);

            // Assert
            Assert.Equal(produto, result);
            _produtoRepositoryMock.Verify(r => r.CreateAsync(produto), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProdutosList()
        {
            // Arrange
            var produtos = new List<Produto>
    {
        new Produto { Guid = Guid.NewGuid(), Nome = "Produto 1" },
        new Produto { Guid = Guid.NewGuid(), Nome = "Produto 2" }
    };

            _produtoRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByGuidAsync_ExistingGuid_ShouldReturnProduto()
        {
            // Arrange
            var produto = new Produto { Guid = Guid.NewGuid(), Nome = "Produto Teste" };
            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(produto.Guid))
                .ReturnsAsync(produto);

            // Act
            var result = await _produtoService.GetByGuidAsync(produto.Guid);

            // Assert
            Assert.Equal(produto, result);
        }

        [Fact]
        public async Task GetByGuidAsync_NonExistingGuid_ShouldReturnNull()
        {
            // Arrange
            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.GetByGuidAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ExistingProduto_ShouldUpdateProduto()
        {
            // Arrange
            var produto = new Produto { Guid = Guid.NewGuid(), Nome = "Produto Atualizado" };
            var existingProduto = new Produto { Guid = produto.Guid, Nome = "Produto Antigo" };

            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(produto.Guid))
                .ReturnsAsync(existingProduto);
            _produtoRepositoryMock
                .Setup(r => r.UpdateAsync(existingProduto))
                .ReturnsAsync(existingProduto);

            // Act
            var result = await _produtoService.UpdateAsync(produto);

            // Assert
            Assert.Equal(produto.Nome, result.Nome);
            _produtoRepositoryMock.Verify(r => r.UpdateAsync(existingProduto), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingGuid_ShouldReturnTrue()
        {
            // Arrange
            var produto = new Produto { Guid = Guid.NewGuid() };

            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(produto.Guid))
                .ReturnsAsync(produto);

            // Act
            var result = await _produtoService.DeleteAsync(produto.Guid);

            // Assert
            Assert.True(result);
            _produtoRepositoryMock.Verify(r => r.DeleteAsync(produto), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingGuid_ShouldReturnFalse()
        {
            // Arrange
            _produtoRepositoryMock
                .Setup(r => r.GetByGuidAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
}
