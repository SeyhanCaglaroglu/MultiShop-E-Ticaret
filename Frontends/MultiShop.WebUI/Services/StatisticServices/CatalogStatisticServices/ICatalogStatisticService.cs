namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public interface ICatalogStatisticService
    {
        Task<long> GetBrandCount();
        Task<long> GetCategoryCount();
        Task<long> GetProductCount();
        Task<decimal> GetProductAvgPrice();
        Task<string> GetMaxPriceProductName();
        Task<string> GetMinPriceProductName();
    }
}
