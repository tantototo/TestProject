using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestProjectEF
{
    public class AccountServices : IAccountServices
    {
        AppContext db = new AppContext();

        public void Put(Account account, int sum)
        {
            account.Sum += sum;
            account.Histories.Add(new History
            {
                Operation = History.Operations.Put,
                OperDate = DateTime.Now,
                Sum = sum,
                Account = account
            });

            Update(account);
        }
        public void Withdraw(Account account, int sum)
        {
            if (account.Sum >= sum)
            {
                account.Sum -= sum;
                account.Histories.Add(new History
                {
                    Operation = History.Operations.Withdraw,
                    OperDate = DateTime.Now,
                    Sum = sum,
                    Account = account
                });

                Update(account);
            }
        }
        
        public void Add(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }
        public IEnumerable<Account> Find(string search)
        {
            var results = db.Accounts.Where(a => EF.Functions.Like(a.Person.Name, $"%{search}%")
            || EF.Functions.Like(a.Person.Passport, $"%{search}%") || EF.Functions.Like(a.AccNumber, $"%{search}%")).ToList();

            return results;
        }
        public void Update(Account account)
        {
            db.Accounts.Update(account);
            db.SaveChanges();
        }
        public void Delete(Account account)
        {
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

    }
}
