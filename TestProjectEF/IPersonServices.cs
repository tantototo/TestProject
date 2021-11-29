using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectEF
{
    public interface IPersonServices
    {
        /// <summary>
        /// Добавление, поиск, удаление элемента в списке
        /// </summary>
        /// <param name="person">Элемент для добавления</param>

        void Add(Person person);
        IEnumerable<Person> Find(string search);
        Person Change(Person person);
        void Delete(Person person);
    }
}
