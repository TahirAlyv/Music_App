using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicService.Dtos;
using MusicService.Services.Interface;
using System.Security.Claims;

namespace MusicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] MusicUploadDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");

            var result = await _musicService.UploadMusicAsync(dto, userId!);
            if (!result) return BadRequest("Installation failed.");

            return Ok("Music successfully uploaded.");
        }


        [HttpGet("musics")]
        public async Task<IActionResult> GetAllMusics()
        {
            try
            {
                var musics = await _musicService.GetAllAsync();

                if (musics == null || !musics.Any())
                    return NotFound("not found song.");

                return Ok(musics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"server error: {ex.Message}");
            }
        }
        [HttpGet("byids")]
        public async Task<IActionResult> GetMusicsByIds([FromQuery] List<int> musicIds)
        {
            if (musicIds == null || musicIds.Count == 0)
                return BadRequest("no song id.");

            try
            {
                var musics = await _musicService.GetByIdAsync(musicIds);

                if (musics == null || !musics.Any())
                    return NotFound("No music found.");

                return Ok(musics);
            }
            catch (Exception ex)
            {
 
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }





    }


}
