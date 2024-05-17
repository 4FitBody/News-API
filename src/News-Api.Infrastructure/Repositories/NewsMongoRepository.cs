using MongoDB.Driver;
using News_Api.Core.Models;
using News_Api.Core.Repositories;

namespace News_Api.Infrastructure.Repositories;

public class NewsMongoRepository : INewsRepository
{
    private readonly IMongoDatabase newsDb;
    private readonly IMongoCollection<News> mycollection;

    public NewsMongoRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);

        this.newsDb = client.GetDatabase(databaseName);

        this.mycollection = this.newsDb.GetCollection<News>(collectionName);
    }
   
    public async Task CreateAsync(News news)
    {
        var lastIndex = this.mycollection.Find(e => e.Id >= 0).ToList().Last().Id;

        news.Id = lastIndex + 1;

        await this.mycollection.InsertOneAsync(news);
    }

    public async Task<IEnumerable<News>?> GetAllAsync()
    {
        var news = await this.mycollection.FindAsync(n => n.IsApproved == true);

        var allNews = news.ToList();

        return allNews;
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
