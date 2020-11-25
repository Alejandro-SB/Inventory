using Inventory.Web.Api;
using Inventory.Web.Models;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inventory.Web.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly ApiService _apiService;

        public ProductController()
        {
            _apiService = new ApiService(ConfigurationManager.AppSettings["WebApiUrl"]);
        }

        public async Task<ActionResult> Index()
        {
            string token = GetToken();

            var products = await _apiService.GetAllProducts(token);

            return View(products.Select(x => new ProductViewModel
            {
                Name = x.Name,
                ExpirationDate = x.ExpirationDate,
                ProductType = x.ProductType
            }));
        }

        [HttpGet]
        public ActionResult New()
        {
            var model = new NewProductModel();

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(NewProductModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.Name.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                ModelState.AddModelError("Name", "Name can only contain letters, numbers and whitespace");
                return View(model);
            }

            var token = GetToken();
            var isSaved = await _apiService.CreateProductAsync(token, model.Name, model.ExpirationDate, model.ProductType);

            if(isSaved)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Product cannot be saved");

                return View(model);
            }
        }

        [HttpPost, ValidateInput(false)]
        public async Task<RedirectToRouteResult> Delete(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                await _apiService.DeleteProductAsync(GetToken(), productName);
            }

            return RedirectToAction("Index");
        }
    }
}