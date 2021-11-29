using System;
using System.Collections.Generic;
using System.Text;


namespace TestProjectEF
{
    public class AccountServices : IAccountServices
    {
        private List<Account> accounts = new List<Account>();
        int curSum; //для хранения суммы

        public int CurrentSum { get { return curSum; } }

        public void Put(int sum) { curSum += sum; }

        public void Withdraw(int sum)
        {
            if (curSum >= sum)
            {
                curSum -= sum;
            }
        }
        
        public void Add(Account account)
        {
            accounts.Add(account);
        }

        public IEnumerable<Account> Find(string search)
        {
            IEnumerable<Account> results = accounts.FindAll(x => x.AccNumber.Contains(search));
            return results;
        }

        public void Delete(Account account)
        {
            accounts.Remove(account);
        }

    }
}
