
using System.ComponentModel;

namespace TestProjectWeb.Models
{
    public class Account
    {
        public int Id { get; set; }
        [DisplayName("Number")]
        public string? AccNumber { get; set; }
        public int? Sum { get; set; }

        [DisplayName("Person")]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<History>? Histories { get; set; }

        //public Account(Person person)
        //{
        //    //AccNumber = GenerateAccNumber();
        //    Sum = 0;
        //    Person = person;
        //}

    }
}
