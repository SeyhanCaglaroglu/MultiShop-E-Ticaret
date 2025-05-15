using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var value = await _repository.GetByIdAsync(updateOrderDetailCommand.OrderDetailId);

            value.OrderDetailId = updateOrderDetailCommand.OrderDetailId;
            value.OrderingId = updateOrderDetailCommand.OrderingId;
            value.ProductId = updateOrderDetailCommand.ProductId;
            value.ProductName = updateOrderDetailCommand.ProductName;
            value.ProductPrice = updateOrderDetailCommand.ProductPrice;
            await _repository.UpdateAsync(value);
        }
    }
}
