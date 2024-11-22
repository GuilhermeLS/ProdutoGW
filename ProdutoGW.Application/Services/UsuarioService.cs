using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AutenticarAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                return null;

            return usuario;
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);
            if (existingUser != null)
                throw new Exception("Usuário já existe.");

            return await _usuarioRepository.CreateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(Guid usuarioGuid)
        {
            var usuario = await _usuarioRepository.GetByGuidAsync(usuarioGuid);
            if (usuario == null)
            {
                return false;
            }

            await _usuarioRepository.DeleteAsync(usuario);
            return true;
        }
    }
}

