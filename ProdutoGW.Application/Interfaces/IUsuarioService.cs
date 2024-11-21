using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AutenticarAsync(string email, string senha);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
    }
}

