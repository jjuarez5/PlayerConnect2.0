using Microsoft.AspNetCore.Mvc;

namespace PlayerConnect
{
    public interface ICosmosDbClient
    {
        public Task<ActionResult> CreateUserAsync(User user);
    }
}