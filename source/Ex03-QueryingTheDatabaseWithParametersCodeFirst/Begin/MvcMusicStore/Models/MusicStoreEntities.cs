using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }

        public MusicStoreEntities() 
        {
            Database.SetInitializer<MusicStoreEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Album>().ToTable("Album");
            base.OnModelCreating(modelBuilder);
        }
    }
}