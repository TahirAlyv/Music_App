
namespace MusicService.Dtos
{
    public class MusicDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
        public string FilePath { get; set; }
        public string? CoverImagePath { get; set; }
        public TimeSpan? Duration { get; internal set; }
    }

}