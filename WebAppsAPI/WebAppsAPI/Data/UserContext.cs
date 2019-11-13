using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppsAPI.Models;

namespace WebAppsAPI.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne()
                .IsRequired()
                .HasForeignKey("UserId"); 

            builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(50);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne()
                .IsRequired()
                .HasForeignKey("PostId");

            builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(50);
            builder.Entity<Post>().Property(p => p.Text).IsRequired().HasMaxLength(200);


            builder.Entity<Comments>().Property(c => c.Text).IsRequired().HasMaxLength(200);

            //Another way to seed the database
            builder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Sebastien", LastName = "Wojtyla",Email="sebastienwojtyla@gmail.com",DateAdded = new DateTime(2018,2,20) },
                new User { Id = 2, FirstName = "Random", LastName = "Guy", Email = "randomGuy@gmail.com", DateAdded = new DateTime(2018,1,18) },
                new User { Id = 3, FirstName = "Web4", LastName = "Web4", Email = "Web4@gmail.com", DateAdded = new DateTime(2017, 1, 18) }
  );

            builder.Entity<Post>().HasData(
                   
                    new { Id = 1, Title = "Hello",Text = "This is my first post, finally it works!",DateAdded = new DateTime(2018,2,21),UserId = 1 },
                    new { Id = 2, Title = "Finally", Text = "This is my second post!", DateAdded = new DateTime(2018,2,22), UserId = 1 },
                    new { Id = 3, Title = "Random", Text = "Hey, I am a random guy!", DateAdded = new DateTime(2018,1,19), UserId = 2 },
                    new { Id = 4, Title = "Sun!!!", Text = "Finally a sunny day!!!", DateAdded = new DateTime(2018, 1, 19), UserId = 3 },
                    new { Id = 5, Title = "WOW", Text = "That thing knows my position! Privacy pls?", DateAdded = new DateTime(2018, 1, 19), UserId = 3 },
                    new { Id = 6, Title = "Goodies", Text = "New merch is out!", DateAdded = new DateTime(2018, 1, 19), UserId = 3 }
                    );
            builder.Entity<Comments>().HasData(
                    
                    new { Id = 1, Text = "First Comment!", DateAdded = new DateTime(2018,3,11), PostId = 1 },
                    new { Id = 2, Text = "Second comment", DateAdded = new DateTime(2018,3,14), PostId = 1 },
                    new { Id = 3, Text = "Random comment from a random guy", DateAdded = new DateTime(2018, 1, 20), PostId = 2 },
                    new { Id = 4, Text = "Privacy? Big Brother is watching you...", DateAdded = new DateTime(2018, 1, 20), PostId = 5 }
                  );
        }

        public DbSet<User> Users { get; set; }
    }
}

