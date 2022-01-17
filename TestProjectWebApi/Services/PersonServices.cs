using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestProjectWebApi.Models;
using TestProjectWebApi.Data;

namespace TestProjectWebApi.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly AppDBContext db;

        public PersonServices(AppDBContext context)
        {
            db = context;
        }

        public IEnumerable<Person> Get()
        {
            var personsList = db.Persons;
            return personsList;
        }

        public Person Get(int id)
        {
            var person = db.Persons.Where(person => person.Id == id).FirstOrDefault();
            return person;
        }

        public void Add(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }

        public IEnumerable<Person> Find(string search)
        {
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
