using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using News_Api.Core.Repositories;
using News_Api.Infrastructure.Commands;

namespace News_Api.Infrastructure.Handlers;

public class UpdateHandler : IRequestHandler<UpdateCommand>
{
    private readonly INewsRepository newsRepository;

    public UpdateHandler(INewsRepository newsRepository) => this.newsRepository = newsRepository;

    public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        ArgumentNullException.ThrowIfNull(request.News);

        await this.newsRepository.UpdateAsync((int)request.Id, request.News);
    }
}
