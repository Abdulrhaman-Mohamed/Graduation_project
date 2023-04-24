using Repo_Core.Identity_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repo_Core.Models;


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
        public virtual DbSet<ParamValue> ParamValues { get; set; }
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

            // Acknowledge
            modelBuilder.Entity<Acknowledge>().HasKey(c => new { c.Id });

            //CommandParam
            modelBuilder.Entity<CommandParam>().HasOne(c => c.Command)
               .WithMany(c => c.CommandParams)
               .HasForeignKey(c => new { c.CommandId, c.SubSystemId });

            modelBuilder.Entity<CommandParam>()
                .HasKey(c => new { c.Id, c.CommandId, c.SubSystemId });

            // ParamType
            modelBuilder.Entity<ParamType>().HasKey(c => new { c.Id });
            // ParamValue
            modelBuilder.Entity<ParamValue>().HasOne(c => c.CommandParam)
                .WithMany(c => c.ParamValues)
                .HasForeignKey(c => new { c.CommandParamID, c.CommandID, c.SubSystemID });

            modelBuilder.Entity<ParamValue>().HasKey(c => new { c.Id, c.SubSystemID, c.CommandID, c.CommandParamID });


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

            modelBuilder.Entity<Plan>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(o => o.Plans);


            //Images
            modelBuilder.Entity<Images>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Images>()
                .HasOne(o => o.Posts)
                .WithMany(o => o.Images);
        }
    }
}




