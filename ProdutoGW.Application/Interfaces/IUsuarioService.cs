using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AutenticarAsync(string email, string senha);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByGuidAsync(Guid produtoGuid);
    }
}

