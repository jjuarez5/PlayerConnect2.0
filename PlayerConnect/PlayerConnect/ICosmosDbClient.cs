using Microsoft.AspNetCore.Mvc;

namespace PlayerConnect
{
    public interface ICosmosDbClient
    {
        public Task<ActionResult> CreateUserAsync(User user);
        public Task<User> GetUserAsync(string id);
        public Task<List<User>> GetAllUsersAsync();
    }
}