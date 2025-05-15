using MultiShop.DtoLayer.NotificationDtos;

namespace MultiShop.WebUI.Services.NotificationServices
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateNotificationAsync(CreateNotificationDto createNotificationDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("notifications", createNotificationDto);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync(); // Hata mesajını al
                throw new HttpRequestException(
                    $"Error creating notification. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}, Content: {errorContent}");
            }
        }


        public async Task DeleteAllNotificationAsync()
        {
            var responseMessage = await _httpClient.DeleteAsync("notifications/DeleteAllNotifications");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync(); // Hata mesajını al
                throw new HttpRequestException(
                    $"Error creating notification. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}, Content: {errorContent}");
            }
        }

        public async Task DeleteNotificationAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync("notifications/DeleteNotification?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync(); // Hata mesajını al
                throw new HttpRequestException(
                    $"Error creating notification. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}, Content: {errorContent}");
            }
        }

        public async Task<List<ResultNotificationDto>> GetAllNotificationsAsync()
        {
            var responseMessage = await _httpClient.GetAsync("notifications");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching notifications. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}");
            }

            if (responseMessage.Content == null)
            {
                throw new InvalidOperationException("Response content is null.");
            }

            try
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultNotificationDto>>();
                return values ?? new List<ResultNotificationDto>(); // Eğer `null` dönerse, boş bir liste döndür
            }
            catch (Exception ex)
            {
                // JSON dönüşüm hatalarını logla ve yeniden fırlat
                throw new InvalidOperationException("Error deserializing the response content.", ex);
            }
        }


        public async Task<GetByIdNotificationDto> GetByIdNotificationAsync(int id)
        {
            // API'ye istek gönder
            var responseMessage = await _httpClient.GetAsync("notifications?id=" + id);

            // Yanıtın başarılı olup olmadığını kontrol et
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error fetching notification with ID {id}. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}");
            }

            // Yanıt içeriğinin boş olup olmadığını kontrol et
            if (responseMessage.Content == null)
            {
                throw new InvalidOperationException("Response content is null.");
            }

            try
            {
                // Yanıt içeriğini deserializasyondan geçir
                var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdNotificationDto>();

                // Dönen değer null ise hata fırlat
                if (value == null)
                {
                    throw new InvalidOperationException($"Notification with ID {id} not found.");
                }

                return value;
            }
            catch (Exception ex)
            {
                // JSON dönüşüm hatalarını yakala ve yeniden fırlat
                throw new InvalidOperationException("Error deserializing the response content.", ex);
            }
        }

        public async Task<int> NotificationCount()
        {
            // API'ye GET isteği gönder
            var responseMessage = await _httpClient.GetAsync("notifications/NotificationCount");

            // İstek başarılı mı kontrol et
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching notification count. Status code: {responseMessage.StatusCode}, Reason: {responseMessage.ReasonPhrase}");
            }

            // Yanıt içeriğini oku ve integer olarak döndür
            var value = await responseMessage.Content.ReadAsStringAsync();

            // Eğer içerik null veya geçerli bir sayı değilse hata fırlat
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var count))
            {
                throw new InvalidOperationException("Failed to parse the notification count from the response.");
            }

            return count;
        }

    }
}
