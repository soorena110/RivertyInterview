using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;

namespace RivertyInterview.Services
{
    public class RomanCalculationHistoryService : IRomanCalculationHistoryService
    {
        private IRomanCalculationHistoryRepository _romanCalculationHistoryRepository;
        public RomanCalculationHistoryService(IRomanCalculationHistoryRepository repository)
        {
            _romanCalculationHistoryRepository = repository;
        }
        public async Task SaveAddHistory(AddHistory addHistory)
        {
            await _romanCalculationHistoryRepository.AddHistory(new RomanCalculation
            {
                LeftOperand = addHistory.LeftOperand,
                RightOperand = addHistory.RightOperand,
                Result = addHistory.Result,
                Operation = RomanCalculationOperation.Add
            });
        }

        public async Task<IList<RomanCalculation>> GetLast100History()
        {
            return (await _romanCalculationHistoryRepository.GetAllHistory()).Take(100).ToArray();
        }

        public async Task<RomanCalculation> GetHistoryItemById(int id)
        {
            return await _romanCalculationHistoryRepository.GetHistoryItemById(id);
        }

    }
}
