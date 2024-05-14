using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Repositories;

public class NewsSqlRepository : INewsRepository
{
    private readonly NewsDbContext dbContext;

    public NewsSqlRepository(NewsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<News>?> GetAllAsync()
    {
        var news = this.dbContext.News.AsEnumerable();

        return news;
    }

    public async Task CreateAsync(News news)
    {
        news.CreationDate = DateTime.UtcNow;

        await this.dbContext.News.AddAsync(news);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var newsToDelete = await this.dbContext.News.FirstOrDefaultAsync(news => news.Id == id);

        this.dbContext.Remove<News>(newsToDelete!);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, News news)
    {
        var oldNews = await this.dbContext.News.FirstOrDefaultAsync(e => e.Id == id);

#pragma warning disable CS8602
        oldNews.Title = news.Title;
#pragma warning restore CS8602
        oldNews.Description = news.Description;
        oldNews.CreationDate = DateTime.UtcNow;
        oldNews.IsApproved = news.IsApproved;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<News> GetByIdAsync(int id)
    {
        var searchedNews = await this.dbContext.News.FirstOrDefaultAsync(news => news.Id == id);

        return searchedNews!;
    }
}