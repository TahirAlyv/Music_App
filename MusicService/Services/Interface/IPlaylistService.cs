using MusicService.Dtos;

namespace MusicService.Services.Interface
{
    public interface IPlaylistService
    {
        Task<bool> CreatePlaylistAsync(string playlistName, string userId);
        Task<bool> AddMusicToPlaylistAsync(int playlistId, int musicId);
        Task<bool> RemoveMusicFromPlaylistAsync(int playlistId, int musicId);
        Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(string userId);
        Task<IEnumerable<MusicDto>> GetMusicsInPlaylistAsync(int playlistId);
        Task<bool> DeletePlaylistAsync(int playlistId);
    }
}
