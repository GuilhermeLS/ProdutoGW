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

            var existingProduct = await _produtoRepository.GetByIdAsync(produto.Id);
            if (existingProduct != null)
                throw new Exception("Produto já existe.");

            return await _produtoRepository.CreateAsync(produto);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            var existingProduto = await _produtoRepository.GetByIdAsync(produto.Id);
            if (existingProduto == null)
            {
                return null; // Ou lançar uma exceção, dependendo do seu caso
            }

            // Atualiza os dados do produto
            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.Descricao = produto.Descricao;

            // Chama o repositório para salvar a atualização
            var updatedProduto = await _produtoRepository.UpdateAsync(existingProduto);
            return updatedProduto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
            {
                return false;
            }

            await _produtoRepository.DeleteAsync(id);
            return true;
        }
    }
}

