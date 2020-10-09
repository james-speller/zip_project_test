namespace TestProject.Tests
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestProject.Business.Models;
    using TestProject.Business.Services;
    using TestProject.Data.Models;
    using TestProject.WebAPI.Controllers;
    using Xunit;

    public class AccountControllerTests
    {
        private Mock<IAccountService> mockedAccountService;

        private Mock<ILogger<AccountController>> mockedLogger;

        public AccountControllerTests()
        {
            var accountsData = new List<Account>
            {
                new Account { Id = 1, UserId = 1, MaximumMonthlyCreditLimit = 1000 },
                new Account { Id = 2, UserId = 2, MaximumMonthlyCreditLimit = 2000 },
                new Account { Id = 3, UserId = 3, MaximumMonthlyCreditLimit = 3000 },
            };

            this.mockedAccountService = new Mock<IAccountService>();
            this.mockedAccountService.Setup(ar => ar.CreateAccount(It.Is<int>(i => i == 4))).Returns(new SuccessResult<int>(4));
        }

        [Fact]
        public async void CreateAccount_create_returns_succcess()
        {
            var accountController = new AccountController(this.mockedLogger.Object, this.mockedAccountService.Object);
            var createAccountResult = await accountController.CreateAccount(4);
            Assert.Equal(, createAccountResult.)
        }

        [Fact]
        public void CreateAccount_duplicate_returns_conflict()
        {
            var accountController = new AccountController(this.mockedLogger.Object, this.mockedAccountService.Object);
        }

        [Fact]
        public void CreateAccount_no_user_returns_not_found()
        {
            var accountController = new AccountController(this.mockedLogger.Object, this.mockedAccountService.Object);
        }

        [Fact]
        public void CreateAccount_insufficient_credit_returns_invalid()
        {
            var accountController = new AccountController(this.mockedLogger.Object, this.mockedAccountService.Object);
        }

        [Fact]
        public void ListAccounts_returns_all_accounts()
        {
            var accountController = new AccountController(this.mockedLogger.Object, this.mockedAccountService.Object);
        }
    }
}
