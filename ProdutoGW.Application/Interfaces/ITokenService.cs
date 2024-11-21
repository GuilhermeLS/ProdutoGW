using ProdutoGW.Domain.Entities;

namespace ProdutoGW.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}

