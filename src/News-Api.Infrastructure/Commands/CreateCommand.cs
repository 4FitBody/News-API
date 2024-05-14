using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using  News_Api.Core.Models;

using MediatR;

namespace News_Api.Infrastructure.Commands;

public class CreateCommand : IRequest
{
    public News? News { get; set; }

    public CreateCommand(News? news) => this.News = news;

    public CreateCommand() { }
}
