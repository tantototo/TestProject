using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestProjectWebApi.Models;
using TestProjectWebApi.Data;

namespace TestProjectWebApi.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDBContext db;

        public AccountServices(AppDBContext context)
        {
            db = context;
        }

        public Account Get(int id)
        {
            var account = db.Accounts.Where(acc => acc.Id == id).FirstOrDefault();
            return account;
        }

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

        public string GenerateAccNumber()
        {
            Random rand = new Random();
            String card = "BE";
            for (int i = 0; i < 14; i++)
            {
                int n = rand.Next(10) + 0;
                card += n.ToString();
            }
            return card;
        }
    }
}
