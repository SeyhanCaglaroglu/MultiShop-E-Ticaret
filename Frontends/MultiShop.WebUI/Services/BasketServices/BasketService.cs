using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            // Mevcut sepeti al
            var values = await GetBasket();

            if (values != null)
            {
                // Eğer ürün sepet içinde yoksa ekle
                var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
                if (existingItem == null)
                {
                    // Ürün sepete ekleniyor
                    values.BasketItems.Add(basketItemDto);
                }
                else
                {
                    // Ürün sepette varsa, miktarı artır
                    existingItem.Quantity += basketItemDto.Quantity;
                }

                // Sepeti kaydetme işlemi
                await SaveBasket(values);
            }
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync("baskets?id=" +  userId);
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // Durum kodunu loglayarak hata sebebini belirleyebilirsiniz.
                throw new Exception($"API hatası: {responseMessage.StatusCode}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
            return values;
        }


        public async Task<bool> RemoveBasketItem(string id)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x=>x.ProductId==id);
            values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basketTotalDto);
        }
    }
}
