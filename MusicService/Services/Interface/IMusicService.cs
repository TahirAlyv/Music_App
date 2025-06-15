using MusicService.Dtos;
using MusicService.Models;

namespace MusicService.Services.Interface
{
    public interface IMusicService
    {
        Task<bool> UploadMusicAsync(MusicUploadDto dto, string userId);
        Task<List<MusicDto>> GetAllAsync();
        Task<List<MusicDto>> GetByIdAsync(List<int> musicsid);
    }
}
