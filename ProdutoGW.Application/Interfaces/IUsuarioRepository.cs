﻿using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByGuidAsync(Guid usuarioGuid);
        Task<Usuario> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario usuario);
        
    }
}

