using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Models;
using Xunit;

namespace UnitTests.Web.Controller
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetUsers_ActionExecutes_ReturnsAllUsers()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(GetTestUsers());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<User, UserDto>(It.IsAny<User>())).Returns(new UserDto());

            var controller = new UserController(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetUsers();

            // Assert
            Assert.Equal(2, result.Count);
        }


        private List<User> GetTestUsers()
        {
            List<User> users = new List<User>();
            users.Add(new User(4000, "nmak@gmail.com", "password"));
            users.Add(new User(5000, "abar@gmail.com", "password"));

            return users;
        }
    }
}
