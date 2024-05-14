using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Models;

namespace News_Api.Infrastructure.Queries;

public class GetAllQuery : IRequest<IEnumerable<News>>
{

}
