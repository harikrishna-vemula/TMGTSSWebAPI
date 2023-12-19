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
    public class ScoresheetController : Controller
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
        public ScoresheetController(IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }
    }
}
