using System;
using System.Collections.Generic;
using System.Text;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Services
{
    public interface IAccountServices
    {
        /// <summary>
        /// Операции с текущим счетом
        /// </summary>

        public Account Get(int id);

        void Put(Account account, int sum);       // Положить
        void Withdraw(Account account, int sum);  // Снять

        void Add(Account account);
        IEnumerable<Account> Find(string search);
        void Update(Account account);
        void Delete(Account account);

        string GenerateAccNumber();
    }
}
