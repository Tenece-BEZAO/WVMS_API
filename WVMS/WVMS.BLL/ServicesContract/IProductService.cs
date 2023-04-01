using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;
using WVMS.Shared.Dtos.Request;

namespace WVMS.BLL.ServicesContract
{
    public interface IProductService
    {
        Task<Product> GetProduct(int id);
        ICollection<Product> GetAllProducts();
        bool ProductExist(int prodId);
        Task<string> UpdateProduct(int Id, CreateProductRequest request);
        Task <string> CreateProduct(ProductDto product);
        Task DeleteProduct(int Id);
    }
}
