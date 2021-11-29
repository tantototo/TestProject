using System;
using System.Collections.Generic;
using System.Text;


namespace TestProject
{
    public class AccountServices : IAccountServices
    {
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
    }
}
