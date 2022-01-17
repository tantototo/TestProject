using TestProjectWebApi.Dtos;
using TestProjectWebApi.Models;

namespace TestProjectWebApi
{
    public static class Extensions
    {
        public static PersonDto AsDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Passport = person.Passport,
                Accounts = person.Accounts
            };
        }

        public static AccountDto AsDto(this Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                AccNumber = account.AccNumber,
                Sum = account.Sum,
                PersonId = account.PersonId,
                Person = account.Person,
                Histories = account.Histories
            };
        }
    }
}
