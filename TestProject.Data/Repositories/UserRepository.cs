namespace TestProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TestProject.Data.Models;

    public class UserRepository : IUserRepository
    {
        private readonly TestProjectContext context;

        public UserRepository(TestProjectContext context)
        {
            this.context = context;
        }

        public int CreateUser(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
            return user.Id;
        }

        public User GetUserByEmail(string email)
        {
            return this.context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsers()
        {
            return this.context.Users.ToList();
        }
    }
}
