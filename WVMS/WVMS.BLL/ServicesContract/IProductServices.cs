using WVMS.DAL.Entities;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.ServicesContract
{
    public interface IProductServices
    {
        Task<ProductResponse> CreateProduct(CreateProductRequest product);
        Task DeleteProduct(Guid Id);
        ICollection<Product> GetAllProducts();
        IEnumerable<Product> GetProduct(Guid userId);
        Task<ProductResponse> UpdateProduct(UpdateProductRequest product);
    }
}