using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetByGuidAsync(Guid guid);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task DeleteAsync(Produto produto);
    }
}

