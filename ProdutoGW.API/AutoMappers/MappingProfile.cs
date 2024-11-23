using AutoMapper;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests.Produtos;
using ProdutoGW.Domain.Requests.Usuarios;
using ProdutoGW.Domain.Responses.Produtos;
using ProdutoGW.Domain.Responses.Usuarios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProdutoGW.API.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Produto mappings
            CreateMap<Produto, ProdutoResponse>();
            CreateMap<ProdutoCreateRequest, Produto>();
            CreateMap<ProdutoUpdateRequest, Produto>();

            // Usuário mappings
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<UsuarioCreateRequest, Usuario>();
        }
    }
}
