using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

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

        [HttpGet]
        [Route("{userId}")]
        public async Task<User> GetUserAsync(string userId)
        {
            try
            {
                var result = await this._cosmosDbClient.GetUserAsync(userId).ConfigureAwait(false);
                return result;
            }
            catch (CosmosException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUsersAsync()
        {
            List<User> users = new List<User>();
            try
            {
                users = await this._cosmosDbClient.GetAllUsersAsync();
            }
            catch (CosmosException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return new JsonResult(users);
        }
    }
}
