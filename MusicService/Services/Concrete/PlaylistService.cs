 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicService.Dtos;
using MusicService.Models;
using MusicService.Repository.Interface;
using MusicService.Services.Interface;

namespace MusicService.Services.Concrete
{
    public class PlaylistService : IPlaylistService
    {

        private readonly IBaseRepository<Playlist> _playlistRepository;
        private readonly IBaseRepository<PlaylistItem> _playlistItemRepository;
        private readonly IBaseRepository<Music> _musicRepository;

        public PlaylistService(IBaseRepository<Playlist> playlistRepository, IBaseRepository<PlaylistItem> playlistItemRepository, IBaseRepository<Music> musicRepository)
        {
            _playlistRepository = playlistRepository;
            _playlistItemRepository = playlistItemRepository;
            _musicRepository = musicRepository;
        }

        public async Task<bool> AddMusicToPlaylistAsync(int playlistId, int musicId)
        {
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            var music = await _musicRepository.GetByIdAsync(musicId);
            if (playlist == null || music == null)
                return false;


            var playlistItem = new PlaylistItem
            {
                PlaylistId = playlistId,
                MusicId = musicId
            };


            await _playlistItemRepository.AddAsync(playlistItem);
            return await _playlistItemRepository.SaveChangesAsync();
        }

        public async Task<PlaylistDto> CreatePlaylistAsync(string playlistName, string userId)
        {
            var playlist = new Playlist
            {
                Name = playlistName,
                UserId = userId,
            };

            await _playlistRepository.AddAsync(playlist);
            var result = await _playlistRepository.SaveChangesAsync();

            if (!result)
                return null!;

            return new PlaylistDto
            {
                Id = playlist.Id,
                Name = playlist.Name
            };
        }

        public async Task<bool> DeletePlaylistAsync(int playlistId)
        {
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            if (playlist == null)
                return false;

             _= _playlistRepository.DeleteAsync(playlist);
             return await _playlistRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MusicDto>> GetMusicsInPlaylistAsync(int playlistId)
        {
            var playlistItems = await _playlistItemRepository.GetAllWithIncludeByConditionAsync(
                                    pi => pi.PlaylistId == playlistId,
                                    pi => pi.Music);

            if (playlistItems == null)
                return Enumerable.Empty<MusicDto>();

             var filtered = playlistItems
                  .Select(pi => new MusicDto
                  {
                      Id = pi.Music.Id,
                      Title = pi.Music.Title,
                      Artist = pi.Music.Artist,
                      Album = pi.Music.Album,
                      Genre = pi.Music.Genre,
                      CoverImagePath = pi.Music.CoverImagePath,
                      FilePath = pi.Music.FilePath,
                  });

            return filtered;
        }

        public async Task<List<PlaylistAndItemDto>> GetPlaylistsByUserIdAsync(string userId)
        {
            var playlist = await _playlistRepository.GetPlaylistsWithItemsAndMusicAsync(userId);

            return playlist;


        }

        public async Task<bool> RemoveMusicFromPlaylistAsync(int playlistId, int musicId)
        {
            var itemToRemove = await _playlistItemRepository.GetFirstOrDefaultAsync(pi=> pi.PlaylistId==playlistId && pi.MusicId== musicId);

            if (itemToRemove == null)
                return false;

            await _playlistItemRepository.DeleteAsync(itemToRemove);
            return await _playlistItemRepository.SaveChangesAsync();

        }


        public async Task<string> ChangePlaylistName(int id,string newName)
        {
            var playlists = await _playlistRepository.GetByIdAsync(id);

            if (playlists == null)
                return "Playlist not found";

            playlists.Name = newName;

            await _playlistRepository.UpdateAsync(playlists);
            var result = await _playlistRepository.SaveChangesAsync();

            if (!result)
                return "Failed to update playlist name";

            return $"Playlist name updated successfully; {newName}";
        }
    }
}
