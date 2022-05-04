using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace PlayerConnect
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private Container _container;

        public CosmosDbClient(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        // Creates a new user
        public async Task<ActionResult> CreateUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return new BadRequestResult();
            }

            user.Id = Guid.NewGuid().ToString();

            try
            {
                await this._container.CreateItemAsync<User>(user, new PartitionKey(user.Id)).ConfigureAwait(false);
            }
            catch (CosmosException)
            {

                throw;
            }

            return new OkResult();
            
        }

        // Get a user
        public async Task<User> GetUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                User user = await this._container.ReadItemAsync<User>(id, new PartitionKey(id)).ConfigureAwait(false);
                return user;
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

        private async IAsyncEnumerable<User> GetAllUsersForList()
        {
            var iterator = _container.GetItemQueryIterator<User>();

            while (iterator.HasMoreResults)
                foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                {
                    yield return item;
                }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new();

            await foreach(var item in GetAllUsersForList())
            {
                users.Add(item);
            }

            return users;
        }

    }
}
