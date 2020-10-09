namespace TestProject.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Data.Models;

    public class AccountRepository : IAccountRepository
    {
        private readonly TestProjectContext context;

        public AccountRepository(TestProjectContext context)
        {
            this.context = context;
        }

        public int CreateUserAccount(Account account)
        {
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account.Id;
        }

        public Account GetAccountByUserId(int userId)
        {
            return this.context.Accounts.FirstOrDefault(a => a.UserId == userId);
        }

        public List<Account> GetAccounts()
        {
            return this.context.Accounts.ToList();
        }
    }
}
