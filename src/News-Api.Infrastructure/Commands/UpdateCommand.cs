using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Models;

namespace News_Api.Infrastructure.Commands;

public class UpdateCommand : IRequest
{
    public int? Id { get; set; }
    public News? News { get; set; }

    public UpdateCommand(int? id, News? news)
    {
        this.Id = id;

        this.News = news;
    }

    public UpdateCommand() { }
}
