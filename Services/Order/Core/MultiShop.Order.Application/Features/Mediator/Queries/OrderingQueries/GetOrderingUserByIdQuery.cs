using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingUserByIdQuery : IRequest<List<GetOrderingByUserIdQueryResult>>
    {
        public string Id { get; set; }

        public GetOrderingUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
