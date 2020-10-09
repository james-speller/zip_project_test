namespace TestProject.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;
    using Xunit;

    public class UserRepositoryTests
    {
        private readonly Mock<TestProjectContext> mockContext;

        public UserRepositoryTests()
        {
            var data = new List<User>
            {
                new User { Id = 1, Email = "user1@gmail.com", Name = "First User", MonthlySalary = 1000, MonthlyExpenses = 500 },
                new User { Id = 2, Email = "user2@gmail.com", Name = "Second User", MonthlySalary = 2000, MonthlyExpenses = 750 },
                new User { Id = 3, Email = "user3@gmail.com", Name = "Third User", MonthlySalary = 3000, MonthlyExpenses = 1000 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            this.mockContext = new Mock<TestProjectContext>();
            this.mockContext.Setup(c => c.Users).Returns(mockSet.Object);
        }

        [Fact]
        public void CreateUser_saves_via_context()
        {
            var mockUserSet = new Mock<DbSet<User>>();

            var createMockContext = new Mock<TestProjectContext>();
            createMockContext.Setup(m => m.Users).Returns(mockUserSet.Object);

            var userRepository = new UserRepository(createMockContext.Object);
            userRepository.CreateUser(new User
            {
                Email = "james.speller@gmail.com",
                Name = "James",
                MonthlySalary = 2500,
                MonthlyExpenses = 1250,
            });

            mockUserSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            createMockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetUserById_matches_user_in_context()
        {
            var userRepository = new UserRepository(mockContext.Object);
            var user = userRepository.GetUserById(2);

            Assert.Equal("Second User", user.Name);
        }

        [Fact]
        public void GetUserByEmail_matches_user_in_context()
        {
            var userRepository = new UserRepository(mockContext.Object);
            var user = userRepository.GetUserByEmail("user3@gmail.com");

            Assert.Equal("Third User", user.Name);
        }

        [Fact]
        public void GetUsers_returns_all_users_in_context()
        {
            var userRepository = new UserRepository(mockContext.Object);
            var users = userRepository.GetUsers();

            Assert.Equal(3, users.Count);
            Assert.Equal("First User", users[0].Name);
            Assert.Equal("Second User", users[1].Name);
            Assert.Equal("Third User", users[2].Name);
        }
    }
}
