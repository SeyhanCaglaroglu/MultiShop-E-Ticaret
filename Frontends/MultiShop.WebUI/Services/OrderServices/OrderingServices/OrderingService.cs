using MultiShop.DtoLayer.OrderDtos.OrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public class OrderingService : IOrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserId>> GetOrderingByUserId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("orderings/GetOrderingsByUserId?id=" +  id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultOrderingByUserId>>();
            return values;
        }
    }
}
