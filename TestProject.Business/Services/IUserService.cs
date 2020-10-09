namespace TestProject.Business.Services
{
    using System.Collections.Generic;
    using TestProject.Business.Models;
    using TestProject.Data.Models;

    public interface IUserService
    {
        /// <summary>
        /// This method will attempt to create a <see cref="User"/> and return the id of the created record. If a
        /// User with a matching email already exists in the database then the method will return a conflict response and
        /// the User will not be created. If there are any validation errors the method will return an invalid 
        /// response and the User will not be created.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to persist in the database.</param>
        /// <returns>Returns a <see cref="Result{int}"/>.</returns>
        Result<int> CreateUser(User user);

        /// <summary>
        /// This method will attempt to match a <see cref="User"/> record in the database by it's Id. If there is a
        /// match it will be returned, if there is no match then the method will return a not found response.
        /// </summary>
        /// <param name="id">The id of the <see cref="User"/> to retrieve.</param>
        /// <returns>Returns a <see cref="Result{User}"/>.</returns>
        Result<User> GetUserById(int id);

        /// <summary>
        /// This method will return a list of all <see cref="User"/> records in the system. If there are no matches
        /// then an empty list will be returned.
        /// </summary>
        /// <returns>Returns a <see cref="Result{List{User}}"/>.</returns>
        Result<List<User>> GetUsers();
    }
}
