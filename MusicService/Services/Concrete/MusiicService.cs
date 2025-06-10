using MusicService.Data;
using MusicService.Dtos;
using MusicService.Models;
using MusicService.Repository.Interface;
using MusicService.Services.Interface;

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

        public async Task<bool> UploadMusicAsync(MusicUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return false;

            string uploadsFolder = Path.Combine(_env.ContentRootPath, "MusicFiles");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, dto.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var music = new Music
            {
                Title = dto.Title,
                FilePath = filePath,
                UploadedByUserId = dto.UserId
            };

            await _repository.AddAsync(music);


            return await _repository.SaveChangesAsync();
        }
    }

}
