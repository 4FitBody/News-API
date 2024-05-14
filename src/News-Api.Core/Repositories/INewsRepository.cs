using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_Api.Core.Models;

namespace News_Api.Core.Repositories;

public interface INewsRepository
{
    Task<IEnumerable<News>?> GetAllAsync();
    Task CreateAsync(News news);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, News news);
    Task<News> GetByIdAsync(int id);
}
