using CandidateApplicationManager.Core;
using CandidateApplicationManager.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CandidateApplicationManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Container _userContainer;
        private readonly IConfiguration _configuration;
        public UserRepository(CosmosClient cosmosClient, IConfiguration configuration1)
        {
            this._configuration = configuration1;

            string? databaseName = configuration1["CosmosDbSetting:DatabaseName"];
            string? userContainerName = "Users";
            _userContainer = cosmosClient.GetContainer(databaseName, userContainerName);

        }

        public async Task<GeneralUser> CreateUserAsync(GeneralUser user)
        {
            ItemResponse<GeneralUser>? response = await _userContainer.CreateItemAsync(user);
            return response.Resource;
        }

        public async Task<GeneralUser> UpdateUserAsync(GeneralUser user)
        {
            ItemResponse<GeneralUser> response = await _userContainer.ReplaceItemAsync(user, user.Id.ToString());
            return response.Resource;
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _userContainer.DeleteItemAsync<GeneralUser>(userId, new PartitionKey(userId));
        }

        public async Task<GeneralUser> GetUserByIdAsync(string userId)
        {
            QueryDefinition query = _userContainer.GetItemLinqQueryable<GeneralUser>().
                Where(r => r.UserId.Equals(userId)).Take(1).ToQueryDefinition();

            string? sqlQuery = query.QueryText;

            FeedResponse<GeneralUser> response = await _userContainer.GetItemQueryIterator<GeneralUser>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<IEnumerable<GeneralUser>> GetAllUsersAsync()
        {
            FeedIterator<GeneralUser>? query = _userContainer.GetItemLinqQueryable<GeneralUser>().ToFeedIterator();

            List<GeneralUser> users = new List<GeneralUser>();
            while (query.HasMoreResults) 
            {
                FeedResponse<GeneralUser> response = await query.ReadNextAsync();
                users.AddRange(response);
            }

            return users;

        }
    }
}
