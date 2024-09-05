
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
        public DbSet<ReputationMaster> ReputationMasters { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Name = "Fun" },
               new Category { CategoryId = 2, Name = "Math" },
               new Category { CategoryId = 3, Name = "Func" }
           );
            modelBuilder.Entity<ReputationMaster>().HasData(
               new ReputationMaster { ReputationMasterId = 1, MinPoints = 0, UptoPoints= 49, Badge = "Novice" },
               new ReputationMaster { ReputationMasterId = 2, MinPoints = 50, UptoPoints= 99, Badge = "Learner" },
               new ReputationMaster { ReputationMasterId = 3, MinPoints = 100, UptoPoints= 199, Badge = "Apprentice" },
               new ReputationMaster { ReputationMasterId = 4, MinPoints = 200, UptoPoints= 349, Badge = "Explorer" },
               new ReputationMaster { ReputationMasterId = 5, MinPoints = 350, UptoPoints= 499, Badge = "Adept" },
               new ReputationMaster { ReputationMasterId = 6, MinPoints = 500, UptoPoints= 749, Badge = "Skilled" },
               new ReputationMaster { ReputationMasterId = 7, MinPoints = 750, UptoPoints= 999, Badge = "Expert" },
               new ReputationMaster { ReputationMasterId = 8, MinPoints = 1000, UptoPoints= 1449, Badge = "Master" },
               new ReputationMaster { ReputationMasterId = 9, MinPoints = 1500, UptoPoints= 1999, Badge = "Veteran" },
               new ReputationMaster { ReputationMasterId = 10, MinPoints = 2000, UptoPoints= 2999, Badge = "Hero" },
               new ReputationMaster { ReputationMasterId = 11, MinPoints = 3000, UptoPoints= 4999, Badge = "Champion" },
               new ReputationMaster { ReputationMasterId = 12, MinPoints = 4500, UptoPoints= 5999, Badge = "Centurion" },
               new ReputationMaster { ReputationMasterId = 13, MinPoints = 6000, UptoPoints= 7999, Badge = "Guardian" },
               new ReputationMaster { ReputationMasterId = 14, MinPoints = 8000, UptoPoints= 9999, Badge = "Sentinel" },
               new ReputationMaster { ReputationMasterId = 15, MinPoints = 10000, UptoPoints= 12499, Badge = "Commander" },
               new ReputationMaster { ReputationMasterId = 16, MinPoints = 12500, UptoPoints= 14999, Badge = "Legend" },
               new ReputationMaster { ReputationMasterId = 17, MinPoints = 15000, UptoPoints= 19999, Badge = "Sage" },
               new ReputationMaster { ReputationMasterId = 18, MinPoints = 20000, UptoPoints= 24999, Badge = "Archon" },
               new ReputationMaster { ReputationMasterId = 19, MinPoints = 25000, UptoPoints= 29999, Badge = "Paladin" },
               new ReputationMaster { ReputationMasterId = 20, MinPoints = 30000, UptoPoints= 39999, Badge = "Grandmaster" },
               new ReputationMaster { ReputationMasterId = 21, MinPoints = 40000, UptoPoints= 49999, Badge = "Overlord" },
               new ReputationMaster { ReputationMasterId = 22, MinPoints = 50000, UptoPoints= 100000, Badge = "Oracle" }


           );
        }
    }
}