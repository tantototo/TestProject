using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectEF
{
    class Program
    {
        static IPersonServices service = new PersonServices();
        static IAccountServices accService = new AccountServices();

        static void Main(string[] args)
        {
            var isOperation = true;
            while (isOperation)
            {
                try
                {
                    PrintInformation();
                    RunCommand(ReadNumber());
                }
                catch
                {
                    isOperation = false;
                    Console.WriteLine("Операции завершены.");
                }
            }
        }

        static void RunCommand(int number)
        {
            switch (number)
            {
                case 1:
                    service.Add(GetPerson());
                    break;
                case 2:
                    FindPerson();
                    break;
                case 3:
                    Console.WriteLine($"Сумма на счету: {FindAccount().Sum}");
                    break;
                case 4:
                    accService.Put(FindAccount(), GetSum());
                    break;
                case 5:
                    accService.Withdraw(FindAccount(), GetSum());
                    break;
                case 6:
                    service.Delete(FindUsedPerson());
                    break;
                default:
                    Console.WriteLine($"Операция с номером {number} отсутствует.");
                    break;
            }
        }


        static void PrintInformation()
        {
            string text = "Введите номер операции: \n" +
                        "1 - Создание клиента \n" +
                        "2 - Найти клиента \n" +
                        "3 - Проверить баланс \n" +
                        "4 - Положить деньги на счет \n" +
                        "5 - Снять деньги со счета \n" +
                        "6 - Удалить клиента \n" +
                        "Для отмены введите любую другую клавишу. ";

            Console.WriteLine(text);
        }

        static int ReadNumber()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        static Person GetPerson()
        {
            Console.Write("Введите имя клиента банка: ");
            var name = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = ReadNumber();

            string text = "Введите номер паспорта: ";
            Console.Write(text);
            var pas = Console.ReadLine();
            while (CheckPassport(pas))
            {
                Console.Write(text);
                pas = Console.ReadLine();
            }

            //var person = new Person(name, age, pas);
            var person = new Person { Name = name, Age = age, Passport = pas };
            person.Accounts.Add(new Account { Sum = 0, AccNumber = GenerateAccNumber(), Person = person });
            return person;
        }

        static Account FindAccount()
        {
            string text = "Введите строку для поиска: ";
            Console.Write(text);
            var result = accService.Find(Console.ReadLine());
            while (result.Count() != 1)
            {
                Console.Write(text);
                result = accService.Find(Console.ReadLine());
            }

            var enumerator = result.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        static Person FindUsedPerson()
        {
            var result = FindPerson();
            while(result.Count() != 1)
            {
                result = FindPerson();
            }

            var enumerator = result.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        static IEnumerable<Person> FindPerson()
        {
            Console.Write("Введите строку для поиска: ");
            IEnumerable<Person> result = service.Find(Console.ReadLine());
            var enumerator = result.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var client = enumerator.Current;
                Console.WriteLine($"Найден клиент: {client.Name}, пасспорт - {client.Passport}");
            }
            return result;
        }

        static int GetSum()
        {
            Console.Write("Введите сумму: ");
            return ReadNumber();
        } 

        static bool CheckPassport(string pas)
        {
            IEnumerable<Person> result = service.Find(pas);
            if (result.Count() > 0)
            {
                Console.Write("Клиент с таким паспортом уже существует.");
                return true;
            }
            else
            {
                return false;
            }
        }

        static string GenerateAccNumber()
        {
            Random rand = new Random();
            String card = "BE";
            for (int i = 0; i < 14; i++)
            {
                int n = rand.Next(10) + 0;
                card += n.ToString();
            }
            return card;
        }

    }
}
