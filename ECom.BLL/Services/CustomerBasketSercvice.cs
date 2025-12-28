using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace ECom.BLL.Services
{
    public class CustomerBasketSercvice : ICustomerBasketSercvice
    {
        private readonly IDatabase _database;

        public CustomerBasketSercvice(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomBasket?> GetBasketAsync(string basketId)
        {
            var result = await _database.StringGetAsync(basketId);

            if (result.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomBasket>(result!);
        }

        public async Task<CustomBasket?> UpdateBasketAsync(CustomBasket basket)
        {
            var created = await _database.StringSetAsync(
                basket.Id,
                JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(3)
            );

            if (!created)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
