using Microsoft.EntityFrameworkCore;
using WebAPI.Dapper.Models;
namespace WebAPI.Dapper.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
