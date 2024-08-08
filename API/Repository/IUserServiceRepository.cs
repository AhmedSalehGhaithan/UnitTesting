using API.Models;

namespace API.Repository
{
    public interface IUserServiceRepository
    {
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
