using Microsoft.EntityFrameworkCore;
using StudentApp.Api.Model;

namespace StudentApp.Api.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
    }
}