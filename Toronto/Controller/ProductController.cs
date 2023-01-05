using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toronto.Models;
using Toronto.Service;

namespace Toronto.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(ProductService productService)
        {
            this.ProductService = productService;
        }

        ProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        [HttpGet]
        [Route("Tax")]
        public ActionResult Update([FromQueryAttribute] int Id,
                                   [FromQueryAttribute] int Tax)
        {
            ProductService.UpdateTax(Id, Tax);
            return Ok();
        }
    }
}
