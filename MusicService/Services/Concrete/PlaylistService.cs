 
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

        public PlaylistService(IBaseRepository<Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> AddMusicToPlaylistAsync(int playlistId, int musicId)
        {
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            var music = await _playlistRepository.GetByIdAsync(musicId);
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

        public Task<bool> CreatePlaylistAsync(string playlistName, string userId)
        {

            var playlist = new Playlist
            {
                Name = playlistName,
                UserId = userId,
            };

            _playlistRepository.AddAsync(playlist);
            return _playlistRepository.SaveChangesAsync();

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
                    Title = pi.Music.Title,
                    FilePath = pi.Music.FilePath,
                    UploadedByUserId = pi.Music.UploadedByUserId
                });

            return filtered;
        }

        public async Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(string userId)
        {
            var playlist = await _playlistRepository.GetAllWithIncludeAsync(p => p.Items);

            if (playlist == null)
                return null!;

            var newPlaylist = playlist.Where(pl => pl.UserId == userId)
                    .Select(p => new PlaylistDto
                    {
                        Name = p.Name,
                        Items = p.Items
                    });

            return newPlaylist;


        }

        public async Task<bool> RemoveMusicFromPlaylistAsync(int playlistId, int musicId)
        {
            var itemToRemove = await _playlistItemRepository.GetFirstOrDefaultAsync(
                 pi => pi.PlaylistId == playlistId && pi.MusicId == musicId);

            if (itemToRemove == null)
                return false;

            await _playlistItemRepository.DeleteAsync(itemToRemove);
            return await _playlistItemRepository.SaveChangesAsync();

        }
    }
}
