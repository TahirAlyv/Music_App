using Favorite_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Favorite_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly FavoriteService _favoriteService;

        public FavoriteController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("test")]

        public async Task<ActionResult> Test()
        {
            return Ok("Test successful Fav!");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites([FromQuery] int musicId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");
            await _favoriteService.AddToFavoritesAsync(userId, musicId);
            return Ok("Music added to favorites.");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromFavorites([FromQuery] int musicId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");
            await _favoriteService.RemoveFromFavoritesAsync(userId, musicId);
            return Ok("Music removed from favorites.");
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("UserId not found in token");
            var favorites = await _favoriteService.GetFavoritesAsync(userId);
            return Ok(favorites);
        }
    }
}
