namespace TestProject.Business.Tests
{
    using Microsoft.Extensions.Options;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Business.Models;
    using TestProject.Business.Services;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;
    using Xunit;

    public class AccountServiceTests
    {
        private Mock<IAccountRepository> mockedAccountRepository;

        private Mock<IUserRepository> mockedUserRepository;

        public AccountServiceTests()
        {
            var accountsData = new List<Account>
            {
                new Account { Id = 1, UserId = 1, MaximumMonthlyCreditLimit = 1000 },
                new Account { Id = 2, UserId = 2, MaximumMonthlyCreditLimit = 2000 },
                new Account { Id = 3, UserId = 3, MaximumMonthlyCreditLimit = 3000 },
            };

            var userData = new List<User>
            {
                // This user cannot have a duplicate account.
                new User { Id = 1, MonthlySalary = 2000, MonthlyExpenses = 600 },

                // This user does not have sufficient credit.
                new User { Id = 4, MonthlySalary = 1500, MonthlyExpenses = 600 },

                // This user should be able to create an account.
                new User { Id = 5 , MonthlySalary = 2000, MonthlyExpenses = 600 },
                
                // User 6 doesn't exist so will return not found.
            };

            this.mockedAccountRepository = new Mock<IAccountRepository>();
            this.mockedAccountRepository.Setup(ar => ar.GetAccounts()).Returns(accountsData);
            this.mockedAccountRepository.Setup(ar => ar.GetAccountByUserId(It.Is<int>(i => i == 1))).Returns(accountsData.First());
            this.mockedAccountRepository.Setup(ar => ar.GetAccountByUserId(It.Is<int>(i => i != 1))).Returns((Account)null);
            this.mockedAccountRepository.Setup(ar => ar.CreateUserAccount(It.IsAny<Account>())).Returns(4);

            this.mockedUserRepository = new Mock<IUserRepository>();
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i == 1))).Returns(userData[0]);
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i == 4))).Returns(userData[1]);
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i == 5))).Returns(userData[2]);
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i == 6))).Returns((User)null);
        }

        [Fact]
        public void CreateAccount_created_account_returns_success()
        {
            var mockOptions = Options.Create(new AppSettings { MinimumCreditLimit = 1000 });
            var accountService = new AccountService(this.mockedAccountRepository.Object, this.mockedUserRepository.Object, mockOptions);
            var createAccountResult = accountService.CreateAccount(5);

            Assert.Equal(Models.ResultType.Ok, createAccountResult.ResultType);
            Assert.Equal(4, createAccountResult.Data);
        }

        [Fact]
        public void CreateAccount_no_user_returns_not_found()
        {
            var mockOptions = Options.Create(new AppSettings { MinimumCreditLimit = 1000 });
            var accountService = new AccountService(this.mockedAccountRepository.Object, this.mockedUserRepository.Object, mockOptions);
            var createAccountResult = accountService.CreateAccount(6);

            Assert.Equal(Models.ResultType.NotFound, createAccountResult.ResultType);
        }

        [Fact]
        public void CreateAccount_insufficient_credit_returns_invalid()
        {
            var mockOptions = Options.Create(new AppSettings { MinimumCreditLimit = 1000 });
            var accountService = new AccountService(this.mockedAccountRepository.Object, this.mockedUserRepository.Object, mockOptions);
            var createAccountResult = accountService.CreateAccount(4);

            Assert.Equal(Models.ResultType.Invalid, createAccountResult.ResultType);
        }

        [Fact]
        public void CreateAccount_duplicate_account_returns_conflict()
        {
            var mockOptions = Options.Create(new AppSettings { MinimumCreditLimit = 1000 });
            var accountService = new AccountService(this.mockedAccountRepository.Object, this.mockedUserRepository.Object, mockOptions);
            var createAccountResult = accountService.CreateAccount(1);

            Assert.Equal(Models.ResultType.Conflict, createAccountResult.ResultType);
        }

        [Fact]
        public void GetAccounts_returns_all_accounts()
        {
            var mockOptions = Options.Create(new AppSettings { MinimumCreditLimit = 1000 });
            var accountService = new AccountService(this.mockedAccountRepository.Object, this.mockedUserRepository.Object, mockOptions);
            var accountsResult = accountService.GetAccounts();

            Assert.Equal(Models.ResultType.Ok, accountsResult.ResultType);
            Assert.Equal(3, accountsResult.Data.Count);
            Assert.Equal(1, accountsResult.Data[0].Id);
            Assert.Equal(2, accountsResult.Data[1].Id);
            Assert.Equal(3, accountsResult.Data[2].Id);
        }
    }
}
