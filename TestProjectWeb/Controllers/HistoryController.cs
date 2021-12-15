using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestProjectWeb.Data;
using TestProjectWeb.Models;

namespace TestProjectWeb.Controllers
{
    public class HistoryController : Controller
    {
        private readonly AppDBContext _context;

        public HistoryController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.History.Include(a => a.Account);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> OpenAccountHistory(int? id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || id == null)
            {
                return NotFound();
            }
            if (account.Histories == null || account.Histories.Count() == 0)
            {
                return NotFound();
            }
            var history = account.Histories.OrderByDescending(h => h.OperDate).ToList();
            return View(nameof(Index), history);
        }

    }
}
