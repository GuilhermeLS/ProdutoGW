using Moq;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Application.Interfaces;
using FluentValidation;
using ProdutoGW.Domain.Validation;

namespace ProdutoGW.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly IValidator<Produto> _produtoValidator;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoValidator = new ProdutoValidator();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _produtoValidator);
        }

        [Fact]
        public async Task CreateProduto_ShouldReturnCreatedProduto()
        {
            var produto = new Produto
            {
                Nome = "Produto 1",
                Descricao = "Descrição do Produto 1",
                Categoria = "Categoria 1",
                Marca = "Marca 1",
                Preco = 10.0m,
                QuantidadeEmEstoque = 10
            };

            _produtoRepositoryMock.Setup(repo => repo.CreateAsync(produto)).ReturnsAsync(produto);

            var result = await _produtoService.CreateAsync(produto);

            Assert.NotNull(result);
            Assert.Equal(produto.Nome, result.Nome);
        }
    }
}
