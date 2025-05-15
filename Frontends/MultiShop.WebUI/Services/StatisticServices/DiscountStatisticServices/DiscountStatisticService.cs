
using System.Net.Http;
using System.Text.Json;

namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices
{
    public class DiscountStatisticService : IDiscountStatisticService
    {
        private readonly HttpClient _httpClient;

        public DiscountStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountCouponCount()
        {
            var responseMessage = await _httpClient.GetAsync("Discounts/GetDiscountCouponCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadAsStringAsync();

                if (int.TryParse(value, out var commentCount))
                {
                    return commentCount;
                }
                else
                {
                    throw new JsonException("Beklenen int formatında bir JSON alınamadı.");
                }
            }
            else
            {
                throw new HttpRequestException($"İstek Başarısız Oldu ! {responseMessage.StatusCode}");
            }
        }
    }
}
