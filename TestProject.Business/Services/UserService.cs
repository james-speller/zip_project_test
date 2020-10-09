namespace TestProject.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Business.Models;
    using TestProject.Data.Models;
    using TestProject.Data.Repositories;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <inheritdoc/>
        public Result<int> CreateUser(User user)
        {
            // Validate User record and if there are any problems return all of them so User can resolve them all at once.
            var validationErrors = this.ValidateUser(user);
            if (validationErrors.Any())
            {
                return new InvalidResult<int>(validationErrors);
            }

            // Try and match the User's email to ensure that we don't create duplicates.
            // James note, in a real world scenario I would probably confirm with a product owner and internal security experts
            // whether we should return a message that hides that we already have a user in the system with that email so
            // people can't just trawl through and get the emails of everyone using the system.
            var existingUser = this.userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return new ConflictResult<int>($"A User with Id {existingUser.Id} already exists for this email.");
            }

            // Create the User record and return the resulting Id.
            var userId = this.userRepository.CreateUser(user);
            return new SuccessResult<int>(userId);
        }

        /// <inheritdoc/>
        public Result<User> GetUserById(int id)
        {
            var user = this.userRepository.GetUserById(id);
            if (user == null)
            {
                return new NotFoundResult<User>($"No User was found for Id: {id}.");
            }
            else
            {
                return new SuccessResult<User>(user);
            }
        }

        /// <inheritdoc/>
        public Result<List<User>> GetUsers()
        {
            return new SuccessResult<List<User>>(this.userRepository.GetUsers());
        }

        private List<string> ValidateUser(User user)
        {
            var validationErrors = new List<string>();

            // Validate User's monthly salary is a positive number greater than 0.
            if (!(user.MonthlySalary > 0))
            {
                validationErrors.Add("User's monthly salary must be a positive number greater than 0.");
            }

            // Validate User's monthly expenses is a positive number greater than 0.
            if (!(user.MonthlyExpenses > 0))
            {
                validationErrors.Add("User's monthly expenses must be a positive number greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                validationErrors.Add("User's name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                validationErrors.Add("User's email cannot be empty.");
            }

            return validationErrors;
        }
    }
}
