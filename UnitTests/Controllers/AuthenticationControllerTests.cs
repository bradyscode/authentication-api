using authentication_dot_net.Controllers;
using authentication_dot_net.Interfaces.UserInterface;
using authentication_dot_net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private Mock<ILogger<AuthenticationController>> _loggerMock;
        private Mock<IUserInterface> _userInterfaceMock;
        private AuthenticationController _controller;
        
        [SetUp]
        public void Setup()
        {
            //mocking the config
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Issuer","Issuer" },
                {"Jwt:Audience","Audience" },
                {"Jwt:Key","bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca" }
            };
            IConfiguration configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            //end mocking the config
            _loggerMock = new Mock<ILogger<AuthenticationController>>();
            _userInterfaceMock = new Mock<IUserInterface>();
            _controller = new AuthenticationController(_userInterfaceMock.Object, _loggerMock.Object, configurationMock);

        }


        //AuthenticateUser Endpoint
        [Test]
        public async Task AuthenticateUserAsync_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var user = new UserDTO { Username = "test1", Password = "Password123!" };
            _userInterfaceMock.Setup(x => x.AuthenticateUser(user.Username, user.Password)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.AuthenticateUserAsync(user);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = result as OkObjectResult;
            Assert.NotNull(response.Value);
            Assert.That(response.Value is string);
        }

        [Test]
        public async Task AuthenticateUserAsync_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var user = new UserDTO { Username = "test1", Password = "invalidpassword" };
            _userInterfaceMock.Setup(x => x.AuthenticateUser(user.Username, user.Password)).Returns(Task.FromResult(false));

            // Act
            var result = await _controller.AuthenticateUserAsync(user);

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        //CreateUser Endpoint
        [Test]
        public void CreateUser_UsernameAlreadyExists_ThrowsException()
        {
            // Arrange
            var userDTO = new UserDTO { Username = "test1", Password = "Password123!" };
            _userInterfaceMock.Setup(x => x.UsernameExists(userDTO.Username)).Returns(true);

            // Act
            Assert.Throws<Exception>(() => _controller.CreateUser(userDTO));
        }

        [Test]
        public void CreateUser_ValidUserDTO_ReturnsCreated()
        {
            // Arrange
            var userDTO = new UserDTO { Username = "test2", Password = "Password123!" };
            _userInterfaceMock.Setup(x => x.UsernameExists(userDTO.Username)).Returns(false);
            _userInterfaceMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(true);

            // Act
            var result = _controller.CreateUser(userDTO);

            // Assert
            Assert.IsInstanceOf<object>(result);
        }
    }
}
