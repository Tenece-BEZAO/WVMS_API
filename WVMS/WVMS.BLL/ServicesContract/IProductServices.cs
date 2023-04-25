using WVMS.DAL.Entities;
using WVMS.Shared.Dtos.Request;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.ServicesContract
{
    public interface IProductServices
    {
        Task<ProductResponse> CreateProductAsync(CreateProductRequest product);
        Task<string> DeleteProductAsync(Guid Id);
        ICollection<Product> GetAllProducts();
        Task<List<ProductResponse>> GetProductAsync();
        Task<string> UpdateProductAsync(Guid productId, UpdateProductRequest productRequest);
        Task<List<ProductSearchResponseDto>> SearchProductAsync(SearchRequestDto searchParam);
        Task<ProductSearchResponseDto> GetProductById(Guid productId);
        Task<string> AddToCartAsync(Guid productId, int quantity);
    }
}