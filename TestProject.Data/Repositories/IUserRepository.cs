namespace TestProject.Data.Repositories
{
    using System.Collections.Generic;
    using TestProject.Data.Models;

    public interface IUserRepository
    {
        int CreateUser(User user);

        User GetUserById(int id);

        User GetUserByEmail(string email);

        List<User> GetUsers();
    }
}
