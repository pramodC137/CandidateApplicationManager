using CandidateApplicationManager.Entities;

namespace CandidateApplicationManager.Core
{
    public interface IUserRepository
    {
        Task<GeneralUser> CreateUserAsync(GeneralUser user);
        Task<GeneralUser> UpdateUserAsync(GeneralUser user);
        Task DeleteUserAsync(string userId);
        Task<GeneralUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<GeneralUser>> GetAllUsersAsync();
    }
}
