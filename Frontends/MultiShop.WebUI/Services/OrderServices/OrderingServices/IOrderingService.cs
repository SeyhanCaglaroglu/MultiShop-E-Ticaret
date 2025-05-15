using MultiShop.DtoLayer.OrderDtos.OrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public interface IOrderingService
    {
        Task<List<ResultOrderingByUserId>> GetOrderingByUserId(string id);
    }
}
