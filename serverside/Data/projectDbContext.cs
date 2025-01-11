using System.Security.Cryptography;
using System.Text;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using serverside.Models.Domain;

namespace serverside.Data
{
    public class projectDbContext : DbContext
    {
        public projectDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) //pass it to the base
        {

        }

        //create the props according to the tables in db

        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Users
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

           modelBuilder.Entity<Posts>()
                .HasMany(p => p.Votes)
                .WithOne(v => v.Post)
                .HasForeignKey(v => v.PostId);


            modelBuilder.Entity<Posts>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for PostId.

            // Comments Configuration
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascading delete for Post.

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete for User.

            // Configure Votes
            modelBuilder.Entity<Votes>()
                .HasOne(v => v.Post)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascading delete for Post.

            modelBuilder.Entity<Votes>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete for User.

            // PostTags Configuration
            modelBuilder.Entity<PostTags>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTags>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTags>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            // Categories Configuration
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }

    }
}