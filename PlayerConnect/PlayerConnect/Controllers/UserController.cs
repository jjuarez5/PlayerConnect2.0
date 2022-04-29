using Microsoft.AspNetCore.Mvc;

namespace PlayerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ICosmosDbClient _cosmosDbClient;
        public UserController(ICosmosDbClient cosmosDbClient)
        {
            _cosmosDbClient = cosmosDbClient;
        }

        [HttpPost]
       public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            ActionResult result = BadRequest();

            if (!user.IsValid())
            {
                return result;
            }
            else
            {
                // Create the new user
                result = await this._cosmosDbClient.CreateUserAsync(user).ConfigureAwait(false);
            }

            return Ok(result);
        }
    }
}
