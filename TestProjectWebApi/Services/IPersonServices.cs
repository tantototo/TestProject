using System;
using System.Collections.Generic;
using System.Text;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Services
{
    public interface IPersonServices
    {
        /// <summary>
        /// Добавление, поиск, удаление элемента в списке
        /// </summary>
        /// <param name="person">Элемент для добавления</param>
        
        public IEnumerable<Person> Get();
        public Person Get(int id);
        void Add(Person person);
        public IEnumerable<Person> Find(string search);
        void Update(Person person);
        void Delete(Person person);
    }
}
