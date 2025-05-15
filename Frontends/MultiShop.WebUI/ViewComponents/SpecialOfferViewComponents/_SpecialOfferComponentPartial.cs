using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.SpecialOfferViewComponents
{
    public class _SpecialOfferComponentPartial : ViewComponent
    {
        private  readonly ISpecialOfferService _specialOfferService;

        public _SpecialOfferComponentPartial(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _specialOfferService.GetAllSpecialOffersAsync();

            if (values != null && values.Any())
            {
                return View(values);
            }
            return View();
        }
    }
}
