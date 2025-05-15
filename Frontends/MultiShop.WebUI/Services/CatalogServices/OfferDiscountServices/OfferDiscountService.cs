using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using System.Text.Json;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly HttpClient _httpClient;

        public OfferDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            await _httpClient.PostAsJsonAsync<CreateOfferDiscountDto>("offerdiscounts", createOfferDiscountDto);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync("offerdiscounts?id=" + id);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountsAsync()
        {
            var responseMessage = await _httpClient.GetAsync("offerdiscounts");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultOfferDiscountDto>>();
            return values;
        }

        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            // API isteğini yap
            var responseMessage = await _httpClient.GetAsync("offerdiscounts/" + id);

            // Yanıt başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // Yanıtın içeriğini JSON'a dönüştür
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>();

                // Eğer gelen veri null ise, null döndür
                if (value == null)
                {
                    return null;
                }

                return value;
            }

            // Eğer yanıt başarısızsa, null döndür
            return null;
        }

        public async Task<GetByIdOfferDiscountDto> GetOfferDiscountByCategoryId(string id)
        {
            // API isteğini yap
            var responseMessage = await _httpClient.GetAsync("offerdiscounts/GetOfferDiscountByCategoryId?id=" + id);

            // Yanıt başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // Yanıtın boş olup olmadığını kontrol et
                if (responseMessage.Content == null || responseMessage.Content.Headers.ContentLength == 0)
                {
                    return null;
                }

                try
                {
                    // Yanıtı JSON olarak parse et
                    var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdOfferDiscountDto>();
                    return value;
                }
                catch (JsonException ex)
                {
                    // JSON formatı geçersizse hata yakala
                    throw new Exception("API'den gelen yanıt geçerli bir JSON formatında değil.", ex);
                }
            }

            // Başarısız bir yanıt dönerse
            return null;
        }


        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("offerdiscounts",updateOfferDiscountDto);
        }
    }
}
