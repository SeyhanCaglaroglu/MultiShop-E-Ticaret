using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class UpdateAdressCommandHandler
    {
        private readonly IRepository<Adress> _repository;

        public UpdateAdressCommandHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAdressCommand updateAdressCommand)
        {
            
            var value = await _repository.GetByIdAsync(updateAdressCommand.AdressId);
            value.AdressId = updateAdressCommand.AdressId;
            value.UserId = updateAdressCommand.UserId;
            value.Detail1 = updateAdressCommand.Detail;
            value.District = updateAdressCommand.District;
            value.City = updateAdressCommand.City;
            await _repository.UpdateAsync(value);
        }
    }
}
