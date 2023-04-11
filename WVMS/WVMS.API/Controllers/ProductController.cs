using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;


        public ProductController(IProductServices productService, IMapper mapper)
        {
            _productServices = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets lists of all the products
        /// </summary>
        /// <returns>The product list</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Product>))]
        public IActionResult GetAllProducts()
        {
            var allProducts = _productServices.GetAllProducts();
            return Ok(allProducts);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = await _productServices.CreateProduct(product);
                if (newProduct != null)
                {
                    return Created("Get", newProduct);
                }
                return BadRequest();
            }
            return BadRequest();
        }


        /// <summary>
        /// Gets a product by Id
        /// </summary>
        [HttpGet]
        [Route("product/{Id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> GetProduct(Guid userId)
        {
            /* if (!_productServices.ProductExist(Id))
                 return NotFound();*/

            var product = _productServices.GetProduct(userId);

            return Ok(product);
        }


        /// <summary>
        /// Deletes a product by Id
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> DeleteProduct(Guid Id)
        {
            await _productServices.DeleteProduct(Id);
            return Ok();
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <returns>The updated product</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {

            var respose = await _productServices.UpdateProduct(request);
            if (respose == null)
                return BadRequest("something went wrong");

            return Ok(respose);
        }


    }
}