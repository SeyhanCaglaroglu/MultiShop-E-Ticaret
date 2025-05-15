using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailService
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDetailDto>("ProductDetails", createProductDetailDto);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _httpClient.DeleteAsync("ProductDetails?id=" + id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var responseMessage = await _httpClient.GetAsync("ProductDetails");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDetailDto>>();
            return values;
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("ProductDetails/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDetailDto>();
            return value;
        }

        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
        {
            // API isteği gönderiliyor
            var responseMessage = await _httpClient.GetAsync("ProductDetails/GetProductDetailByProductId/" + id);

            // Yanıt başarılı mı kontrol et
            if (responseMessage.IsSuccessStatusCode)
            {
                try
                {
                    // JSON içeriğini okuma
                    var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDetailDto>();

                    // Eğer içerik null ise, boş bir nesne döndür
                    if (value == null)
                    {
                        Console.WriteLine("Geçerli bir JSON veri alınamadı.");
                        return new GetByIdProductDetailDto(); // Boş bir nesne döndürüyoruz
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


        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDetailDto>("ProductDetails", updateProductDetailDto);
        }

        
    }
}
