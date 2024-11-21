using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}

