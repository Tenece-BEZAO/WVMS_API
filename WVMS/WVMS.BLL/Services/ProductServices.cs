using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public async Task<ProductResponse> CreateProduct(CreateProductRequest product)
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




        public IEnumerable<Product> GetProduct(Guid userId)
        {
            var product = _productRepo.GetQueryable(p => p.UserId.ToString() == userId.ToString()).OrderBy(i => i.ProductId);


            if (product == null)
                throw new InvalidOperationException("Sorry, there's no product with that Id");

            return product;

        }


        public ICollection<Product> GetAllProducts()
        {
            return _productRepo.GetAll().ToList();
        }


        public async Task DeleteProduct(Guid Id)
        {
            Product product = await _productRepo.GetSingleByAsync(p => p.ProductId.ToString() == Id.ToString());
            if (product == null)
                throw new InvalidOperationException("Product doesn't exist");

            await _productRepo.DeleteAsync(product);
            return;

        }


        public async Task<ProductResponse> UpdateProduct(UpdateProductRequest product)
        {
            AppUsers userExists = await _userManager.FindByIdAsync(product.UserId.ToString());
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
            return result;
        }


    }
}
