using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Application.Services;
using ProdutoGW.Domain.Entities;
using ProdutoGW.Domain.Requests;

namespace ProdutoGW.API.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterRequest request)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                Role = "User" 
            };

            var result = await _usuarioService.CreateAsync(usuario);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
