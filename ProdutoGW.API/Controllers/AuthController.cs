using Microsoft.AspNetCore.Mvc;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Requests.Login;

namespace ProdutoGW.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public AuthController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioService.AutenticarAsync(request.Email, request.Senha);
            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _tokenService.GerarToken(usuario);
            return Ok(new { token });
        }

    }
}
