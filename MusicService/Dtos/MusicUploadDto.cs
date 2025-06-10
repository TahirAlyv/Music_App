namespace MusicService.Dtos
{
    public class MusicUploadDto
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public string UserId { get; set; } 
    }
}
