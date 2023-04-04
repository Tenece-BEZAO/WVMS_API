using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.Services
{
    public class ProductServices : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IRepository<Product> _productRepo;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _productRepo = _unitOfWork.GetRepository<Product>();
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest product)
        {
           
            AppUsers userExists = await _userManager.FindByIdAsync(product.UserId.ToString());
            if(userExists == null)
            {
                throw new Exception("User doesn't exist");
            }

            var newProduct = _mapper.Map<Product>(product);
            var createProduct = await _productRepo.AddAsync(newProduct);

            if(createProduct == null)
            {
                throw new Exception("Unable to create new product");
            }
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
             /*public Product GetProduct(Guid userId)
            {
            var product = _productRepo.GetQueryable(p => p.UserId.ToString() == userId.ToString()).FirstOrDefault();

            if (product == null)
                throw new InvalidOperationException("Sorry, there's no product with that Id");

            return product;
            }
            */

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

        /*public async Task<ProductResponse> UpdateProduct(CreateProductRequest product)
       {
           AppUsers userExists = await _userManager.FindByIdAsync(product.UserId.ToString());
           if (userExists == null)
           {
               throw new Exception("User doesn't exist");
           }

           var existingProduct = await _productRepo.GetSingleByAsync(p => p.ProductId == product.ProductId);

           if (existingProduct == null)
           {
               throw new Exception("Product doesn't exist");
           }

           _mapper.Map(product, existingProduct);

           var updatedProduct = await _productRepo.UpdateAsync(existingProduct);

           if (updatedProduct == null)
           {
               throw new Exception("Unable to update product");
           }

           var result = _mapper.Map<ProductResponse>(updatedProduct);
           return result;
       }
       */

        public async Task<ProductResponse> UpdateProduct(CreateProductRequest product)
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

       /* public async Task<ProductResponse> UpdateProduct(CreateProductRequest request)
        {
            AppUsers user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if(user == null)
                throw new InvalidOperationException("User does not exist");

            var productUser = await _productRepo.GetSingleByAsync(w => w.ProductId == request.ProductId);

            if(productUser == null)
                throw new Exception("This user does not have any product");

            _mapper.Map(request, productUser);
            //CCreateMap<Report, ReportResponseForUpdateDto>();  
            Product productUpdated = await _productRepo.UpdateAsync(productUser);
            var result = _mapper.Map<CreateProductRequest>(productUpdated);

            return result;




            //var userReport = await _reportRepo.GetSingleByAsync(r => r.ReportId == modelRequest.Id);

            *//*Product newProduct = await _productRepo.GetSingleByAsync(p => p.ProductId == Id);
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

            return "Updated";*//*
            throw new NotImplementedException();
        }*/

        /*public async Task<ReportResponseForUpdateDto> UpdateReportAsync(ReportRequestForUpdateDto modelRequest)
        {
            AppUsers user = await _userManager.FindByIdAsync(modelRequest.UserId.ToString());

            if (user == null)
            {
                throw new Exception("user is not found");
            }

            var userReport = await _reportRepo.GetSingleByAsync(r => r.ReportId == modelRequest.Id);

            if (userReport is null)
            {
                throw new Exception("User does not have a report");
            }
                _mapper.Map(modelRequest, userReport);
            //CCreateMap<Report, ReportResponseForUpdateDto>();  
            Report reportUpdated = await _reportRepo.UpdateAsync(userReport);
            var result = _mapper.Map<ReportResponseForUpdateDto>(reportUpdated);

            return result;
        }

            */
    }
}
