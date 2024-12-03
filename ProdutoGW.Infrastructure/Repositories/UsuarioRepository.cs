using Microsoft.EntityFrameworkCore;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Infrastructure.Data;
using ProdutoGW.Infrastructure.Interfaces;

namespace ProdutoGW.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProdutoContext _context;

        public UsuarioRepository(ProdutoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByGuidAsync(Guid guid)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Guid == guid);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }


        public async Task DeleteAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}

