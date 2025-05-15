
using System.Text.Json;

namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices
{
    public class CommentStatisticService : ICommentStatisticService
    {
        private readonly HttpClient _httpClient;

        public CommentStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetActiveCommentCount()
        {
            var responseMessage = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            if(responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadAsStringAsync();

                if(int.TryParse(value, out var commentCount))
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

        public async Task<int> GetPassiveCommentCount()
        {
            var responseMessage = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
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

        public async Task<int> GetTotalCommentCount()
        {
            var responseMessage = await _httpClient.GetAsync("comments/GetTotalCommentCount");
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
