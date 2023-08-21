using System;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions options) : base(options)
        {
        }

        // EntityFrameworkCore will use the name "BlogPosts" to create the table inside of the database.
        public DbSet<BlogPost> BlogPosts { get; set; }

        //EntityFrameworkCore will use the name "Tags" to create the table inside of the database.
        public DbSet<Tag> Tags { get; set; }

        //After EntityFrameworkCore runs migrations, we will have two base tables, "BlogPosts" and "Tags".
        //We will also have one additional table for the relationship between the "Tags" Table and the "BlogPosts" table.
        //One BlogPost can have many Tags and vice versa. It is a 'many-to-many' relationship.
    }
}

