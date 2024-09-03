
using Microsoft.EntityFrameworkCore;

namespace QOTD.Backend.Models
{
    public class AppdbContext:DbContext
    {
        public AppdbContext(DbContextOptions<AppdbContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<AnswerKey> AnswerKeys { get; set; }
        public DbSet<ReputationMaster> ReputationMaster { get; set; }
        public DbSet<UserResponse> UserResponse { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}