using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Areas.Admin.Models
{
    public class SpecialOfferViewModel
    {
        public CreateSpecialOfferDto CreateSpecialOfferDto { get; set; }
        public List<ResultProductDto> Products { get; set; }
        public UpdateSpecialOfferDto updateSpecialOfferDto { get; set; }
    }
}
