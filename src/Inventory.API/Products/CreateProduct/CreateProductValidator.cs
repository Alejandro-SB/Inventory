using FluentValidation;

namespace Inventory.API.Products.CreateProduct
{
    /// <summary>
    /// The validator of the model to create a product
    /// </summary>
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        /// <summary>
        /// Creates an instance of the validator
        /// </summary>
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty");
        }
    }
}