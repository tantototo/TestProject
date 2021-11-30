using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectEF
{
    public class History
    {
        public enum Operations { Put, Withdraw }

        public int Id { get; set; }
        public Operations Operation { get; set; }
        public DateTime OperDate { get; set; }
        public int? Sum { get; set; }

        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        //public History(Operations oper, int? _sum, Account account)
        //{
        //    Operation = oper;
        //    OperDate = new DateTime();
        //    Sum = _sum;
        //    Account = account;
        //}
    }
}
