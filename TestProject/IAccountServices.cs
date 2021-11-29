using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public interface IAccountServices
    {
        /// <summary>
        /// Операции с текущим счетом
        /// </summary>

        int CurrentSum { get; }  // Текущая сумма
        void Put(int sum);       // Положить
        void Withdraw(int sum);  // Снять
    }
}
