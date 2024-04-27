using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using uowpublic.Models;

namespace uowpublic.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyPhoto> PropertyPhoto { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostPhoto> PostPhoto { get; set; }
        public DbSet<PostTag> PostTag { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Course> Course { get; set; }
        // DbSet...

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasNoKey();
        }
    }
}