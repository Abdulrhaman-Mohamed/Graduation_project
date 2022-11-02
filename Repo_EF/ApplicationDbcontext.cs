using Microsoft.EntityFrameworkCore;
using Repo_Core.Models;
using SecondEgSA.Model1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }
        
        public DbSet<Register> Registers { get; set; }
        
        public virtual DbSet<CoM_Param> CoM_Param { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<param_TB_type> param_TB_type { get; set; }
        public virtual DbSet<Param_Value> Param_Value { get; set; }
        
        public virtual DbSet<Sat_Station> Sat_Station { get; set; }
        public virtual DbSet<Satellite> Satellites { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Subsystem> Subsystems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Register>(d =>
            {
                d.HasKey(o => o.Id_Reg);
                d.Property(o => o.Email).IsRequired().HasMaxLength(60);
                d.Property(o => o.Password).IsRequired();
                
                



            });
           
            modelBuilder.Entity<CoM_Param>()
                .HasMany(e => e.Param_Value)
                .WithOne(e => e.CoM_Param)
                .HasForeignKey(e => new { e.parm_ID, e.com_id, e.sub_ID });

            modelBuilder.Entity<CoM_Param>()
                .HasKey(o => o.com_id);

            modelBuilder.Entity<CoM_Param>()
                .HasKey(o => o.sub_Id);


            modelBuilder.Entity<Command>()
                .HasKey(o => o.sub_ID);

            modelBuilder.Entity<Param_Value>()
                .HasKey(o=> new {o.com_id , o.parm_ID });

            modelBuilder.Entity<Param_Value>()
                .HasOne(o => o.CoM_Param)
                .WithMany(o => o.Param_Value)
                .HasPrincipalKey(e => new { e.param_ID, e.com_id, e.sub_Id });

            

            //modelBuilder.Entity<Command>()
            //    .Property(e => e.com_description)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Command>()
            //    .Property(e => e.sensor_name)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Command>()
            //    .HasMany(e => e.CoM_Param)
            //    .WithRequired(e => e.Command)
            //    .HasForeignKey(e => new { e.com_id, e.sub_Id })
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Command>()
            //    .HasMany(e => e.plan_)
            //    .WithOptional(e => e.Command)
            //    .HasForeignKey(e => new { e.com_ID, e.sub_ID });

            ////modelBuilder.Entity<param_TB_type>()
            ////    .Property(e => e.param_type)
            ////    .IsUnicode(false);

            ////modelBuilder.Entity<param_TB_type>()
            ////    .HasMany(e => e.CoM_Param)
            ////    .WithOptional(e => e.param_TB_type)
            ////    .HasForeignKey(e => e.param_type);

            //modelBuilder.Entity<Param_Value>()
            //    .Property(e => e.description)
            //    .IsUnicode(false);

           
            

            

            modelBuilder.Entity<Satellite>()
                .Property(e => e.Sat_name)
                .IsUnicode(false);

            modelBuilder.Entity<Satellite>()
                .Property(e => e.Orbit_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Station_name)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Station_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Longitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Station>()
                .Property(e => e.Latitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Station>()
                .HasMany(e => e.Sat_Station)
                .WithOne(e => e.Station)
                .HasForeignKey(e => e.Station_ID);

            modelBuilder.Entity<Subsystem>()
                .Property(e => e.Sub_name)
                .IsUnicode(false);

            modelBuilder.Entity<Subsystem>()
                .Property(e => e.Sub_type)
                .IsUnicode(false);


        }
    }
    
    
}

