using Microsoft.EntityFrameworkCore;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Infrastructure.Data;

namespace ProdutoGW.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _context;

        public ProdutoRepository(ProdutoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByGuidAsync(Guid guid)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Guid == guid);
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task DeleteAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }


    }
}
