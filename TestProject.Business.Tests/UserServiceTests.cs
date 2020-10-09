namespace TestProject.Business.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TestProject.Business.Services;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;
    using Xunit;

    public class UserServiceTests
    {
        private Mock<IUserRepository> mockedUserRepository;

        public UserServiceTests()
        {
            var userData = new List<User>
            {
                new User { Id = 1, Name = "First User", Email = "user1@gmail.com", MonthlySalary = 2000, MonthlyExpenses = 600 },

                new User { Id = 2, Name = "Second User", Email = "user2@gmail.com", MonthlySalary = 1500, MonthlyExpenses = 600 },

                new User { Id = 3 , Name = "Third User", Email = "user3@gmail.com", MonthlySalary = 2000, MonthlyExpenses = 600 },
                
                // User 4 doesn't exist be okay to create.
            };

            this.mockedUserRepository = new Mock<IUserRepository>();
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i == 1))).Returns(userData[0]);
            this.mockedUserRepository.Setup(ur => ur.GetUserById(It.Is<int>(i => i > 3))).Returns((User)null);
            this.mockedUserRepository.Setup(ur => ur.GetUserByEmail(It.Is<string>(i => i == "user2@gmail.com"))).Returns(userData[1]);
            this.mockedUserRepository.Setup(ur => ur.GetUsers()).Returns(userData);
            this.mockedUserRepository.Setup(ur => ur.CreateUser(It.IsAny<User>())).Returns(4);
        }

        [Fact]
        public void Create_created_user_returns_success()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var userCreationResult = userService.CreateUser(new User { Name = "Fourth User", Email = "user4@gmail.com", MonthlyExpenses = 1000, MonthlySalary = 3000 });
            Assert.Equal(Models.ResultType.Ok, userCreationResult.ResultType);
            Assert.Equal(4, userCreationResult.Data);
        }

        [Fact]
        public void Create_duplicate_email_returns_conflict()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var userCreationResult = userService.CreateUser(new User { Name = "Fourth User", Email = "user2@gmail.com", MonthlyExpenses = 1000, MonthlySalary = 3000 });
            Assert.Equal(Models.ResultType.Conflict, userCreationResult.ResultType);
        }

        [Fact]
        public void Create_invalid_user_returns_invalid()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var userCreationResult = userService.CreateUser(new User { Name = "", Email = " ", MonthlyExpenses = -1000, MonthlySalary = -3000 });
            Assert.Equal(Models.ResultType.Invalid, userCreationResult.ResultType);
            Assert.Equal(4, userCreationResult.Errors.Count);
        }

        [Fact]
        public void GetUserById_matching_user_returns_success()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var getUserResult = userService.GetUserById(1);
            Assert.Equal(Models.ResultType.Ok, getUserResult.ResultType);
            Assert.Equal("user1@gmail.com", getUserResult.Data.Email);
        }

        [Fact]
        public void GetUserById_no_matching_user_returns_not_found()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var getUserResult = userService.GetUserById(4);
            Assert.Equal(Models.ResultType.NotFound, getUserResult.ResultType);
        }

        [Fact]
        public void GetUser_returns_all_users()
        {
            var userService = new UserService(this.mockedUserRepository.Object);
            var getUsersResult = userService.GetUsers();

            Assert.Equal(3, getUsersResult.Data.Count);
            Assert.Equal(1, getUsersResult.Data[0].Id);
            Assert.Equal(2, getUsersResult.Data[1].Id);
            Assert.Equal(3, getUsersResult.Data[2].Id);
        }
    }
}
