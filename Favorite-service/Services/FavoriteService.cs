using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace Favorite_service.Services
{
    public class FavoriteService
    {
        private readonly StackExchange.Redis.IDatabase _redisDb;

        public FavoriteService()
        {
            var muxer = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { { "redis-10616.c17.us-east-1-4.ec2.redns.redis-cloud.com", 10616 } },
                User = "default",
                Password = "eLre9e00rjdTM4cDCYjSEajsaIiyTWbC"
            });

            _redisDb = muxer.GetDatabase();
        }

        public async Task AddToFavoritesAsync(string userId, int musicId)
        {
            string key = userId;
            await _redisDb.SetAddAsync(key, musicId);
        }

        public async Task RemoveFromFavoritesAsync(string userId, int musicId)
        {
            string key = userId;
            await _redisDb.SetRemoveAsync(key, musicId);
        }

        public async Task<List<int>> GetFavoritesAsync(string userId)
        {
            string key = userId;
            var musicIds = await _redisDb.SetMembersAsync(key);
            return musicIds.Select(x => (int)x).ToList();
        }
    }
}
