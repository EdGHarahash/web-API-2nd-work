
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateForum.Entities;
using PrivateForum.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Invite> Invites { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Forum");
        //}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserTag>().HasKey(sc => new { sc.ApplicationUserId, sc.TagId});
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
        //    builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

        //    // shadow properties
        //    builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
        //    builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

        //    base.OnModelCreating(builder);
        //}

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges();

        //    updateUpdatedProperty<SourceInfo>();
        //    updateUpdatedProperty<DataEventRecord>();

        //    return base.SaveChanges();
        //}

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        //public static ApplicationContext Create()
        //{
        //    return new ApplicationContext();
        //}
    }
}
