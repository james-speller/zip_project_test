namespace TestProject.Business.Services
{
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using TestProject.Business.Models;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        private readonly IUserRepository userRepository;

        private readonly AppSettings appSettings;

        public AccountService(
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            IOptions<AppSettings> appSettings)
        {
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        /// <inheritdoc/>
        public Result<int> CreateAccount(int userId)
        {
            // Get User, if it doesn't exist then send a not found response.
            var user = this.userRepository.GetUserById(userId);

            if (user == null)
            {
                return new NotFoundResult<int>($"No User record was found with Id: {userId}.");
            }

            // Check the User's monthly salary - expenses > credit limit otherwise we cannot create them an account
            // and return an invalid response.
            if (user.MonthlySalary - user.MonthlyExpenses < appSettings.MinimumCreditLimit)
            {
                return new InvalidResult<int>(
                    new List<string>
                    {
                        $"The User's salary ({user.MonthlySalary} - expenses ({user.MonthlyExpenses}) is not greater than the credit limit of {appSettings.MinimumCreditLimit}."
                    });
            }

            // Check for an existing Account to make sure we don't create duplicates. If one
            // already exists then return a conflict response.
            var existingUserAccount = this.accountRepository.GetAccountByUserId(userId);
            if (existingUserAccount != null)
            {
                return new ConflictResult<int>($"The User with Id: {userId} already has an existing Account with Id {existingUserAccount.Id}.");
            }

            // Create the account and return the id to the client as part of a success response.
            var accountId = this.accountRepository.CreateUserAccount(new Account
            {
                MaximumMonthlyCreditLimit = user.MonthlySalary - user.MonthlyExpenses,
                UserId = userId,
            });

            return new SuccessResult<int>(accountId);
        }

        /// <inheritdoc/>
        public Result<List<Account>> GetAccounts()
        {
            return new SuccessResult<List<Account>>(this.accountRepository.GetAccounts());
        }
    }
}
