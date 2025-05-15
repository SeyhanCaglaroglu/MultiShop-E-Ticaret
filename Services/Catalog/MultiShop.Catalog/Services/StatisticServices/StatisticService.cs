using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Product> _productCollection;

        public StatisticService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CateogryCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        }

        public long GetBrandCount()
        {
            return _brandCollection.CountDocuments(FilterDefinition<Brand>.Empty);
            
        }

        public long GetCategoryCount()
        {
            return _categoryCollection.CountDocuments(FilterDefinition<Category>.Empty);
        }

        public string GetMaxPriceProductName()
        {
            var products = _productCollection.AsQueryable()
                .Where(p => p.ProductPrice != null) // Null olmayan fiyatları filtreliyoruz
                .ToList(); // Tüm ürünleri çekiyoruz

            var maxPriceProduct = products.OrderByDescending(p => p.ProductPrice) // En yüksek fiyatı sıralıyoruz
                .FirstOrDefault(); // İlk ürünü alıyoruz

            return maxPriceProduct?.ProductName ?? "Ürün bulunamadı"; // Sonuç yoksa uygun bir mesaj döndürüyoruz
        }
        public string GetMinPriceProductName()
        {
            var products = _productCollection.AsQueryable()
                .Where(p => p.ProductPrice != null) // Null olmayan fiyatları filtreliyoruz
                .ToList(); // Tüm ürünleri çekiyoruz

            var minPriceProduct = products.OrderBy(p => p.ProductPrice) // En düşük fiyatı sıralıyoruz
                .FirstOrDefault(); // İlk ürünü alıyoruz

            return minPriceProduct?.ProductName ?? "Ürün bulunamadı"; // Sonuç yoksa uygun bir mesaj döndürüyoruz
        }


        public decimal GetProductAvgPrice()
        {
            var result = _productCollection.Aggregate()
                .Match(p => p.ProductPrice != null) // Null olmayan fiyatları filtreliyoruz
                .Group(p => 1, g => new { AvgPrice = g.Average(p => (double)p.ProductPrice) }) // Price alanını double olarak alıyoruz
                .FirstOrDefault();

            return (decimal)(result?.AvgPrice ?? 0); // double olan sonucu decimal’e dönüştürüyoruz
        }





        public long GetProductCount()
        {
            return _productCollection.CountDocuments(FilterDefinition<Product>.Empty);
        }
    }
}
