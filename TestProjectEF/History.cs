using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectEF
{
    public class History
    {
        public enum Operation { Put, Withdraw }

        int id;
        Operation operation;
        DateTime operDate;
        int sum;
        public Account Account { get; set; }
        public History(Operation oper, int _sum, Account account)
        {
            operation = oper;
            operDate = new DateTime();
            sum = _sum;
            Account = account;
        }
    }
}
