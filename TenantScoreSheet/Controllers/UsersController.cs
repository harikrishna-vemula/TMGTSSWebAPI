using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TenantScoreSheet.Models;
using TenantScoreSheet.Repository;

namespace TenantScoreSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public readonly IConfiguration configuration;
        public readonly SqlConnection? connection;

        /// <summary>
        /// Constructor for the UserController class.
        /// </summary>
        /// <param name="configuration">An instance of IConfiguration used to access configuration settings.</param>
        /// <param name="_webHostEnvironment">An instance of IWebHostEnvironment representing the current web host environment.</param>
        /// <param name="config">An instance of IConfiguration used to access additional configuration settings.</param>
        public UsersController(IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet("{Email}/{Password}", Name = "GetUserDetails")]
        public async Task<Dictionary<string, object>> Get(string Email, string Password)
        {
            UserRepository? userServiceRepository = new(configuration, connection);
            var responseDictionary = new Dictionary<string, object>();
            try
            {
                //var jwtService = new Authentication.JwtService(configuration);
                //var securityToken = jwtService.GenerateSecurityToken(Email);
                Users loggedInUser = await userServiceRepository.CheckLoginUser(Email, Password);
                if (loggedInUser.Id != 0)
                {
                    responseDictionary.Add("Status", "Success");
                    responseDictionary.Add("Message", "Valid User Credential...");
                   // responseDictionary.Add("token", securityToken);
                    responseDictionary.Add("users", loggedInUser);
                }
               
                else
                {
                    responseDictionary.Add("Status", "Error");
                    responseDictionary.Add("Message", "Invalid User Credentials...");
                }
            }
            catch (Exception ex)
            {
                responseDictionary.Add("Status", "Error");
                responseDictionary.Add("Message", ex.Message);
            }
            finally
            {
            }
            return responseDictionary;
        }
    }
}
