using RivertyInterview.Models.Repository;

namespace RivertyInterview.Models.ServiceModel
{
    public record struct AddHistory
    {
        public string LeftOperand; 
        public string RightOperand; 
        public string Result;
    }

    public interface IRomanCalculationHistoryService
    {
        public Task SaveAddHistory(AddHistory addHistory);
        Task<IList<RomanCalculation>> GetLast100History();
        Task<RomanCalculation> GetHistoryItemById(int id);
    }
}
