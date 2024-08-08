using API.Models;
using API.Services;
using FakeItEasy;
using FluentAssertions;
using Test.Helpers;

namespace Test.Service
{
    public class UserServiceTest
    {

        private static User CreateFakeUser() => A.Fake<User>();
        // Create
        [Fact]
        public async void UserService_CreateUser_ReturnTrue()
        {
            // Arrange
            var user = CreateFakeUser();

            // Act 
            var client = CustomFakeHttpClient.FakeHttpClient<bool>(true);
            var userService = new UserService(client);
            var result = await userService.CreateAsync(user);
            //The Dispose method is part of the IDisposable interface in .NET. When an object implements IDisposable, it means that the object holds unmanaged resources (like file handles, network connections, or memory) that need to be explicitly released when the object is no longer needed.
            client.Dispose();

            // Assert
            result.Should().BeTrue();
        }

        // GetAll
        [Fact]
        public async void UserService_GetUsers_ReturnUsers()
        {
            // Arrange
            var users = A.Fake<List<User>>();
            users.Add(new User { Email = "Email@mail.com", Name = "Unkown", Id = 1 });
            var client = CustomFakeHttpClient.FakeHttpClient<List<User>>(users);
            var userService = new UserService(client);

            // Act
            var result = await userService.GetAllAsync();
            client.Dispose();

            // Assert
            result.Should().AllBeOfType<User>();
            result.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(1)]
        public async void UserService_GetUser_ReturnUser(int id)
        {
            // Arrange
            var user = CreateFakeUser();
            user.Email = "Email@mail.com";
            user.Name = "New"; user.Id = id;

            // Act
            var client = CustomFakeHttpClient.FakeHttpClient<User>(user);
            var userService = new UserService(client);
            var result = await userService.GetById(id);
            client.Dispose();

            // Assert
            result.Should().BeOfType<User>();
            result.Should().NotBeNull();
        }

        // Update
        [Fact]
        public async void UserServices_UpdateUser_ReturnTrue()
        {
            // Arrange
            var user = CreateFakeUser();
            user.Email = " Email@mail.com"; user.Name = "newGet";user.Id = 1;

            // Act 
            var client = CustomFakeHttpClient.FakeHttpClient<bool>(true);
            var userService = new UserService(client);
            var result = await userService.UpdateAsync(user);
            client.Dispose();

            // Assert
            result.Should().BeTrue();
        }

        // Delete
        [Fact]
        public async void UserService_DeleteUser_returnTrue()
        {
            // Arrange
            int userId = 1;

            // Fact
            var client = CustomFakeHttpClient.FakeHttpClient<bool>(true);
            var userService = new UserService(client);
            var result = await userService.DeleteAsync(userId);

            // Assert
            result.Should().BeTrue();

        }
    }
}
