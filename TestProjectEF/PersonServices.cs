using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectEF
{
    public class PersonServices : IPersonServices
    {
        AppContext db = new AppContext();

        public void Add(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }
        public IEnumerable<Person> Find(string search)
        {
            //var users = db.Persons.Where(p => EF.Functions.Like(p.Name, $"%{search}%") 
            //|| EF.Functions.Like(p.Passport, $"%{search}%"));

            IEnumerable<Person> results = from p in db.Persons
                                          where EF.Functions.Like(p.Name, $"%{search}%") 
                                          || EF.Functions.Like(p.Passport, $"%{search}%")
                                          select p;
            return results;
        }
        public void Update(Person person)
        {
            db.Persons.Update(person);
            db.SaveChanges();
        }
        public void Delete(Person person)
        {
            db.Persons.Remove(person);
            db.SaveChanges();
        }

    }
}
