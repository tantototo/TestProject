using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProjectWeb.Data;
using TestProjectWeb.Models;

namespace TestProjectWeb.Controllers
{
    public class PersonsController : Controller
    {
        private readonly AppDBContext _context;

        public PersonsController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var personsList = _context.Persons;
            return View(await personsList.ToListAsync());
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var personsList = _context.Persons.Where(p => EF.Functions.Like(p.Name, $"%{searchString}%")
            || EF.Functions.Like(p.Passport, $"%{searchString}%"));

            return View("Index", await personsList.ToListAsync());
        }

        //Get
        public IActionResult Add()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Person person)
        {
            if (CheckPassport(person.Passport))
            {
                return View(person);
            }

            if (ModelState.IsValid)
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                NotifacateSuccess("Person created successfully");
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var personList = _context.Persons.Find(id);
            if(personList == null)
            {
                return NotFound();
            }

            return View(personList);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Persons.Update(person);
                _context.SaveChanges();
                NotifacateSuccess("Person updated successfully");
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var person = _context.Persons.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePerson(int? id)
        {
            var person = _context.Persons.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            NotifacateSuccess("Person deleted successfully");
            return RedirectToAction("Index");
        }


        private bool CheckPassport(string pas)
        {
            IEnumerable<Person> result = _context.Persons.Where(p => p.Passport == pas);
            if (result.Count() > 0)
            {
                TempData["error"] = "Клиент с таким паспортом уже существует.";
                return true;
            }
            else
            {
                return false;
            }
        }

        private void NotifacateSuccess(string message)
        {
            TempData["success"] = message;
        }

    }
}
