
using System.Text.Json;

namespace MultiShop.WebUI.Services.StatisticServices.UserStatisticServices
{
    public class UserStatisticServices : IUserStatisticServices
    {
        private readonly HttpClient _httpClient;

        public UserStatisticServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetUserCount()
        {
            var responseMessage = await _httpClient.GetAsync("/api/Statistics");

            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadAsStringAsync();

                // Yanıtı int'e dönüştürmeyi deneyin
                if (int.TryParse(value, out var userCount))
                {
                    return userCount;
                }
                else
                {
                    throw new JsonException("Beklenen int formatında bir JSON alınamadı.");
                }
            }
            else
            {
                // Başarısız olan yanıt için hata fırlatın
                throw new HttpRequestException($"İstek başarısız oldu: {responseMessage.StatusCode}");
            }

        }
    }
}
