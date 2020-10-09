namespace TestProject.Data.Repositories
{
    using System.Collections.Generic;
    using TestProject.Data.Models;

    public interface IAccountRepository
    {
        Account GetAccountByUserId(int userId);

        int CreateUserAccount(Account account);

        List<Account> GetAccounts();
    }
}
