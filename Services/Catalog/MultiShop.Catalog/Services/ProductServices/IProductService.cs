using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string CategoryId);
        Task<int> GetProductCountByCategoryId(string id);
        Task<List<ResultProductDto>> GetProductsFilteredByPrice(string range);
        Task<List<ResultProductDto>> GetProductsFilteredByPriceWithCategory(string id,string range);
        Task<List<ResultProductDto>> GetProductsWithSearch(string productName);
        Task<List<ResultProductDto>> GetProductsByCategoryId(string id);
    }
}
