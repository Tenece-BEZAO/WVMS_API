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
using WVMS.Shared.Dtos.Request;

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

        public async Task<string> CreateProduct(ProductDto product)
        {
            Product newProduct = new Product { ProductName = product.ProductName, Price = product.Price, Description = product.Description, ExpiryDate= product.ExpiryDate, Quantity = product.Quantity, VendorId = (int)product.VendorId };
            var result = await _productRepo.AddAsync(newProduct);

            if (result != null)
                return result.ProductId.ToString();

            return "Error";
        }


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
            return;
        }

        public async Task<string> UpdateProduct(int Id, CreateProductRequest request)
        {
            Product newProduct = await _productRepo.GetSingleByAsync(p => p.ProductId == Id);
            if (newProduct == null)
                throw new InvalidOperationException("Product does not exist");
            //  Product product1 = _mapper.Map(request, newProduct);
            newProduct.ProductName = request.ProductName;
            newProduct.Description = request.Description;
            newProduct.Quantity = request.Quantity;
            newProduct.Price = request.Price;
            newProduct.ExpiryDate = request.ExpiryDate;
            newProduct.VendorId = request.VendorId;

      var updated =   await _productRepo.UpdateAsync(newProduct);
            if (updated == null)
                throw new NotImplementedException("was unable to update");

            return "Updated";

        }

        
    }
}
