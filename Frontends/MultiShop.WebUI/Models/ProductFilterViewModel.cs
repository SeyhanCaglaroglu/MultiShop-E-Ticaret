using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Models
{
    public class ProductFilterViewModel
    {
        public List<ResultProductDto> Products { get; set; }
        public List<(string Range,int Count)> PriceFilters { get; set; }
    }
}
