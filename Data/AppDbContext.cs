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
        public DbSet<Post_Photo> Post_Photo { get; set; }
        public DbSet<Post_Tag> Post_Tag { get; set; }
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
            modelBuilder.Entity<Post>()
                .Ignore(p => p.Tags);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Photos) // 一个帖子可以有多个照片
                .WithOne() // 每个照片只能属于一个帖子
                .HasForeignKey(photo => photo.Post_Id); // 指定照片实体的外键属性
            modelBuilder.Entity<Post_Tag>()
                .HasKey(pt => new { pt.Post_Id, pt.Tag_Id });
            // modelBuilder.Entity<Post_Tag>()
            //     .Property(pt => pt.Post_Id)
            //     .HasColumnName("Post_Id");
            // modelBuilder.Entity<Post_Tag>()
            //     .Property(pt => pt.Tag_Id)
            //     .HasColumnName("Tag_Id");
            // modelBuilder.Entity<Post_Photo>()
            //     .Property(pt => pt.Post_Id)
            //     .HasColumnName("Post_Id");
        }
    }
}