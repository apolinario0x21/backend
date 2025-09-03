using FluentValidation;
using ProductStore.Models;

namespace ProductManagement.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(3, 20).WithMessage("Nome deve ter entre 3 e 20 caracteres.");

        }
    }
}
