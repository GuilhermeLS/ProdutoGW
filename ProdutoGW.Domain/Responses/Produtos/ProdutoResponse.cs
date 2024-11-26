namespace ProdutoGW.Domain.Responses.Produtos
{
    public class ProdutoResponse
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
    }
}
