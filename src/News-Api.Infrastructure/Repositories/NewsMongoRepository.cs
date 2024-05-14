

using MongoDB.Driver;
using News_Api.Core.Models;
using News_Api.Core.Repositories;

namespace News_Api.Infrastructure.Repositories;

public class NewsMongoRepository : INewsRepository
{
    private readonly IMongoDatabase newsDb;
    private readonly IMongoCollection<News> mycollection;

    public NewsMongoRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);

        this.newsDb = client.GetDatabase("NewsDb");

        this.mycollection = this.newsDb.GetCollection<News>("News");
    }

    public async Task CreateAsync(News news)
    {
        await this.mycollection.InsertOneAsync(news);
    }
    public async Task<IEnumerable<News>?> GetAllAsync()
    {
        var food = await this.mycollection.FindAsync(f => f.IsApproved == true);

        var allFood = food.ToList();

        return allFood;
    }

    public async Task<News> GetByIdAsync(int id)
    {
        var news = await this.mycollection.FindAsync(news => news.Id == id);

        var searchForNews = news.FirstOrDefault();

        return searchForNews;
    }
    public async Task DeleteAsync(int id)
    {
        await this.mycollection.FindOneAndDeleteAsync(news => news.Id == id);
    }


    public async Task UpdateAsync(int id, News newsToUpdate)
    {
        await this.mycollection.ReplaceOneAsync<News>(filter: n => n.Id == id, replacement: newsToUpdate);
    }
    public async Task ApproveAsync(int id)
    {
        var update = Builders<News>.Update.Set(news => news.IsApproved, false);

        var options = new FindOneAndUpdateOptions<News>();

        await this.mycollection.FindOneAndUpdateAsync<News>(news => news.Id == id, update, options);
    }
}
