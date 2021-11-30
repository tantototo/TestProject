using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectEF
{
    public interface IAccountServices
    {
        /// <summary>
        /// Операции с текущим счетом
        /// </summary>

        void Put(Account account, int sum);       // Положить
        void Withdraw(Account account, int sum);  // Снять

        void Add(Account account);
        IEnumerable<Account> Find(string search);
        void Update(Account account);
        void Delete(Account account);
    }
}
