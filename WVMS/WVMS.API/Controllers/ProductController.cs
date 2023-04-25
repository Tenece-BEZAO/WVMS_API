using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
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
        [AllowAnonymous]
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
        [Authorize(Roles = "Vendor, SuperAdmin, Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = await _productServices.CreateProductAsync(product);
                if (newProduct != null)
                {
                    return Created("Get", newProduct);
                }
                return BadRequest();
            }
            return BadRequest();
        }




        /// <summary>
        /// Gets all vendors product
        /// </summary>
        [HttpGet("get-vendor-products")]
        [Authorize(Roles = "Vendor")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ProductResponse>>> GetVendorProduct()
        {
            var product = await _productServices.GetProductAsync();

            return Ok(product);
        }



        /// <summary>
        /// Deletes a product by Id
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Vendor, SuperAdmin, Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> DeleteProduct(Guid Id)
        {
            await _productServices.DeleteProductAsync(Id);
            return Ok();
        }




        /// <summary>
        /// Updates a product
        /// </summary>
        /// <returns>The updated product</returns>
        [HttpPut]
        [Authorize(Roles = "Vendor, SuperAdmin, Admin")]
        public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductRequest productRequest)
        {

            var respose = await _productServices.UpdateProductAsync(productId, productRequest);
            if (respose == null)
                return BadRequest("something went wrong");

            return Ok(respose);
        }




        /// <summary>
        /// Search for a product
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("search-products")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<ProductSearchResponseDto>>> SearchProducts([FromQuery] SearchRequestDto searchParam)
        {
            var response = await _productServices.SearchProductAsync(searchParam);

            return Ok(response);
        }



        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("get-product-by-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var response = await _productServices.GetProductById(productId);
            return Ok(response);
        }


        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost("add-to-cart")]
        [Authorize(Roles = "Vendor, Admin, SuperAdmin")]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var response = await _productServices.AddToCartAsync(productId, quantity);
            return Ok(response);
        }

    }
}