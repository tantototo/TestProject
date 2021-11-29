using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class PersonServices: IPersonServices
    {
        private List<Person> peoples = new List<Person>();

        public void Add(Person person)
        {
            peoples.Add(person);
        }

        public IEnumerable<Person> Find(string search)
        {
            IEnumerable<Person> results = peoples.FindAll(x => x.Name.Contains(search) || x.Passport.Contains(search));
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
