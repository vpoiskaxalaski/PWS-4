using System.Data.Entity;

namespace PIS_MVC5_1.Models
{
    public class PhoneContext : DbContext
    {
        public PhoneContext() : base("connectionString") {}

        public DbSet<User> Users { get; set; }
    }
}