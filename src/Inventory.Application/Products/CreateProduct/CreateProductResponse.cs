using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.CreateProduct
{
    public class CreateProductResponse : BaseResponse
    {
        public int Id { get; set; }

        public CreateProductResponse(int id)
        {
            Id = id;
        }
    }
}