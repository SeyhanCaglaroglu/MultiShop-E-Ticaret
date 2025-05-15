using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Areas.Admin.Models
{
    public class OfferDiscountViewModel
    {
        public UpdateOfferDiscountDto UpdateOfferDiscountDto { get; set; }
        public CreateOfferDiscountDto CreateOfferDiscountDto { get; set; }
        public List<ResultCategoryDto> ResultCategories { get; set; }
    }
}
