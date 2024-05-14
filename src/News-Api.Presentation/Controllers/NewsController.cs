using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using News_Api.Core.Models;
using News_Api.Infrastructure.Commands;
using News_Api.Infrastructure.Queries;
using News_Api.Infrastructure.Services;
using News_Api.Presentation.Models;
using News_Api.Presentation.Options;
using Newtonsoft.Json;

namespace News_Api.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class NewsController : ControllerBase
{
    private readonly ISender sender;
    private readonly BlobContainerService blobContainerService;

    public NewsController(ISender sender, IOptions<BlobOptions> blobOptions)
    {
        this.sender = sender;

        this.blobContainerService = new BlobContainerService(blobOptions.Value.Url, blobOptions.Value.ContainerName);
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var news = await this.sender.Send(getAllQuery);

        return base.Ok(news);
    }

    [HttpGet]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var news = await this.sender.Send(getByIdQuery);

        return base.Ok(news);
    }

    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> Create(object newsContentJson)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var newsContent = JsonConvert.DeserializeObject<NewsContent>(newsContentJson.ToString()!, settings);

        var imageFileName = newsContent!.ImageFileName;

        var imageFileData = newsContent.ImageFileContent;

        var rawPath = Guid.NewGuid().ToString() + imageFileName;

        var path = rawPath.Replace(" ", "%20");

        var news = newsContent!.News!;

        news.CreationDate = DateTime.Now;

        news.ImageUrl = "https://4fitbodystorage.blob.core.windows.net/images/" + path;

        await this.blobContainerService.UploadAsync(new MemoryStream(imageFileData!), rawPath);

        news.IsApproved = true;

        var createCommand = new CreateCommand(news);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [HttpDelete]
    [Route("/api/[controller]/[action]/{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> Delete(int? id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }


    [HttpPut]
    [Route("/api/[controller]/[action]/{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> Update(int? id, [FromBody] News news)
    {
        var createCommand = new UpdateCommand(id, news);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }
}
