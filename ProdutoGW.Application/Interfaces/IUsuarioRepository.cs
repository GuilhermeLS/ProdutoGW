using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}

