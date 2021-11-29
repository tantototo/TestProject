using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectEF
{
    public class PersonServices : IPersonServices
    {
        private List<Person> peoples = new List<Person>();

        public void Add(Person person)
        {
            peoples.Add(person);
            using (var db = new AppContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        public IEnumerable<Person> Find(string search)
        {
            IEnumerable<Person> results = peoples.FindAll(x => x.Name.Contains(search) || x.Passport.Contains(search));

            using (var db = new AppContext())
            {
                var users = db.Persons.Where(p => EF.Functions.Like(p.Name, $"%{search}%") 
                || EF.Functions.Like(p.Passport, $"%{search}%"));
                
                var users2 = from p in db.Persons
                             where EF.Functions.Like(p.Name, $"%{search}%") || EF.Functions.Like(p.Passport, $"%{search}%")
                             select p;
            }
            return results;
        }

        public Person Change(Person person)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person person)
        {
            peoples.Remove(person);
        }

    }
}
