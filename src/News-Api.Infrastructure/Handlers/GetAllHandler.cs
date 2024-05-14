using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Handlers;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<News>>
{
     private readonly INewsRepository newsRepository;

    public GetAllHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task<IEnumerable<News>>? Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var news = this.newsRepository.GetAll();

        if (news is null)
        {
            return Enumerable.Empty<Exercise>();
        }

        return news;
    }

}
