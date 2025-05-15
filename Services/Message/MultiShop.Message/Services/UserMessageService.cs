using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entities;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly MessageContext _messageContext;
        private readonly IMapper _mapper;
        public UserMessageService(MessageContext messageContext, IMapper mapper)
        {
            _messageContext = messageContext;
            _mapper = mapper;
        }

        public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            var value = _mapper.Map<UserMessage>(createMessageDto);
            await _messageContext.UserMessages.AddAsync(value);
            await _messageContext.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(int id)
        {
            var value = await _messageContext.UserMessages.FindAsync(id);

            if (value == null)
            {
                throw new InvalidOperationException("Message not found.");
            }

            _messageContext.UserMessages.Remove(value);

            await _messageContext.SaveChangesAsync();
        }


        public async Task<List<ResultMessageDto>> GetAllMessageAsync()
        {
            var values = await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDto>>(values);
        }

        public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
        {
            var value = await _messageContext.UserMessages.FindAsync(id);
            return _mapper.Map<GetByIdMessageDto>(value);
        }

        public async Task<List<ResultInBoxMessageDto>> GetInBoxMessageAsync(string id)
        {
            var values = await _messageContext.UserMessages.Where(x=>x.ReceivedId == id).ToListAsync();
            return _mapper.Map<List<ResultInBoxMessageDto>>(values);
        }

        public async Task<List<ResultSendBoxMessageDto>> GetSendBoxMessageAsync(string id)
        {
            var values = await _messageContext.UserMessages.Where(x => x.SenderId == id).ToListAsync();
            return _mapper.Map<List<ResultSendBoxMessageDto>>(values);
        }

        public async Task<int> GetTotalMessageCount()
        {
            var value = await _messageContext.UserMessages.CountAsync();
            return value;
        }

        public async Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            var value = _mapper.Map<UserMessage>(updateMessageDto);
            _messageContext.Update(value);
            await _messageContext.SaveChangesAsync();
        }
    }
}
