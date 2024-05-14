using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Handlers;

public class CreateHandler : IRequestHandler<CreateCommand>
{
     private readonly INewsRepository newsRepository;

    public CreateHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.News);

        await this.newsRepository.CreateAsync(request.News);
    }

}
