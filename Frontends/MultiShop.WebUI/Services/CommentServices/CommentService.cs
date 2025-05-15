using MultiShop.DtoLayer.CommentDtos;
using System.Text.Json;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCommentDto>("Comments", createCommentDto);
        }

        public async Task DeleteCommentAsync(string id)
        {
            await _httpClient.DeleteAsync("Comments?id=" + id);
        }

        public async Task<List<ResultCommentDto>> GetAllCommentsAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("Comments");

                // HTTP isteği başarılı mı kontrol et
                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Hata durumunda uygun bir exception fırlat
                    throw new Exception($"Error fetching comments. Status Code: {responseMessage.StatusCode}");
                }

                // Yanıtın JSON olarak doğru formatta olup olmadığını kontrol et
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();

                // Eğer JSON boş veya null ise, boş bir liste döndür
                if (values == null)
                {
                    return new List<ResultCommentDto>();
                }

                return values;
            }
            catch (JsonException jsonEx)
            {
                // JSON okuma hatası durumunda uygun bir hata mesajı
                throw new Exception("An error occurred while deserializing the response.", jsonEx);
            }
            catch (Exception ex)
            {
                // Diğer tüm hatalar için genel hata mesajı
                throw new Exception("An error occurred while fetching comments.", ex);
            }
        }


        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Comments/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>();
            return value;
        }

        public async Task<List<ResultCommentDto>> GetByProductIdWithCommentAsync(string id)
        {
            try
            {
                // İlgili endpoint'e GET isteği yapılıyor
                var responseMessage = await _httpClient.GetAsync("Comments/GetCommentByProductId/" + id);

                // HTTP durum kodunu kontrol et
                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Başarısız durumda hata fırlat
                    throw new Exception($"Error fetching comments for product ID {id}. Status Code: {responseMessage.StatusCode}");
                }

                // JSON verisini oku
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();

                // Eğer JSON verisi null dönerse, boş bir liste döndür
                if (values == null)
                {
                    return new List<ResultCommentDto>();
                }

                return values;
            }
            catch (JsonException jsonEx)
            {
                // JSON deserialization hatası durumunda özel hata mesajı
                throw new Exception("An error occurred while deserializing the response.", jsonEx);
            }
            catch (Exception ex)
            {
                // Diğer hatalar için genel hata mesajı
                throw new Exception($"An error occurred while fetching comments for product ID {id}.", ex);
            }
        }

        public async Task<int> GetCommentCountByProductId(string id)
        {
            try
            {
                // İlgili endpoint'e GET isteği yapılıyor
                var responseMessage = await _httpClient.GetAsync("Comments/GetCommentByProductId/" + id);

                // HTTP durum kodunu kontrol et
                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Başarısız durumda hata fırlat
                    throw new Exception($"Error fetching comments for product ID {id}. Status Code: {responseMessage.StatusCode}");
                }

                // JSON verisini oku
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();

                // Eğer JSON verisi null dönerse, boş bir liste döndür
                if (values == null)
                {
                    return new int();
                }

                return values.Count;
            }
            catch (JsonException jsonEx)
            {
                // JSON deserialization hatası durumunda özel hata mesajı
                throw new Exception("An error occurred while deserializing the response.", jsonEx);
            }
            catch (Exception ex)
            {
                // Diğer hatalar için genel hata mesajı
                throw new Exception($"An error occurred while fetching comments for product ID {id}.", ex);
            }
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCommentDto>("Comments", updateCommentDto);
        }
    }
}
