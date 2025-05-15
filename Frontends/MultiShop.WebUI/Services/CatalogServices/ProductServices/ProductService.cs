using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDto>("products",createProductDto);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("products?id=" + id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("products/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
            return value;
        }

        public async Task<int> GetProductCountByCategoryId(string id)
        {
            // API çağrısı yapılıyor
            var responseMessage = await _httpClient.GetAsync("products/GetProductCountByCategoryId/" + id);

            // Eğer yanıt başarılı değilse, hata işleme yapın
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API çağrısı başarısız oldu. StatusCode: {responseMessage.StatusCode}");
            }

            // Yanıt içeriğini string olarak alıyoruz
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            // String cevabı integer'a dönüştürüyoruz
            return int.Parse(responseString);  // veya int.TryParse() ile hata kontrolü ekleyebilirsiniz
        }

        public async Task<List<ResultProductDto>> GetProductsByCategoryId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsByCategoryId?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<List<ResultProductDto>> GetProductsFilteredByPrice(string range)
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsFilteredByPrice?range=" + range);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<List<ResultProductDto>> GetProductsFilteredByPriceWithCategory(string id, string range)
        {
            var responseMessage = await _httpClient.GetAsync($"products/GetProductsFilteredByPriceWithCategory?id={id}&range={range}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<List<ResultProductDto>> GetProductsWithSearch(string productName)
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsWithSearch?productName=" +  productName);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products/ProductListWithCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var responseMessage = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryId/" + CategoryId);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("products",updateProductDto);
        }
    }
}
