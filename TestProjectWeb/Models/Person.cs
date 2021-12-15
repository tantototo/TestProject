using System.ComponentModel.DataAnnotations;

namespace TestProjectWeb.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }
        [Required]
        public string Passport { get; set; }
        public ICollection<Account>? Accounts { get; set; }

        //public Person(string name, int age, string passport)
        //{
        //    Name = name;
        //    Age = age;
        //    Passport = passport;
        //}
    }
}
