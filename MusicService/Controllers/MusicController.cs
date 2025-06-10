using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicService.Dtos;
using MusicService.Services.Interface;

namespace MusicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = await _musicService.UploadMusicAsync(dto);
            if (!result) return BadRequest("Installation failed.");

            return Ok("Music successfully uploaded.");
        }


        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            return Ok("Test successful!");
        }

    }


}
