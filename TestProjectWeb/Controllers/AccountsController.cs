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
    public class AccountsController : Controller
    {
        private readonly AppDBContext _context;

        public AccountsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index(int? id)
        {
            //var appDBContext = _context.Accounts.Include(a => a.Person);

            var appDBContext = _context.Accounts.Where(a => a.PersonId == id);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewBag.AccNumber = GenerateAccNumber();
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccNumber,Sum,PersonId")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Sum ??= 0;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = account.PersonId });
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Name", account.PersonId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Passport", account.PersonId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccNumber,Sum,PersonId")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = account.PersonId });
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Passport", account.PersonId);
            return View(account);
        }


        // GET: Accounts/Put
        public async Task<IActionResult> Put(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewBag.Operation = History.Operations.Put;
            return View(account);
        }

        // POST: Accounts/Put
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Put(int id, int sum)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            try

            {
                account.Sum += sum;
                account.Histories.Add(new History
                {
                    Operation = History.Operations.Put,
                    OperDate = DateTime.Now,
                    Sum = sum,
                    Account = account
                });
                _context.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new { id = account.PersonId });

        }

        // GET: Accounts/Withdraw
        public async Task<IActionResult> Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewBag.Operation = History.Operations.Withdraw;
            return View(account);
        }

        // POST: Accounts/Withdraw
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, int sum)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            try
            {
                if (account.Sum >= sum)
                {
                    account.Sum -= sum;
                    account.Histories.Add(new History
                    {
                        Operation = History.Operations.Withdraw,
                        OperDate = DateTime.Now,
                        Sum = sum,
                        Account = account
                    });
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                else { 
                    TempData["error"] = "Недостаточно средств на счету.";
                    ViewBag.Operation = History.Operations.Withdraw;
                    return View(account);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new { id = account.PersonId });

        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = account.PersonId });
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        static string GenerateAccNumber()
        {
            Random rand = new Random();
            String card = "BE";
            for (int i = 0; i < 14; i++)
            {
                int n = rand.Next(10) + 0;
                card += n.ToString();
            }
            return card;
        }
    }
}
