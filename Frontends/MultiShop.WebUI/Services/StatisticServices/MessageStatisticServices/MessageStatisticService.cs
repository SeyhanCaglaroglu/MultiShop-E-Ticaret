
using System.Net.Http;
using System.Text.Json;

namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices
{
    public class MessageStatisticService : IMessageStatisticService
    {
        private readonly HttpClient _httpClient;

        public MessageStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalMessageCount()
        {
            var responseMessage = await _httpClient.GetAsync("UserMessage/GetTotalMessageCount");
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
