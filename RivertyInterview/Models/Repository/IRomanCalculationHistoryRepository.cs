namespace RivertyInterview.Models.Repository
{
    public interface IRomanCalculationHistoryRepository
    {
        Task<IEnumerable<RomanCalculation>> GetAllHistory();
        Task AddHistory(RomanCalculation calculation);
        Task<RomanCalculation> GetHistoryItemById(int id);
    }
}
