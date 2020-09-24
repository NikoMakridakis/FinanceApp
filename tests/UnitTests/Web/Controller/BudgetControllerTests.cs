using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Models;
using Xunit;

namespace UnitTests.Web.Controller
{
    public class BudgetControllerTests
    {
        private readonly BudgetController _budgetController;
        private readonly Mock<IFinanceAppRepository> _financeAppRepoMock = new Mock<IFinanceAppRepository>();
        private readonly IMapper _mapper;
        public BudgetControllerTests()
        {
            _budgetController = new BudgetController(_financeAppRepoMock.Object, _mapper);
        }


        [Fact]
        public async Task GetBudgets_ShouldReturnAllBudgets()
        {
            // Arrange
            var budgetId = 100;
            var date = DateTime.Now;
            var budgetDto = new BudgetDto
            {
                BudgetId = budgetId,
                Date = date
            };
            _financeAppRepoMock.Setup(x => x.GetBudgetsAsync()).ReturnsAsync();

            // Act
            await _budgetController.GetBudgets();

            // Assert
        }
    }
}
