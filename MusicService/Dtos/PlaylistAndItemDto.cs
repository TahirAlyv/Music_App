
namespace MusicService.Dtos
{
    public class PlaylistAndItemDto
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string UserId { get; set; }
        public List<MusicDto> Songs { get; internal set; }
    }
}