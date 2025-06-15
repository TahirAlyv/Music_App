using MusicService.Dtos;

namespace MusicService.Services.Interface
{
    public interface IPlaylistService
    {
        Task<PlaylistDto> CreatePlaylistAsync(string playlistName, string userId);
        Task<bool> AddMusicToPlaylistAsync(int playlistId, int musicId);
        Task<bool> RemoveMusicFromPlaylistAsync(int playlistId, int musicId);
        Task<List<PlaylistAndItemDto>> GetPlaylistsByUserIdAsync(string userId);
        Task<IEnumerable<MusicDto>> GetMusicsInPlaylistAsync(int playlistId);
        Task<bool> DeletePlaylistAsync(int playlistId);
        Task<string> ChangePlaylistName(int id, string newName);
    }
}
