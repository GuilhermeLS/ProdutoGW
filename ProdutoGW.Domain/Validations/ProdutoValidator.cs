using FluentValidation;
using ProdutoGW.Domain.Entities;
namespace ProdutoGW.Domain.Validation
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(p => p.Descricao).NotEmpty().WithMessage("Descrição é obrigatória.");
            RuleFor(p => p.Categoria).NotEmpty().WithMessage("Categoria é obrigatória.");
            RuleFor(p => p.Marca).NotEmpty().WithMessage("Marca é obrigatória.");
            RuleFor(p => p.Preco).GreaterThan(0).WithMessage("Preço deve ser maior que 0.");
            RuleFor(p => p.QuantidadeEmEstoque).GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque não pode ser negativa.");
        }
    }
}

