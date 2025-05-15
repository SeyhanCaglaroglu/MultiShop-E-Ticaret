using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using MultiShop.Order.Presistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Presistence.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderContext _orderContext;

        public OrderingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public List<Ordering> GetOrderingsByUserId(string id)
        {
            var values = _orderContext.Orderings.Where(x=>x.UserId == id).ToList();
            return values;
        }
    }
}
