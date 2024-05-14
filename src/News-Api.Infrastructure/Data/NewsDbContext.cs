using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Data;

public class NewsDbContext : DbContext
{
    public DbSet<News> News { get; set; }

    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }
}
