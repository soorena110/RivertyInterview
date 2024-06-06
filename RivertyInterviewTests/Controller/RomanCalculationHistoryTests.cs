using Microsoft.AspNetCore.Mvc;
using Moq;
using RivertyInterview.Controllers;
using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;
using RivertyInterview.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RivertyInterviewTests.Controllers
{
    public class RomanCalculationHistoryTests
    {
        private readonly Mock<IRomanCalculationHistoryService> _mockService;
        private readonly RomanCalculationHistory _controller;

        public RomanCalculationHistoryTests()
        {
            _mockService = new Mock<IRomanCalculationHistoryService>();
            _controller = new RomanCalculationHistory(_mockService.Object);
        }

        [Fact]
        public async Task GetHistory_ReturnsOkResult()
        {
            _mockService.Setup(service => service.GetLast100History()).ReturnsAsync(new List<RomanCalculation>());
            var result = await _controller.GetHistory();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetHistoryItemById_ExistingId_ReturnsOkResult()
        {
            var historyItem = new RomanCalculation { Id = 1, LeftOperand = "X", RightOperand = "V", Result = "XV" };
            _mockService.Setup(service => service.GetHistoryItemById(It.IsAny<int>())).ReturnsAsync(historyItem);
            var result = await _controller.GetHistoryItem(1);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetHistoryItemById_NonExistingId_ReturnsNotFoundResult()
        {
            _mockService.Setup(service => service.GetHistoryItemById(It.IsAny<int>())).ReturnsAsync((RomanCalculation)null!);
            var result = await _controller.GetHistoryItem(100);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
