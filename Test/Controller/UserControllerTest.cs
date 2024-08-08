using API.Controllers;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Test.Controller
{
    public class UserControllerTest
    {
        private readonly IUserInterface _userInterface;
        private readonly UserController _userController;
        public UserControllerTest()
        {
            //set up dependences
            this._userInterface = A.Fake<IUserInterface>();

            //system under test SUT
            this._userController = new UserController(_userInterface);
        }
        private static User CreateFakeUser() => A.Fake<User>();
        private static List<User> CreateFakeUsers() => A.Fake<List<User>>();

        // Create 
        // this method returns create(success) | BadRequest(fail) Action result
        [Fact]
        public async void UserController_Create_ReturnCreate()
        {
            //Arrange
            var user = CreateFakeUser();

            //Act
            // This line sets up the behavior of the fake IUserInterface. It tells FakeItEasy to return true when the CreateAsync method is called with the user object. This simulates a successful creation of the user in the data store.
            A.CallTo(() => _userInterface.CreateAsync(user)).Returns(true);
            //This line calls the Create method of the UserController with the user object.The Create method is expected to return a CreatedAtActionResult if the user creation is successful
            var result = (CreatedAtActionResult)await _userController.Create(user);

            //Assert
            //This line checks that the status code of the result is 201, which corresponds to "Created". This indicates that the user was successfully created.
            result.StatusCode.Should().Be(201);
            //This line checks that the result is not null, ensuring that the Create method returned a valid CreatedAtActionResult.
            result.Should().NotBeNull();
        }


        // Read all
        // this method returns Ok(success) | NotFound(fail) action result
        [Fact]
        public async void UserController_GetUsers_ReturnOk()
        {
            //arrange
            //his line creates a fake list of User objects using FakeItEasy. This fake list will be used as the return value for the GetAllAsync method.
            var users = CreateFakeUsers();
            //This line adds a new User object to the fake list. The new user has the name "TestController" and email "Controller Email".
            users.Add(new User() { Name = "TestController", Email = "Controller Email" });

            //Act 
            //This line sets up the behavior of the fake IUserInterface. It tells FakeItEasy to return the users list when the GetAllAsync method is called. This simulates retrieving a list of users from a data store.
            A.CallTo(() => _userInterface.GetAllAsync()).Returns(users);
            //This line calls the GetUsers method of the UserController. The GetUsers method is expected to return an OkObjectResult containing the list of users. The result is awaited and cast to OkObjectResult.
            var result = (OkObjectResult)await _userController.GetUsers();

            // Assert
            // This line checks that the status code of the result is 200, which corresponds to "OK". This indicates that the request was successful and the list of users was retrieved correctly.
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            // This line checks that the result is not null, ensuring that the GetUsers method returned a valid OkObjectResult.
            result.Should().NotBeNull();
        }


        // Read single
        // This method returns Ok(success) | NotFound(fail) action result
        // This attribute indicates that the method is a parameterized test, allowing it to be run multiple times with different input data. It's provided by the xUnit testing framework.
        [Theory]
        //This attribute provides the test data for the parameterized test. In this case, it passes the value 1 as the id parameter to the test method.
        [InlineData(10)]
        public async void UserController_GetUser_ReturnOk(int id)
        {
            //Arrange
            var user = CreateFakeUser();
            user.Name = "TestController"; user.Email = "Controller Email"; user.Id = id;

            //Act
            //This line sets up the behavior of the fake IUserInterface. It tells FakeItEasy to return the user object when the GetById method is called with the id parameter (which is 1 in this case). This simulates retrieving a user by their ID from a data store.
            A.CallTo(()=> _userInterface.GetById(id)).Returns(user);
            var result = (OkObjectResult)await _userController.GetUser(id);

            //Assert
            //This line checks that the status code of the result is 200, which corresponds to "OK". This indicates that the request was successful and the list of users was retrieved correctly.
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            //This line checks that the result is not null, ensuring that the GetUsers method returned a valid OkObjectResult.
            result.Should().NotBeNull();

        }

        // Update
        // this method returns Ok(success) | NotFound(fail) action result
        [Fact]
        public async void UserController_Update_ReturnOk()
        {
            //arrange
            var user = CreateFakeUser();

            //Act
            //This line sets up the behavior of the fake IUserInterface. It tells FakeItEasy to return true when the UpdateAsync method is called with the user object. This simulates a successful update operation for the user in the data store.
            A.CallTo(() => _userInterface.UpdateAsync(user)).Returns(true);
            //This line calls the UpdateUser method of the UserController with the user object. The UpdateUser method is expected to return an OkResult if the update operation is successful. The result is awaited and cast to OkResult.
            var result = (OkObjectResult)await _userController.UpdateUser(user);

            //Assert
            //This line checks that the status code of the result is 201, which corresponds to "Created". This indicates that the user was successfully created.
            result.StatusCode.Should().Be(200);
            //This line checks that the result is not null, ensuring that the Create method returned a valid CreatedAtActionResult.
            result.Should().NotBeNull();

        }

        //Delete
        // this method returns NoContent(success) | NotFound(fail) action result
        [Fact]
        public async void UserController_Dlete_ReturnNoContent()
        {
            //Arrange
            int userId = 1;

            //Act
            //This line sets up the behavior of the fake IUserInterface. It tells FakeItEasy to return true when the DeleteAsync method is called with the userId parameter (which is 1 in this case). This simulates a successful deletion of the user from the data store.
            A.CallTo(() => _userInterface.DeleteAsync(userId)).Returns(true);
            // This line calls the DeleteUser method of the UserController with the userId parameter. The DeleteUser method is expected to return a NoContentResult if the deletion is successful. The result is awaited and cast to NoContentResult.
            var result = (NoContentResult)await _userController.DeleteUser(userId);

            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            result.Should().NotBeNull();

        }
    }
}
