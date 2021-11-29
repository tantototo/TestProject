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

        int CurrentSum { get; }  // Текущая сумма
        void Put(int sum);       // Положить
        void Withdraw(int sum);  // Снять

        void Add(Account account);
        IEnumerable<Account> Find(string search);
        void Delete(Account account);
    }
}
