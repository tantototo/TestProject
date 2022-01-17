using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProjectWebApi.Data;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly AppDBContext _context;

        public HistoryController(AppDBContext context)
        {
            _context = context;
        }

        // GET api/<PersonController>/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<History>> OpenAccountHistory(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }
            if (account.Histories == null || account.Histories.Count() == 0)
            {
                return NotFound();
            }
            var history = account.Histories.OrderByDescending(h => h.OperDate).ToList();
            return Ok(history);
        }

    }
}
