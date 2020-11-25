using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetByName
{
    public class GetProductByNameRequest : IRequest<GetProductByNameResponse>
    {
        public string Name { get; set; }

        public GetProductByNameRequest(string name)
        {
            Name = name;
        }
    }
}