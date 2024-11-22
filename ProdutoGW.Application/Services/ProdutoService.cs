using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Entities;
using FluentValidation;

namespace ProdutoGW.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IValidator<Produto> _validator;

        public ProdutoService(IProdutoRepository produtoRepository, IValidator<Produto> validator)
        {
            _produtoRepository = produtoRepository;
            _validator = validator;
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            var validationResult = await _validator.ValidateAsync(produto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await _produtoRepository.GetByGuidAsync(produto.Guid);
            if (existingProduct != null)
                throw new Exception("Produto já existe.");

            return await _produtoRepository.CreateAsync(produto);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetByGuidAsync(Guid produtoGuid)
        {
            var produto = await _produtoRepository.GetByGuidAsync(produtoGuid);
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            var existingProduto = await _produtoRepository.GetByGuidAsync(produto.Guid);
            if (existingProduto == null)
            {
                return null; 
            }

            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.Descricao = produto.Descricao;
            existingProduto.Categoria = produto.Categoria;
            existingProduto.Marca = produto.Marca;
            existingProduto.QuantidadeEmEstoque = produto.QuantidadeEmEstoque;

            var updatedProduto = await _produtoRepository.UpdateAsync(existingProduto);
            return updatedProduto;
        }

        public async Task<bool> DeleteAsync(Guid produtoGuid)
        {
            var produto = await _produtoRepository.GetByGuidAsync(produtoGuid);
            if (produto == null)
            {
                return false;
            }

            await _produtoRepository.DeleteAsync(produto);
            return true;
        }
    }
}

