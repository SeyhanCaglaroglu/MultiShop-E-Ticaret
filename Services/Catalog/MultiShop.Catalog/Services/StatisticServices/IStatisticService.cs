namespace MultiShop.Catalog.Services.StatisticServices
{
    public interface IStatisticService
    {
        long GetBrandCount();
        long GetCategoryCount();
        long GetProductCount();
        decimal GetProductAvgPrice();
        string GetMaxPriceProductName();
        string GetMinPriceProductName();
    }
}
