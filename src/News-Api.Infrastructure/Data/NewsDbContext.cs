using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News_Api.Core.Models;

namespace News_Api.Infrastructure.Data;

public class NewsDbContext : DbContext
{
    public DbSet<News> News { get; set; }

    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }
}
