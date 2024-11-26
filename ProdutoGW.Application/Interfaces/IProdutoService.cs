using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByGuidAsync(Guid produtoGuid);
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> DeleteAsync(Guid produtoGuid);
    }
}

