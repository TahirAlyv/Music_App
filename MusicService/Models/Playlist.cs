namespace MusicService.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }  
        public List<PlaylistItem> Items { get; set; }
    }
}
