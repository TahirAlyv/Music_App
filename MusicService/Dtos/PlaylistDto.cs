using MusicService.Models;

namespace MusicService.Dtos
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public List<PlaylistItem> Items { get; set; }
    }
}