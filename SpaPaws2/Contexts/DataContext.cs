using Microsoft.EntityFrameworkCore;
using SpaPaws2.Models.Enteties;

namespace SpaPaws2.Contexts;

internal class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Micke\Desktop\uc-ella\SpaPaws2\SpaPaws2\Contexts\SpaPaws_sql_db.mdf;Integrated Security=True;Connect Timeout=30";

    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }

    public DbSet<AnimalEntity> Animals { get; set; } = null!;
    public DbSet<BookingEntity> Bookings { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
   
}
