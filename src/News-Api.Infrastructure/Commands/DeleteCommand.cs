using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Commands;

public class DeleteCommand : IRequest
{
    public int? Id { get; set; }

    public DeleteCommand(int? id) => this.Id = id;

    public DeleteCommand() { }

}
