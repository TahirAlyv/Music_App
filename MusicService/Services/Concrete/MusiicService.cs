using MusicService.Data;
using MusicService.Dtos;
using MusicService.Models;
using MusicService.Repository.Interface;
using MusicService.Services.Interface;
using System.Linq.Expressions;

namespace MusicService.Services.Concrete
{
    public class MusiicService : IMusicService
    {
        private readonly IBaseRepository<Music> _repository;
        private readonly IWebHostEnvironment _env;

        public MusiicService(IBaseRepository<Music> repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<List<MusicDto>> GetAllAsync()
        {
            var musicList = await _repository.GetAllAsync();

            return musicList.Select(m => new MusicDto
            {
                Id = m.Id,
                Title = m.Title,
                Artist = m.Artist,
                Album = m.Album,
                Genre = m.Genre,
                FilePath = m.FilePath,
                CoverImagePath = m.CoverImagePath
            }).ToList();
        }

        public async Task<List<MusicDto>> GetByIdAsync(List<int> musicsid)
        {
            var musics = await _repository.GetManyAsync(m => musicsid.Contains(m.Id));

            var musicDtos = musics.Select(m => new MusicDto
            {
                Album = m.Album,
                Artist = m.Artist,
                CoverImagePath = m.CoverImagePath,
                FilePath = m.FilePath,
                Genre = m.Genre,
                Id = m.Id,
                Title = m.Title
            }).ToList();

            return musicDtos;
        }

        public async Task<bool> UploadMusicAsync(MusicUploadDto dto, string userId)
        {
            if (dto.File == null || dto.File.Length == 0)
                return false;

            string uploadsFolder = "/app/music_files";
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

 
            string musicFileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
            string musicPath = Path.Combine(uploadsFolder, musicFileName);
            using (var musicStream = new FileStream(musicPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(musicStream);
            }

 
            string? coverUrl = null;
            if (dto.Cover != null && dto.Cover.Length > 0)
            {
                string coverFileName = Guid.NewGuid() + Path.GetExtension(dto.Cover.FileName);
                string coverPath = Path.Combine(uploadsFolder, coverFileName);
                using (var coverStream = new FileStream(coverPath, FileMode.Create))
                {
                    await dto.Cover.CopyToAsync(coverStream);
                }
                coverUrl = "/files/" + coverFileName;
            }

            var music = new Music
            {
                Title = dto.Title,
                FilePath = "/files/" + musicFileName,
                CoverImagePath = coverUrl,
                UploadedByUserId = userId,
                Artist = dto.Artist,
                Album = dto.Album,
                Genre = dto.Genre
            };

            await _repository.AddAsync(music);
            return await _repository.SaveChangesAsync();
        }


    }

}
