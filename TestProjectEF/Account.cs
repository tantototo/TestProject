using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProjectEF
{
    public class Account
    {
        public int Id { get; set; }
        public string? AccNumber { get; set; }
        public int? Sum { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public List<History> Histories { get; set; } = new();

        //public Account(Person person)
        //{
        //    //AccNumber = GenerateAccNumber();
        //    Sum = 0;
        //    Person = person;
        //}

    }
}
