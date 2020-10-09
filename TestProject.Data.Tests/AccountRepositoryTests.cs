namespace TestProject.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;
    using Xunit;

    public class AccountRepositoryTests
    {
        private readonly Mock<TestProjectContext> mockContext;

        public AccountRepositoryTests()
        {
            var data = new List<Account>
            {
                new Account { Id = 1, UserId = 1, MaximumMonthlyCreditLimit = 1000 },
                new Account { Id = 2, UserId = 2, MaximumMonthlyCreditLimit = 2000 },
                new Account { Id = 3, UserId = 3, MaximumMonthlyCreditLimit = 3000 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Account>>();
            mockSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            this.mockContext = new Mock<TestProjectContext>();
            this.mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
        }

        [Fact]
        public void GetAccountByUserId_matches_account_in_context()
        {
            var accountRepository = new AccountRepository(mockContext.Object);
            var account = accountRepository.GetAccountByUserId(1);

            Assert.Equal(1000, account.MaximumMonthlyCreditLimit);
        }

        [Fact]
        public void CreateUserAccount_saves_via_context()
        {
            var mockAccountSet = new Mock<DbSet<Account>>();

            var createAccountContext = new Mock<TestProjectContext>();
            createAccountContext.Setup(m => m.Accounts).Returns(mockAccountSet.Object);

            var accountRepository = new AccountRepository(createAccountContext.Object);
            accountRepository.CreateUserAccount(new Account
            {
                MaximumMonthlyCreditLimit = 1200,
                UserId = 1
            });

            mockAccountSet.Verify(m => m.Add(It.IsAny<Account>()), Times.Once());
            createAccountContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetAccounts_returns_all_values_in_context()
        {
            var accountRepository = new AccountRepository(mockContext.Object);
            var accounts = accountRepository.GetAccounts();

            Assert.Equal(3, accounts.Count);
            Assert.Equal(1, accounts[0].Id);
            Assert.Equal(2, accounts[1].Id);
            Assert.Equal(3, accounts[2].Id);
        }
    }
}
