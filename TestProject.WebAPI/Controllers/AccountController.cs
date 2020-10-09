namespace TestProject.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TestProject.Business.Services;
    using TestProject.Data.Models;
    using TestProject.WebAPI.Extensions;

    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAccountService accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            this.logger = logger;
            this.accountService = accountService;
        }

        /// <summary>
        /// Create an Account
        /// </summary>
        /// <remarks>
        /// Takes the Id of a <see cref="User"/> and attempts to create them an <see cref="Account"/>. If the User's salary - expenses is
        /// less than the configured credit limit the system will not allow them to create an account.
        /// </remarks>
        /// <param name="userId">The Id of the <see cref="User"/> to create the <see cref="Account"/> for.</param>
        /// <returns>Returns the Id of the created user as an int.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount(int userId)
        {
            try
            {
                var createAccountResult = this.accountService.CreateAccount(userId);
                return this.FromResult(createAccountResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "AccountController.CreateAccount encountered an unexpected error");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// List all Accounts
        /// </summary>
        /// <remarks>
        /// Gets a <see cref="List{AccoutViewModel}"/> which contains all the accounts in the system. If no results exist an empty
        /// list will be returned.
        /// </remarks>
        /// <returns>Returns a <see cref="List{AccoutViewModel}"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> ListAccounts()
        {
            try
            {
                var getAccountsResult = this.accountService.GetAccounts();
                return this.FromResult(getAccountsResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "AccountController.ListAccounts encountered an unexpected error");
                return this.StatusCode(500);
            }
        }
    }
}
