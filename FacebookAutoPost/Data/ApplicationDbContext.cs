using FacebookAutoPost.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FacebookAutoPost.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<AutoPost> AutoPosts { get; set; }

        public DbSet<ParamsUri> ParamsUri { get; set; }

        public DbSet<Frequency> Frequency { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AutoPosts.db");
        }
    }
}
