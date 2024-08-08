using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepo(UserDbContext context) : IUserInterface
    {
        public async Task<bool> CreateAsync(User user)
        {
            context.Users.Add(user);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var getUser = await context.Users.FirstOrDefaultAsync(_ => _.Id == id);
            if(getUser != null)
            {
                context.Users.Remove(getUser);
                var Result = await context.SaveChangesAsync();
                return Result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await context.Users.ToListAsync();

        public async Task<User> GetById(int id) => await context.Users.FirstOrDefaultAsync(_ => _.Id == id); 

        public async Task<bool> UpdateAsync(User user)
        {
            var getUser = await context.Users.FirstOrDefaultAsync(_ => _.Id == user.Id);
            if(getUser != null)
            {
                getUser.Name = user.Name;
                getUser.Email = user.Email;
                var result = await context.SaveChangesAsync();
                return result > 0;  
            }
            return false;
        }
    }
}
