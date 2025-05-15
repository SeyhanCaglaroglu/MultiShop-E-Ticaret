using AutoMapper;
using MultiShop.Message.DAL.Entities;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserMessage,ResultMessageDto>().ReverseMap();
            CreateMap<UserMessage,ResultInBoxMessageDto>().ReverseMap();
            CreateMap<UserMessage,ResultSendBoxMessageDto>().ReverseMap();
            CreateMap<UserMessage,CreateMessageDto>().ReverseMap();
            CreateMap<UserMessage,UpdateMessageDto>().ReverseMap();
            CreateMap<UserMessage,GetByIdMessageDto>().ReverseMap();
        }
    }
}
