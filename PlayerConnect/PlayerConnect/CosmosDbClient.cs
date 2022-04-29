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
            catch (Exception)
            {

                throw;
            }

            return new OkResult();
            
        }
    }
}
