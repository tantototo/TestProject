using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectEF
{
    public class Person //: AccountServices
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Passport { get; set; }
        public List<Account> Accounts { get; set; } = new();

        //public Person(string name, int age, string passport)
        //{
        //    Name = name;
        //    Age = age;
        //    Passport = passport;
        //}
    }
}
