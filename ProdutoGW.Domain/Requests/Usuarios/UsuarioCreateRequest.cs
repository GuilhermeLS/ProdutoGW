namespace ProdutoGW.Domain.Requests.Usuarios
{
    public class UsuarioCreateRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
