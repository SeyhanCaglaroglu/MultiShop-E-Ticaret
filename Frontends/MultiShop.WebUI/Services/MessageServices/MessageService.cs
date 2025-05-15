using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            // API'ye POST isteği gönder
            var response = await _httpClient.PostAsJsonAsync("UserMessage", createMessageDto);

            // API yanıtının durum kodunu kontrol et
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Mesaj gönderilemedi. HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Detay: {errorContent}");
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            await _httpClient.DeleteAsync("UserMessage?id=" +  id);
        }

        public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync("usermessage/id?id=" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdMessageDto>();
            return value;
        }

        public async Task<List<ResultInBoxMessageDto>> GetInBoxMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("usermessage/GetMessageInBox?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultInBoxMessageDto>>();
            return values;
        }

        public async Task<List<ResultSendBoxMessageDto>> GetSendBoxMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("usermessage/GetMessageSendBox?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendBoxMessageDto>>();
            return values;
        }
    }
}
