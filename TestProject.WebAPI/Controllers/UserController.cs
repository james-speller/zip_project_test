namespace TestProject.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TestProject.Business.Services;
    using TestProject.WebAPI.Models;
    using TestProject.WebAPI.Extensions;
    using AutoMapper;
    using TestProject.Data.Models;

    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;

        private readonly IUserService userService;

        private readonly IMapper mapper;

        public UserController(ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper)
        {
            this.logger = logger;
            this.userService = userService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <remarks>
        /// Attempts to match a <see cref="User"/> by their Id, if a match is found it will be returned as a <see cref="UserViewModel"/>.
        /// If there is not match, then a null will be returned.
        /// </remarks>
        /// <param name="userId">The Id of the <see cref="User"/> to retrieve.</param>
        /// <returns>Returns a <see cref="UserViewModel"/>.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var getUserResult = this.userService.GetUserById(userId);
                return this.FromResult<User, UserViewModel>(getUserResult, mapper);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "UserController.GetUser encountered an unexpected error");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// List all Users
        /// </summary>
        /// <remarks>
        /// Gets a <see cref="List{UserViewModel}"/> which coantains all the users in the system. If no results exist an empty
        /// list will be returned.
        /// </remarks>
        /// <returns>Returns a <see cref="List{UserViewModel}"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            try
            {
                var getUsersResult = this.userService.GetUsers();
                return this.FromResult<List<User>, List<UserViewModel>>(getUsersResult, mapper);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "UserController.ListUsers encountered an unexpected error");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <remarks>
        /// Takes a <see cref="UserViewModel"/> and attempts to persist in the database. If the User's name or email are empty
        /// then an invalid response will be sent and the User will not be created. If the User's salary or expenses are not positive
        /// integers then the User will not be created and an invalid response will be sent. If the User's email already exists
        /// in the system then the User will not be created an a conflict response will be sent. If the User is successfully created
        /// then the Id automatically generated for them by the database will be returned.
        /// </remarks>
        /// <param name="user">The <see cref="UserViewModel"/> to Create.</param>
        /// <returns>Returns User Id as int.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel user)
        {
            try
            {
                var userModel = this.mapper.Map<User>(user);
                var createAccountResult = this.userService.CreateUser(userModel);
                return this.FromResult(createAccountResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "UserController.CreateUser encountered an unexpected error");
                return this.StatusCode(500);
            }
        }
    }
}
