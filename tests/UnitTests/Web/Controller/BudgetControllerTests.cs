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
    public class BudgetControllerTests
    {
        [Fact]
        public async Task GetBudgets_ActionExecutes_ReturnsAllBudgets()
        {
            // Arrange
            var mockRepo = new Mock<IBudgetRepository>();
            mockRepo.Setup(repo => repo.GetBudgetsAsync()).ReturnsAsync(GetTestSessions());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<Budget, BudgetDto>(It.IsAny<Budget>())).Returns(new BudgetDto());

            var controller = new BudgetController(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetBudgets();

            // Assert
            var budgets = Assert.IsAssignableFrom<ActionResult<IEnumerable<BudgetDto>>>(result);
        }


        private IEnumerable<Budget> GetTestSessions()
        {
            var sessions = new List<Budget>();
            sessions.Add(new Budget(new DateTime(2020, 10, 1)));
            sessions.Add(new Budget(new DateTime(2020, 12, 1)));

            return sessions;
        }
    }
}
