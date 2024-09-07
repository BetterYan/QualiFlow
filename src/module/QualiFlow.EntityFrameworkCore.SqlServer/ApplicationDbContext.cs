using Microsoft.EntityFrameworkCore;
using QualiFlow.Identity.Core.Entities;

namespace QualiFlow.EntityFrameworkCore.SqlServer;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}
