using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<ResultInBoxMessageDto>> GetInBoxMessageAsync(string id);
        Task<List<ResultSendBoxMessageDto>> GetSendBoxMessageAsync(string id);

        Task CreateMessageAsync(CreateMessageDto createMessageDto);
        
        Task DeleteMessageAsync(int id);
        Task<GetByIdMessageDto> GetByIdMessageAsync(int id);
    }
}
