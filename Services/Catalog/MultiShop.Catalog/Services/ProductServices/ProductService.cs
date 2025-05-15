using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CateogryCollectionName);
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(values);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task<int> GetProductCountByCategoryId(string id)
        {
            var values = await _productCollection.Find(x => x.CategoryId == id).ToListAsync();
            return values.Count;
        }

        public async Task<List<ResultProductDto>> GetProductsByCategoryId(string id)
        {
            var values = await _productCollection.Find(x=>x.CategoryId == id).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<List<ResultProductDto>> GetProductsFilteredByPrice(string range)
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            if (range == "Tum Fiyatlar")
            {
                return _mapper.Map<List<ResultProductDto>>(products);
            }
            else
            {
                var parts = range.Replace("₺", "").Split("-");

                decimal minPrice = decimal.Parse(parts[0].Trim());
                decimal maxPrice = decimal.Parse(parts[1].Trim());



                var values = products.Where(x => Convert.ToInt32(x.ProductPrice) > minPrice && Convert.ToInt32(x.ProductPrice) <= maxPrice).ToList();

                return _mapper.Map<List<ResultProductDto>>(values);
            }

        }

        public async Task<List<ResultProductDto>> GetProductsFilteredByPriceWithCategory(string id, string range)
        {
            var products = await _productCollection.Find(x => x.CategoryId == id).ToListAsync();

            if (range == "Tum Fiyatlar")
            {
                return _mapper.Map<List<ResultProductDto>>(products);
            }
            else
            {
                var parts = range.Replace("₺", "").Split("-");

                decimal minPrice = decimal.Parse(parts[0].Trim());
                decimal maxPrice = decimal.Parse(parts[1].Trim());



                var values = products.Where(x => Convert.ToInt32(x.ProductPrice) > minPrice && Convert.ToInt32(x.ProductPrice) <= maxPrice).ToList();

                return _mapper.Map<List<ResultProductDto>>(values);
            }
        }

        public async Task<List<ResultProductDto>> GetProductsWithSearch(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return new List<ResultProductDto>();

            // Tüm ürünleri al
            var products = await _productCollection.Find(x => true).ToListAsync();

            // Levenshtein Distance hesaplama fonksiyonu
            int CalculateLevenshteinDistance(string source, string target)
            {
                if (string.IsNullOrEmpty(source))
                    return target?.Length ?? 0;
                if (string.IsNullOrEmpty(target))
                    return source.Length;

                int sourceLength = source.Length;
                int targetLength = target.Length;

                var distance = new int[sourceLength + 1, targetLength + 1];

                for (int i = 0; i <= sourceLength; i++)
                    distance[i, 0] = i;

                for (int j = 0; j <= targetLength; j++)
                    distance[0, j] = j;

                for (int i = 1; i <= sourceLength; i++)
                {
                    for (int j = 1; j <= targetLength; j++)
                    {
                        int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;

                        distance[i, j] = Math.Min(
                            Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                            distance[i - 1, j - 1] + cost
                        );
                    }
                }

                return distance[sourceLength, targetLength];
            }

            // Eşik değere göre benzer ürünleri filtrele
            int threshold = 1; // Benzerlik eşik değeri
            var matchingProducts = products.Where(product =>
            {
                var productWords = product.ProductName.Split(' ');

                return productWords
                .Any(word => CalculateLevenshteinDistance(word.ToLower(), productName.ToLower()) <= threshold)
                || CalculateLevenshteinDistance(product.ProductName.ToLower(), productName.ToLower()) <= threshold; // Tam eşleşme için
            })
        .ToList();

            // Sonuçları DTO'ya dönüştür
            return _mapper.Map<List<ResultProductDto>>(matchingProducts);
        }


        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).
                    FirstAsync();
            }
            return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var values = await _productCollection.Find(x => x.CategoryId == CategoryId).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }

            return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
    }
}
