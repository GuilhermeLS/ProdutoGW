using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests;
using ProdutoGW.Domain.Requests.Usuarios;
using ProdutoGW.Domain.Responses.Produtos;
using ProdutoGW.Domain.Responses.Usuarios;

namespace ProdutoGW.API.Controllers
{
    [Authorize]
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
            var usuario = _mapper.Map<Usuario>(request);
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            usuario.Role = "User"; 

            var createdUsuario = await _usuarioService.CreateAsync(usuario);

            var response = _mapper.Map<UsuarioResponse>(createdUsuario);
            return CreatedAtAction(nameof(Create), new { response.Guid }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            if (usuarios == null || !usuarios.Any())
                return Ok(new List<Usuario>());

            var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);

            return Ok(response);
        }

        [HttpGet("{usuarioGuid}")]
        public async Task<IActionResult> GetByGuid(Guid usuarioGuid)
        {
            var usuario = await _usuarioService.GetByGuidAsync(usuarioGuid);
            if (usuario == null)
                return NotFound();

            var response = _mapper.Map<UsuarioResponse>(usuario);

            return Ok(response);
        }
    }
}
