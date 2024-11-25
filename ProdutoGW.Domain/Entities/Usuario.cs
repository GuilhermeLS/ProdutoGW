namespace ProdutoGW.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Role { get; set; } 
    }
}

