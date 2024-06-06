using Microsoft.AspNetCore.Mvc;
using RivertyInterview.Models.Repository;
using RivertyInterview.Models.ServiceModel;

namespace RivertyInterview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RomanCalculationHistory : Controller
    {
        private IRomanCalculationHistoryService _romanCalculationHistoryService { get; set; }
        public RomanCalculationHistory(IRomanCalculationHistoryService romanCalculationHistoryService)
        {
            _romanCalculationHistoryService = romanCalculationHistoryService;
        }

        [HttpGet("hist")]
        public async Task<ActionResult<IEnumerable<RomanCalculation>>> GetHistory()
        {
            var history = await _romanCalculationHistoryService.GetLast100History();
            return Ok(history);
        }

        [HttpGet("hist/{id}")]
        public async Task<ActionResult<RomanCalculation>> GetHistoryItem(int id)
        {
            var historyItem = await _romanCalculationHistoryService.GetHistoryItemById(id);
            if (historyItem == null)
            {
                return NotFound();
            }
            return Ok(historyItem);
        }
    }
}
