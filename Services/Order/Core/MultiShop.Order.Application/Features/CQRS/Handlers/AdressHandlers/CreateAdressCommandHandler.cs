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
    public class CreateAdressCommandHandler
    {
        private readonly IRepository<Adress> _repository;

        public CreateAdressCommandHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateAdressCommand createAdressCommand)
        {
            await _repository.CreateAsync(new Adress
            {
                City = createAdressCommand.City,
                Detail1 = createAdressCommand.Detail1,
                District = createAdressCommand.District,
                UserId = createAdressCommand.UserId,
                Country = createAdressCommand.Country,
                Description = createAdressCommand.Description,
                Detail2 = createAdressCommand.Detail2,
                Email = createAdressCommand.Email,
                Name = createAdressCommand.Name,
                Phone = createAdressCommand.Phone,
                Surname = createAdressCommand.Surname,
                ZipCode = createAdressCommand.ZipCode
            });
        }
    }
}
