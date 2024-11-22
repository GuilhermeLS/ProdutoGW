using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests;
using ProdutoGW.Domain.Requests.Usuarios;
using ProdutoGW.Domain.Responses.Usuarios;

namespace ProdutoGW.API.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateRequest request)
        {
            // Usa o AutoMapper para mapear o request para a entidade de domínio
            var usuario = _mapper.Map<Usuario>(request);
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha); // Gera o hash da senha
            usuario.Role = "User"; // Define a role padrão para novos usuários

            var createdUsuario = await _usuarioService.CreateAsync(usuario);

            // Mapeia a entidade criada para a response
            var response = _mapper.Map<UsuarioResponse>(createdUsuario);
            return CreatedAtAction(nameof(Create), new { response.UsuarioGuid }, response);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid usuarioGuid)
        {
            await _usuarioService.DeleteAsync(usuarioGuid);
            return NoContent();
        }
    }
}
