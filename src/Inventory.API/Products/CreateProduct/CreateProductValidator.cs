using FluentValidation;

namespace Inventory.API.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty");
        }
    }
}