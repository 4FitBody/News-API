using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Models;
using News_Api.Core.Repositories;
using News_Api.Infrastructure.Queries;

namespace News_Api.Infrastructure.Handlers;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<News>>
{
     private readonly INewsRepository newsRepository;

    public GetAllHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task<IEnumerable<News>>? Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var news = await this.newsRepository.GetAllAsync();

        if (news is null)
        {
            return Enumerable.Empty<News>();
        }

        return news;
    }

}
