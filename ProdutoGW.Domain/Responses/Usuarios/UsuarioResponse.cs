using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoGW.Domain.Responses.Usuarios
{
    public class UsuarioResponse
    {
        public Guid UsuarioGuid { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
