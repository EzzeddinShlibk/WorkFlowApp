


using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WorkFlowApp.Models
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectsUser> ProjectsUsers { get; set; }
        public DbSet<Statues> Statuess { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<Profile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Comment>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Priority>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Project>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ProjectsUser>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<TeamUser>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<ProjectTask>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Statues>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Team>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Profile>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            modelBuilder.Entity<ProjectsUser>().HasKey(x => new { x.projectId, x.userId });
            modelBuilder.Entity<TeamUser>().HasKey(x => new { x.teamId, x.userId });


            modelBuilder.Entity<Project>()
       .HasMany(a => a.ProjectsUsers)
       .WithOne(b => b.project)
       .HasForeignKey(b => b.projectId);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }

            modelBuilder.Seed();


        }
    }
}

