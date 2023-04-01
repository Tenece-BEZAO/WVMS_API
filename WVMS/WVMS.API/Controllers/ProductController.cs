using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServices;
        private readonly IMapper _mapper;


        public ProductController(IProductService productService, IMapper mapper )
        {
            _productServices = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Product>))]
        public IActionResult GetAllProducts()
        {
            var allProducts = _productServices.GetAllProducts();
            return Ok(allProducts);
        }

        [HttpPost]
        public IActionResult CreateProduct (ProductDto product)
        {
            var newProduct = _productServices.CreateProduct(product);

            return Created("Get", product.ProductId);
        }

        /*[HttpPost]
        public IActionResult CreateProduct(ProductDto product)
        {
          var response =  _productRepository.CreateProduct(product);

            if (response.Result == "Success")
            {
                return Created("Get", product.ProductId);
                 
            }
            return Ok("Could not create product!");
        }*/

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int Id)
        {
            if (!_productServices.ProductExist(Id))
                return NotFound();

            var product = await _productServices.GetProduct(Id);

            return Ok(product);
        }
       


        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int Id)
        {
            _productServices.DeleteProduct(Id);
            return Ok();
        }
    }
}
