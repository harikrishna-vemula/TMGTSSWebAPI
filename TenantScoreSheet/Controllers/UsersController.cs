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

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<Dictionary<string, object>> CreateUser([FromBody] Users value)
        {
            Dictionary<string, object> response = new();
            UserRepository userServiceRepository = new(configuration, connection);
            try
            {
                bool userExisted = false;

                userExisted = await userServiceRepository.GetUsersDetailsByEmail(0,value.Email);

                if (userExisted == false)
                {
                    bool isUserExist = userServiceRepository.CreateUser(value);
                    if (isUserExist == true)
                    {
                        response.Add("Status", "Success");
                        response.Add("Message", "User is added successfully...");
                    }
                    else
                    {
                        response.Add("Status", "Error");
                        response.Add("Message", "There is something happend while inserting record");
                    }
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "User Name is Already Exists");
                }
            }
            catch (Exception ex)
            {
                response.Add("status", "Error");
                response.Add("Message", ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="value">The Users object containing the updated details of the user.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<Dictionary<string, object>> UpdateUser([FromBody] Users value)
        {
            Dictionary<string, object> response = new();
            UserRepository userServiceRepository = new(configuration, connection);
            try
            {
                //bool userExisted = await userServiceRepository.GetUsersDetailsByEmail(value.Id, value.Email);
                //if (userExisted == false)
                //{
                    bool result = userServiceRepository.UpdateUser(value);
                    if (result == true)
                    {
                        response.Add("Status", "Success");
                        response.Add("Message", "User is Updated successfully...");
                    }
                    else
                    {
                        response.Add("Status", "Error");
                        response.Add("Message", "There is something happend while updating record");
                    }
                //}
                //else
                //{
                //    response.Add("Status", "Error");
                //    response.Add("Message", "User Name is Already Existed For another Id");
                //}
            }
            catch (Exception ex)
            {
                response.Add("status", "Error");
                response.Add("Message", ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public List<Users> GetAllUsers()
        {
            UserRepository userServiceRepository = new(configuration, connection);
            List<Users> usersList;
            try
            {
                usersList = userServiceRepository.GetAllUsers();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return usersList;
        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<Dictionary<string, object>> RegisterUser([FromBody] Users value)
        {
            Dictionary<string, object> response = new();
            UserRepository userServiceRepository = new(configuration, connection);
            try
            {
                bool userExisted = false;

                userExisted = await userServiceRepository.GetUsersDetailsByEmail(0, value.Email);

                if (userExisted == false)
                {
                    bool isUserExist = userServiceRepository.RegisterUser(value);
                    if (isUserExist == true)
                    {
                        response.Add("Status", "Success");
                        response.Add("Message", "User is added successfully...");
                    }
                    else
                    {
                        response.Add("Status", "Error");
                        response.Add("Message", "There is something happend while inserting record");
                    }
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "User Name is Already Exists");
                }
            }
            catch (Exception ex)
            {
                response.Add("status", "Error");
                response.Add("Message", ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetUserDetailsById/{Id}")]
        public List<Users> GetUserDetailsById(int? Id)
        {
            UserRepository userServiceRepository = new(configuration, connection);
            List<Users> usersList;
            try
            {
                usersList = userServiceRepository.GetUserDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return usersList;
        }
    }
}
