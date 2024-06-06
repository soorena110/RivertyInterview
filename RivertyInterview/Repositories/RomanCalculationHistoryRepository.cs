using System.Text;
using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;
using RivertyInterview.Models.Validation;

namespace RivertyInterview.Services
{

    public class RomanCalculationHistoryRepository : IRomanCalculationHistoryRepository
    {
        private static readonly List<RomanCalculation> history = new List<RomanCalculation>();
        private static int lastIdInDatabase = -1;

        public async Task<IEnumerable<RomanCalculation>> GetAllHistory()
        {
            await Task.Delay(1000); // Time for retrieving from database!!

            return history;
        }

        public async Task AddHistory(RomanCalculation calculation)
        {
            await Task.Delay(1000); // Just imagine  we are saving data in a database!!

            calculation.Id = ++lastIdInDatabase; // ← Intentionally and not a bug!!!
                                                 // This will effect the reference-object but it is ok because after using this method we need the new id from data base.
            history.Add(calculation);
        }
        public async Task<RomanCalculation> GetHistoryItemById(int id)
        {
            await Task.Delay(1000); // Wait for DB!!
#pragma warning disable CS8603 // Possible null reference return.
            return history.FirstOrDefault(item => item.Id == id);
#pragma warning restore CS8603
        }
    }
}
