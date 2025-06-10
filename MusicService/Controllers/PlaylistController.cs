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
    public class PlaylistController : ControllerBase
    {

        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }



        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            return Ok("Test successful!");
        }



        [HttpPost("{playlistName}")]

        public async Task<IActionResult> CreatePlaylistAsync(string playListName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");

            var result = await _playlistService.CreatePlaylistAsync(playListName, userId);

            if (!result)
                return BadRequest("Failed to create playlist");

            return Ok("Playlist created successfully");
        }


        [HttpPost("musics")]
        public async Task<IActionResult> AddMusicToPlaylistAsync([FromBody] AddMusicDto dto)
        {
            var result = await _playlistService.AddMusicToPlaylistAsync(dto.PlaylistId, dto.MusicId);
            if (!result)
                return BadRequest("Failed to add music to playlist");
            return Ok("Music added to playlist successfully");
        }


        [HttpDelete("{playlistId}/musics/{musicId}")]

        public async Task<IActionResult> RemoveMusicFromPlaylist(int playlistId,int musicId)
        {
            var result = await _playlistService.RemoveMusicFromPlaylistAsync(playlistId, musicId);

            if (!result)
                return BadRequest("Failed to remove music from playlist");

            return Ok("Music removed from playlist successfully");
        }


        [HttpGet("mine")]

        public async Task<IActionResult> GetUserPlaylists()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");
            var playlists = await _playlistService.GetPlaylistsByUserIdAsync(userId);

            if (playlists == null || !playlists.Any())
                return NotFound("No playlists found for this user");

            return Ok(playlists);

        }

        [HttpGet("{playlistId}/musics")]
        public async Task<IActionResult> GetMusicsInPlaylistAsync(int playlistId)
        {
            var musics = await _playlistService.GetMusicsInPlaylistAsync(playlistId);
            if (musics == null || !musics.Any())
                return NotFound("No music found in this playlist");
            return Ok(musics);
        }


        [HttpDelete("{playlistId}")]

        public async Task<IActionResult> DeletePlaylistAsync(int playlistId)
        {
            var result = await _playlistService.DeletePlaylistAsync(playlistId);
            if (!result)
                return BadRequest("Failed to delete playlist");
            return Ok("Playlist deleted successfully");




        }
    }
}
