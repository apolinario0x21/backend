using FluentValidation;
using ProductStore.Models;

namespace ProductManagement.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(5, 32).WithMessage("O nome do produto deve ter entre 5 e 32 caracteres.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("O preço deve ser um valor positivo.");

            RuleFor(product => product.Category)
                .NotEmpty().Length(3, 20).WithMessage("A categoria deve ter entre 3 e 20 caracteres.");

            RuleFor(product => product.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");
        }
    }
}