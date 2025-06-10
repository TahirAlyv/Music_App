using Microsoft.EntityFrameworkCore;
using MusicService.Models;

namespace MusicService.Data
{
    public class AppDbContext:DbContext
    {
         
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(p => p.Playlist)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PlaylistItem>()
                .HasOne(p => p.Music)
                .WithMany()  
                .HasForeignKey(p => p.MusicId);
        }
    }
}
