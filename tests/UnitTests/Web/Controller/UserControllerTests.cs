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
        public async Task GetBudgets_ActionExecutes_ReturnsAllBudgets()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetBudgetsAsync()).ReturnsAsync(GetTestSessions());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<User, UserDto>(It.IsAny<User>())).Returns(new UserDto());

            var controller = new UserController(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetBudgets();

            // Assert
            var budgets = Assert.IsAssignableFrom<ActionResult<IEnumerable<UserDto>>>(result);
        }


        private IEnumerable<User> GetTestSessions()
        {
            var sessions = new List<User>();
            sessions.Add(new User(4000, 2000));
            sessions.Add(new User(5000, 3000));


            return sessions;
        }
    }
}
