using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductImageDto>("productimages", createProductImageDto);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _httpClient.DeleteAsync("productimages?id=" + id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("productimages");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductImageDto>>();
            return values;
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("productimages/" + id);

            // Yanıt başarılı mı kontrol et
            if (responseMessage.IsSuccessStatusCode)
            {
                try
                {
                    // JSON içeriğini okuma
                    var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductImageDto>();

                    // Eğer içerik null ise, boş bir nesne döndürüyoruz
                    if (value == null)
                    {
                        Console.WriteLine("Geçerli bir JSON veri alınamadı.");
                        return new GetByIdProductImageDto(); // Boş bir nesne döndürüyoruz
                    }

                    return value;
                }
                catch (System.Text.Json.JsonException ex)
                {
                    // JSON okuma hatası durumunda yapılacak işlemler
                    Console.WriteLine($"JSON okuma hatası: {ex.Message}");
                    return null; // veya uygun bir hata nesnesi döndürülebilir
                }
            }
            else
            {
                // Yanıt başarısızsa, hata mesajı loglanabilir
                Console.WriteLine($"API Hatası: {responseMessage.StatusCode} - {responseMessage.ReasonPhrase}");
                return null; // Başarısız yanıt durumunda null döndürüyoruz
            }
        }


        public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("productimages/GetProductImageByProductId/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();

                // JSON içeriğini konsola yazdırarak kontrol edin
                Console.WriteLine("Received JSON: " + content);

                // Deserialization işlemi Newtonsoft.Json ile yapılır
                try
                {
                    var result = JsonConvert.DeserializeObject<GetByIdProductImageDto>(content);

                    // Eğer result null ise, içerik boş veya uyumsuz olabilir
                    if (result == null)
                    {
                        Console.WriteLine("Deserialization result is null.");
                    }

                    return result;
                }
                catch (System.Text.Json.JsonException ex)
                {
                    // Deserialization hatası durumunda hata mesajını yazdırın
                    Console.WriteLine("Deserialization error: " + ex.Message);
                    return null;
                }
            }
            else
            {
                // Hata durumunda null döndür
                return null;
            }
        }




        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("productimages", updateProductImageDto);
        }

        
    }
}
