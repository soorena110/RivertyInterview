using Moq;
using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;
using RivertyInterview.Services;
using Xunit;

public class RomanCalculationHistoryServiceTests
{
    private readonly Mock<IRomanCalculationHistoryRepository> _mockRepository;
    private readonly IRomanCalculationHistoryService _service;

    public RomanCalculationHistoryServiceTests()
    {
        _mockRepository = new Mock<IRomanCalculationHistoryRepository>();
        _service = new RomanCalculationHistoryService(_mockRepository.Object);
    }

    [Fact]
    public async Task SaveAddHistory_Should_Call_AddHistory_On_Repository()
    {
        var addHistory = new AddHistory
        {
            LeftOperand = "X",
            RightOperand = "V",
            Result = "XV"
        };

        await _service.SaveAddHistory(addHistory);

        _mockRepository.Verify(repo => repo.AddHistory(It.Is<RomanCalculation>(calc =>
            calc.LeftOperand == addHistory.LeftOperand &&
            calc.RightOperand == addHistory.RightOperand &&
            calc.Result == addHistory.Result &&
            calc.Operation == RomanCalculationOperation.Add
        )), Times.Once);
    }


    [Fact]
    public async Task SaveAddHistory_Should_Add_Multiple_Entries()
    {
        var addHistory1 = new AddHistory
        {
            LeftOperand = "X",
            RightOperand = "V",
            Result = "XV"
        };
        var addHistory2 = new AddHistory
        {
            LeftOperand = "L",
            RightOperand = "X",
            Result = "LX"
        };

        await _service.SaveAddHistory(addHistory1);
        await _service.SaveAddHistory(addHistory2);

        _mockRepository.Verify(repo => repo.AddHistory(It.IsAny<RomanCalculation>()), Times.Exactly(2));
    }

    [Fact]
    public async Task GetLast100History_Should_Return_Last_100_History_Entries()
    {
        var historyEntries = new List<RomanCalculation>();
        for (int i = 1; i <= 150; i++)
        {
            historyEntries.Add(new RomanCalculation
            {
                Id = i,
                LeftOperand = $"LeftOperand{i}",
                RightOperand = $"RightOperand{i}",
                Result = $"Result{i}",
                Operation = RomanCalculationOperation.Add
            });
        }

        _mockRepository.Setup(repo => repo.GetAllHistory()).ReturnsAsync(historyEntries.Skip(50).ToList());

        var result = await _service.GetLast100History();

        Assert.Equal(100, result.Count);
        for (int i = 0; i < 100; i++)
        {
            Assert.Equal(historyEntries[i + 50].Id, result[i].Id);
            Assert.Equal(historyEntries[i + 50].LeftOperand, result[i].LeftOperand);
            Assert.Equal(historyEntries[i + 50].RightOperand, result[i].RightOperand);
            Assert.Equal(historyEntries[i + 50].Result, result[i].Result);
            Assert.Equal(historyEntries[i + 50].Operation, result[i].Operation);
        }
    }


    [Fact]
    public async Task GetHistoryItemById_ValidId_ReturnsHistoryItem()
    {
        var historyItem = new RomanCalculation { Id = 1, LeftOperand = "X", RightOperand = "V", Result = "XV" };
        _mockRepository.Setup(repo => repo.GetHistoryItemById(It.IsAny<int>())).ReturnsAsync(historyItem);

        var result = await _service.GetHistoryItemById(1);

        Assert.NotNull(result);
        Assert.Equal(historyItem.Id, result.Id);
        Assert.Equal(historyItem.LeftOperand, result.LeftOperand);
        Assert.Equal(historyItem.RightOperand, result.RightOperand);
        Assert.Equal(historyItem.Result, result.Result);
    }

    [Fact]
    public async Task GetHistoryItemById_InvalidId_ReturnsNull()
    {
        _mockRepository.Setup(repo => repo.GetHistoryItemById(It.IsAny<int>())).ReturnsAsync((RomanCalculation)null!);

        var result = await _service.GetHistoryItemById(100);

        Assert.Null(result);
    }
}
