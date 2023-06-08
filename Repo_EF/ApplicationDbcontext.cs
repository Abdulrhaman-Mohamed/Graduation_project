using Repo_Core.Identity_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repo_Core.Models;
using Repo_EF.Repo_Method;

namespace Repo_EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<CommandParam> CommandParams { get; set; }
        public virtual DbSet<ParamType> ParamTypes { get; set; }
    
        public virtual DbSet<Acknowledge> Acknowledges { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanResult> PlanResults { get; set; }
        public virtual DbSet<Satellite> Satellites { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<SubSystem> Subsystems { get; set; }

        //Identity
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<RoverImage> RoverImages { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Station 
            modelBuilder.Entity<Station>().HasKey(c => new { c.Id });

            // SubSystem
            modelBuilder.Entity<SubSystem>().HasKey(c => new { c.Id });

            // Command 
            modelBuilder.Entity<Command>()
                .HasKey(c => new { c.Id, c.SubSystemId });

            // Plan 
            modelBuilder.Entity<Plan>()
                .HasKey(c => new { c.Id, c.SequenceNumber });

            modelBuilder.Entity<Plan>().HasOne(o => o.Command)
                .WithMany(o => o.Plans)
                .HasForeignKey(o => new { commandID = o.CommandId, o.SubSystemId });

            modelBuilder.Entity<Plan>().HasIndex(x => x.FlagWatting)
                .HasFilter("[FlagWatting] IS False");

            // PlanResult 
            modelBuilder.Entity<PlanResult>()
                .HasKey(c => new { c.Id, c.PlanId, c.PlanSequenceNumber });


            // RoverImage
            modelBuilder.Entity<RoverImage>()
                .HasOne(c => c.PlanResult)
                .WithMany(p => p.RoverImages)
                .HasForeignKey(o => new { o.PlanResultId, o.PlanSequenceNumber, o.PlanId });

            // Acknowledge
            modelBuilder.Entity<Acknowledge>().HasKey(c => new { c.Id });

            //CommandParam
            modelBuilder.Entity<CommandParam>().HasOne(c => c.Command)
               .WithMany(c => c.CommandParams)
               .HasForeignKey(c => new { c.CommandId, c.SubSystemId });

            modelBuilder.Entity<CommandParam>()
                .HasKey(c => new { c.Id, c.CommandId, c.SubSystemId });




            // Identity

            //feedback
            modelBuilder.Entity<Feedback>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Feedback>()
                .HasOne(o => o.User)
                .WithMany(o => o.Feedbacks);

            modelBuilder.Entity<Feedback>()
                .HasOne(o => o.Post)
                .WithMany(o => o.feedback);

            //posts
            modelBuilder.Entity<Posts>()
                .HasOne(o => o.User)
                .WithMany(o => o.Posts);


            modelBuilder.Entity<Posts>()
                .HasMany(o => o.feedback)
                .WithOne(o => o.Post);

            modelBuilder.Entity<Plan>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(o => o.Plans)
                .HasForeignKey(o => o.ApplicationUserid);


            //Images
            modelBuilder.Entity<Images>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Images>()
                .HasOne(o => o.Posts)
                .WithMany(o => o.Images);
        }
    }
}




