using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Handlers;

public class DeleteHandler : IRequestHandler<DeleteCommand>
{
    private readonly INewsRepository newsRepository;

    public DeleteHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        await this.newsRepository.DeleteAsync((int)request.Id);
    }
}
