using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class Person : AccountServices
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Passport { get; set; }
        public Person(string name, int age, string pas)
        {
            Name = name;
            Age = age;
            Passport = pas;
        }

    }
}
