using Interlog.HRTool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Interlog.HRTool.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasMany(x => x.Profiles).WithMany(x => x.Employees).UsingEntity<EmployeeProfile>();

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(builder);
        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        
    }

    //TODO delete
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-KSAT8LT;Database=InterlogHRTool;User Id=sa;Password=developer;MultipleActiveResultSets=true");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }

} 
