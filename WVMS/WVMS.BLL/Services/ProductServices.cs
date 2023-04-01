using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.DAL.Interfaces;
using WVMS.Shared.Dtos;

namespace WVMS.BLL.Services
{
    public class ProductServices : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepo = _unitOfWork.GetRepository<Product>();
        }

        public string CreateProduct(ProductDto product)
        {
            Product newProduct = new Product { ProductName = product.ProductName, Price = product.Price, Description = product.Description, ExpiryDate= product.ExpiryDate, Quantity = product.Quantity, VendorId = (int)product.VendorId };
            var result =  _productRepo.AddAsync(newProduct);

            if (result.IsCompletedSuccessfully)
                return "Success";

            return "Error";
        }

        /*public async Task<string> CreateProduct(ProductDto product)
        {
            Product newProduct = new Product { ProductName = product.ProductName, Price = product.Price, Description = product.Description, Quantity = product.Quantity, VendorId = product.VendorId };

            var newProductResult = _appDbContext.Products.AddAsync(newProduct);
            _appDbContext.SaveChanges();

            if (newProductResult.IsCompletedSuccessfully)
            {
                return "Success";
            }

            return "Error";

        }*/

        public async Task<Product> GetProduct(int id)
        {
            //return await _productRepo.GetSingleByAsync(p => p.ProductId == id);
            Product product = await _productRepo.GetSingleByAsync(p => p.ProductId == id);
            if (product == null)
                throw new InvalidOperationException("Sorry, there's no product with that Id");
            
            return product;
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productRepo.GetAll().ToList();
        }

        public bool ProductExist(int Id)
        {
            return _productRepo.Any(p => p.ProductId == Id); 
        }

        public async Task DeleteProduct(int Id)
        {
            Product product = await _productRepo.GetSingleByAsync(p => p.ProductId == Id);
            if (product == null)
                throw new InvalidOperationException("Product doesn't exist");

            await _productRepo.DeleteAsync(product);
            
        }

        public bool Save()
        {
            var saved = _productRepo.Save();
            return saved > 0 ? true : false;
        }

    }
}
