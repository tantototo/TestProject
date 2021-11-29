using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    class Program
    {
        static IPersonServices service = new PersonServices();

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
                    Console.WriteLine($"Сумма на счету: {FindUsedPerson().CurrentSum}");
                    break;
                case 4:
                    FindUsedPerson().Put(GetSum());
                    break;
                case 5:
                    FindUsedPerson().Withdraw(GetSum());
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
            string name = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = ReadNumber();

            string text = "Введите номер паспорта: ";
            Console.Write(text);
            string pas = Console.ReadLine();
            while (CheckPassport(pas))
            {
                Console.Write(text);
                pas = Console.ReadLine();
            }

            var person = new Person(name, age, pas);
            return person;
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

    }
}
