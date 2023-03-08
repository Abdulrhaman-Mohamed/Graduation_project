using Identity.Model;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Identity
{
    public class UserDbcontext : IdentityDbContext<ApplicationUser>
    {
        public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options)
        {

        }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            //feedback
            modelBuilder.Entity<Feedback>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Feedback>()
                .HasOne(o => o.User)
                .WithMany(o=> o.Feedbacks);

            modelBuilder.Entity<Feedback>()
                .HasOne(o => o.Post)
                .WithMany(o => o.feedback);


            //posts
            modelBuilder.Entity<Posts>()
                .HasOne(o => o.User)
                .WithMany(o => o.Posts);
                

        }
    }
}
