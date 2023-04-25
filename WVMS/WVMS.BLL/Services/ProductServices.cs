using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.DAL.Interfaces;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.Services
{
    public class ProductServices : IProductServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductServices(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUsers> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _cartRepo = _unitOfWork.GetRepository<Cart>();
        }

        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest product)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new Exception("User is not authenticated");
            }
            var newProduct = _mapper.Map<Product>(product);

            AppUsers vendor = await _userManager.FindByIdAsync(userId);

            if (vendor is null)
            {
                throw new Exception("Vendor is not found in database");
            }

            newProduct.UserId = Guid.Parse(vendor.Id);

            var createProduct = await _productRepo.AddAsync(newProduct);

            if (createProduct == null)
            {
                throw new Exception("Unable to create new product");
            }
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<ProductResponse>(createProduct);
            return result;
        }


        public async Task<List<ProductResponse>> GetProductAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                throw new Exception("user is not authenticated");
            }

            AppUsers vendor = await _userManager.FindByIdAsync(userId);
            if (vendor is null)
            {
                throw new Exception("Vendor is not found");
            }

            var product = _productRepo.GetQueryable(p => p.UserId.ToString() == vendor.Id.ToString());

            var result = _mapper.Map<List<ProductResponse>>(product);
            return result;
        }


        public ICollection<Product> GetAllProducts()
        {

            return _productRepo.GetAll().ToList();

        }


        public async Task<string> DeleteProductAsync(Guid Id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                throw new Exception("user is not authenticated");
            }

            var vendor = await _userManager.FindByIdAsync(userId);

            if (vendor is null)
            {
                throw new Exception("user is not found");
            }

            Product product = await _productRepo.GetSingleByAsync(p => p.ProductId == Id);

            if (product is null)
            {
                throw new InvalidOperationException("Product doesn't exist");
            }

            await _productRepo.DeleteAsync(product);
            return $"{product.ProductName} has been deleted successfully";

        }


        public async Task<string> UpdateProductAsync(Guid productId, UpdateProductRequest productRequest)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                throw new Exception("User not found");
            }
            Product productExists = await _productRepo.GetSingleByAsync(p => p.ProductId == productId);

            if (productExists is null)
            {
                throw new Exception("Product does not exist");
            }

            var product = _mapper.Map(productRequest, productExists);

            await _productRepo.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return $"{product.ProductName} is updated successfully";

            /*
             * AppUsers userExists = await _userManager.FindByIdAsync(product.UserId.ToString());
            if (userExists == null)
                throw new Exception("User doesn't exist");

            var productExists = await _productRepo.GetSingleByAsync(p => p.ProductId == product.ProductId);
            if (productExists == null)
                throw new Exception("Product doesn't exist");
            _mapper.Map(product, productExists);
            var updatedProduct = await _productRepo.UpdateAsync(productExists);
            if (updatedProduct == null)
                throw new Exception("Unable to update product");

            var result = _mapper.Map<ProductResponse>(updatedProduct);
            return result;*/
        }

        public async Task<List<ProductSearchResponseDto>> SearchProductAsync(SearchRequestDto searchParam)
        {
            var allProducts = await _productRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(searchParam.Search))
            {
                allProducts = allProducts.Where(i => i.ProductName.Contains(searchParam.Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var result = _mapper.Map<List<ProductSearchResponseDto>>(allProducts);

            return result;
        }

        public async Task<ProductSearchResponseDto> GetProductById(Guid productId)
        {

            var product = await _productRepo.GetSingleByAsync(p => p.ProductId == productId);

            if (product is null)
            {
                throw new Exception("Product does not exist");
            }

            var mappedProduct = _mapper.Map<ProductSearchResponseDto>(product);
            return mappedProduct;
        }

        public async Task<string> AddToCartAsync(Guid productId, int quantity)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                throw new Exception("User is not authenticated");
            }

            var vendor = await _userManager.FindByIdAsync(userId);

            if (vendor is null)
            {
                throw new Exception("User is not found");
            }

            var productExists = await _productRepo.GetSingleByAsync(p => p.ProductId == productId);

            if (productExists is null)
            {
                throw new Exception("Product is not found");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Invalid quantity");
            }

            var cartExists = await _cartRepo.GetSingleByAsync(c => c.UserId == Guid.Parse(vendor.Id),
                include: i => i.Include(ci => ci.CartItems)
            );

            if (cartExists is null)
            {
                cartExists = new Cart
                {
                    UserId = Guid.Parse(userId),
                };
                await _cartRepo.AddAsync(cartExists);
            }

            if (cartExists.CartItems is null)
            {
                cartExists.CartItems = new List<CartItem>();
            }

            var cartItemExists = cartExists.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItemExists is not null)
            {
                cartItemExists.Quantity += quantity;
            }
            else
            {
                cartItemExists = new CartItem
                {
                    Quantity = quantity,
                    ProductId = productId
                };

                cartExists.CartItems.Add(cartItemExists);
            }

            await _cartRepo.UpdateAsync(cartExists);

            productExists.Quantity -= quantity;
            if (productExists.Quantity <= 0 && productExists.Quantity > quantity)
            {
                return $"{productExists.ProductName} is sold out";
            }

            await _productRepo.UpdateAsync(productExists);

            return $"{productExists.ProductName} has been added to cart successfully";

        }


    }
}
