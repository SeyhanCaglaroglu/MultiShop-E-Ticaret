using MultiShop.DtoLayer.NotificationDtos;
using MultiShop.DtoLayer.NotificationDtos;

namespace MultiShop.WebUI.Services.NotificationServices
{
    public interface INotificationService
    {
        Task<List<ResultNotificationDto>> GetAllNotificationsAsync();
        Task CreateNotificationAsync(CreateNotificationDto createNotificationDto);
        Task DeleteNotificationAsync(int id);
        Task<GetByIdNotificationDto> GetByIdNotificationAsync(int id);
        Task DeleteAllNotificationAsync();
        Task<int> NotificationCount();

    }
}
