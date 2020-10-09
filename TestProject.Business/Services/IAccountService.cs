namespace TestProject.Business.Services
{
    using System.Collections.Generic;
    using TestProject.Business.Models;
    using TestProject.Data.Models;

    public interface IAccountService
    {
        /// <summary>
        /// This method takes the Id of a <see cref="User"/> and attempts to create an <see cref="Account"/> for them.
        /// If the User isn't matched then the method will return a not found response and the Account will not be created. 
        /// If the User already has an account then a conflict response will be returned and the Account will not be 
        /// created. If the User is matched, but their monthly salary - their monthly expenses is less than the credit limit
        /// value in the appsettings.json then an invalid response will be returned and the Account will not be created.
        /// </summary>
        /// <param name="userId">The id of the <see cref="User"/> who will own the created <see cref="Account"/>.</param>
        /// <returns>Returns a <see cref="Result{int}"/>.</returns>
        Result<int> CreateAccount(int userId);

        /// <summary>
        /// This method will return a list of all <see cref="Account"/> records in the system. If no Accounts exist then
        /// this method will return an empty list.
        /// </summary>
        /// <returns>Returns a <see cref="Result{List{Account}}"/>.</returns>
        Result<List<Account>> GetAccounts();
    }
}
