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
    public class RemoveAdressCommandHandler
    {
        private readonly IRepository<Adress> _repository;

        public RemoveAdressCommandHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveAdressCommand removeAdressCommand)
        {
            var value = await _repository.GetByIdAsync(removeAdressCommand.Id);
            await _repository.DeleteAsync(value);
        }
    }
}
