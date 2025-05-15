using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _PriceFilterProductListComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;

        public _PriceFilterProductListComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? id)
        {
            var products = id == null
                ? await _productService.GetAllProductAsync()
                : await _productService.GetProductsByCategoryId(id);

            var highestPrice = products.Max(x => x.ProductPrice); // En yüksek fiyatı al
            ViewBag.ProductsCount = products.Count;

            var ranges = new List<(decimal Start, decimal End, int Count)>();

            // Sabit aralıklar
            decimal start = 0;
            decimal interval = 2000;
            decimal maxLimit = 20000; // 20.000₺ üst limit

            while (start < maxLimit)
            {
                decimal end = start + interval;

                // 20.000₺ sınırına ulaşıldığında
                if (end > maxLimit)
                {
                    end = maxLimit; // End değerini tam 20.000₺ olarak ayarla
                }

                // Ürünleri bu aralık için say
                int count = products.Count(x => x.ProductPrice > start && x.ProductPrice <= end);
                ranges.Add((start, end, count));

                start = end; // Başlangıç değerini güncelle
            }

            // 20.000₺ ve üzeri için ayrı aralık ekle
            int aboveLimitCount = products.Count(x => x.ProductPrice > maxLimit);
            if (aboveLimitCount > 0)
            {
                ranges.Add((maxLimit, decimal.MaxValue, aboveLimitCount));
            }

            // Fiyat aralıklarını metin formatına dönüştür
            var priceFilters = ranges
                .Select(r => r.End == decimal.MaxValue
                    ? ($"{r.Start:0}₺ ve üzeri", r.Count) // 20.000₺ ve üzeri formatı
                    : ($"{r.Start:0}₺ - {r.End:0}₺", r.Count)) // Diğer aralıklar
                .ToList();

            var model = new ProductFilterViewModel
            {
                PriceFilters = priceFilters,
                Products = products
            };

            return View(model);
        }



    }
}
