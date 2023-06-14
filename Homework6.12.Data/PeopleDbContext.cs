using Microsoft.EntityFrameworkCore;

namespace Homework6._12.Data
{
    public class PeopleDbContext : DbContext
    {
        private string _connectionString;

        public PeopleDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Person> People { get; set; }
    }
}
