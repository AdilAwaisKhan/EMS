using Microsoft.EntityFrameworkCore;

namespace NewApp;

public class EMSContext(DbContextOptions<EMSContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
}
