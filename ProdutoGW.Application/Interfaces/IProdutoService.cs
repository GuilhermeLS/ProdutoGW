using ProdutoGW.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdutoGW.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> DeleteAsync(int id);
    }
}

