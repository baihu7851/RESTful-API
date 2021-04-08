using Microsoft.EntityFrameworkCore;

namespace RESTfulAPI.Model.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role> RoleUser { get; set; }
    }
}