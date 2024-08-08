using API.Data;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Test.Repository
{
    public class RepositoryTest
    {
        private readonly UserRepo _userRepository;

        public RepositoryTest()
        {
            var userDbContext = GetDatabaseContext().Result;
            _userRepository = new UserRepo(userDbContext);
        }

        private async Task<UserDbContext> GetDatabaseContext()
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string from configuration
            var connectionString = configuration.GetConnectionString("SqlConnectionForTest");

            // Configure DbContextOptions to use SQL Server
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Initialize DbContext
            var userDbContext = new UserDbContext(options);
            userDbContext.Database.EnsureCreated();

            // Seed initial data if necessary
            if (!await userDbContext.Users.AnyAsync())
            {
                userDbContext.Users.Add(new User
                {
                    Email = "Email@mail.com",
                    Name = "IUserRepo"
                });
                await userDbContext.SaveChangesAsync();
            }

            return userDbContext;
        }

        // Create
        // this method return bool(true) if success
        [Fact]
        public async void UserRepo_Create_ReturnTrue()
        {
            // Arrange
            var user = A.Fake<User>();

            // Act
            var result = await _userRepository.CreateAsync(user);

            // Assert
            result.Should().BeTrue();
        }

        // Read all
        // return list of users
        [Fact]
        public async void UserRepo_GetAllAsync_ReturnUsers()
        {
            // Arrange

            // Act
            var result = await _userRepository.GetAllAsync();

            // Assert
            result.Should().AllBeOfType<User>();
        }

        // Read single
        [Theory]
        [InlineData(4)]
        public async void UserRepo_GetById_ReturnUser(int id)
        {
            // Arrange

            // Act
            var result = await _userRepository.GetById(id);

            // Assert
            result.Should().BeOfType<User>();
        }

        // Update
        // return true if success
        [Theory]
        [InlineData(9)]
        public async void UserRepo_UpdateAsync_ReturnTrue(int id)
        {
            // Arrange

            // Act
            var user = await _userRepository.GetById(id);
            user.Name = "Ali";
            var result = await _userRepository.UpdateAsync(user);

            // Assert
            result.Should().BeTrue();
        }

        // Delete
        // this method return true if success
        [Fact]
        public async void UserRepo_DeleteAsync_ReturnTrue()
        {
            // Arrange
            int id = 11;

            // Act

            var result = await _userRepository.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
        }
    }
}
