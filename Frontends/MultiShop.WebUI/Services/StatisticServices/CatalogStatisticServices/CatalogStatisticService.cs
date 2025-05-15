
namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticService : ICatalogStatisticService
    {
        private readonly HttpClient _httpClient;

        public CatalogStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetBrandCount()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetBrandCount");
            var value = await responseMessage.Content.ReadFromJsonAsync<long>();
            return value;
        }

        public async Task<long> GetCategoryCount()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetCategoryCount");
            var value = await responseMessage.Content.ReadFromJsonAsync<long>();
            return value;
        }

        public async Task<string> GetMaxPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetMaxPriceProductName");
            var value = await responseMessage.Content.ReadAsStringAsync();
            return value;
        }

        public async Task<string> GetMinPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetMinPriceProductName");
            var value = await responseMessage.Content.ReadAsStringAsync();
            return value;
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetProductAvgPrice");
            var value = await responseMessage.Content.ReadFromJsonAsync<decimal>();
            return value;
        }

        public async Task<long> GetProductCount()
        {
            var responseMessage = await _httpClient.GetAsync("Statistics/GetProductCount");
            var value = await responseMessage.Content.ReadFromJsonAsync<long>();
            return value;
        }
    }
}
