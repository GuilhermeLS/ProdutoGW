﻿namespace ProdutoGW.Domain.Requests.Produtos
{
    public class ProdutoUpdateRequest
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