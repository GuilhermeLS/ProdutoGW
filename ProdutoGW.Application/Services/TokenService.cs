using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProdutoGW.Application.Interfaces;
using ProdutoGW.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProdutoGW.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                { 
                    new Claim("usuarioId", usuario.Guid.ToString()),
                    new Claim("usuarioNome", usuario.Nome),
                    new Claim("usuarioRole", usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }
    }
}
