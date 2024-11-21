using Microsoft.IdentityModel.Tokens;
using ProdutoGW.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProdutoGW.Security
{
    public static class JwtTokenHelper
    {
        public static string GenerateToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secreto-chave-jwt"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

