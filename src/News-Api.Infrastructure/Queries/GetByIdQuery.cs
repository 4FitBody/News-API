using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Models;

namespace News_Api.Infrastructure.Queries;

public class GetByIdQuery : IRequest<News>
{
    public int? Id { get; set; }

    public GetByIdQuery(int? id)
    {
        this.Id = id;
    }

    public GetByIdQuery() { }
}
