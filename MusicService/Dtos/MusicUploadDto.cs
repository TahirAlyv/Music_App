namespace MusicService.Dtos
{
    public class MusicUploadDto
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }        
        public IFormFile? Cover { get; set; }   
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
    }

}
