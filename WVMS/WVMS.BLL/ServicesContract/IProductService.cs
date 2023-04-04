using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.ServicesContract
{
    public interface IProductService
    {
        IEnumerable<Product> GetProduct(Guid userId);
        //Task<Product> GetProduct(int id);
        ICollection<Product> GetAllProducts();
        /*        Task<CreateProductRequest> UpdateProduct(ProductResponse request);
        */
        Task<ProductResponse> UpdateProduct(CreateProductRequest product);
        Task <ProductResponse> CreateProduct(CreateProductRequest product);
        Task DeleteProduct(Guid Id);
    }
}
