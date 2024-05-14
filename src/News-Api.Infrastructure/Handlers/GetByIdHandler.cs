using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Models;
using News_Api.Core.Repositories;
using News_Api.Infrastructure.Queries;

namespace News_Api.Infrastructure.Handlers;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, News>
{
    private readonly INewsRepository newsRepository;

    public GetByIdHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task<News> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        var news = await this.newsRepository.GetByIdAsync((int)request.Id);

        if (news is null)
        {
            return new News();
        }

        return news!;
    }

}
