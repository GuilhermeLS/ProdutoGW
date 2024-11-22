using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoGW.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using global::ProdutoGW.Domain.Entities;
    using global::ProdutoGW.Infrastructure.Data;
    using global::ProdutoGW.Application.Interfaces;

    namespace ProdutoGW.Infrastructure.Repositories
    {
        public class UsuarioRepository : IUsuarioRepository
        {
            private readonly ProdutoContext _context;

            public UsuarioRepository(ProdutoContext context)
            {
                _context = context;
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
}
