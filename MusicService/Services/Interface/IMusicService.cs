using MusicService.Dtos;

namespace MusicService.Services.Interface
{
    public interface IMusicService
    {
        Task<bool> UploadMusicAsync(MusicUploadDto dto);
    }
}
