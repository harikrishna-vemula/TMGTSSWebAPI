using System.Data;
using System.Data.SqlClient;
using TenantScoreSheet.Models;

namespace TenantScoreSheet.Repository
{
    public class UserRepository
    {
        public readonly IConfiguration Configuration;
        SqlCommand? cmd;
        SqlConnection sqlcon;
        string EmailAddress, Password, RCMUrl;
        DataTable dt = new DataTable();
        SqlDataAdapter? da;

        /// <summary>
        /// UserServiceRepository constructor class, responsible for initializing the repository
        /// with the required configuration and database connection.
        /// </summary>
        /// <param name="configuration">The IConfiguration object used to access configuration settings from the app settings.</param>
        /// <param name="Sqlcon">The pre-existing SqlConnection object passed as a parameter to the constructor.</param>
        public UserRepository(IConfiguration configuration, SqlConnection Sqlcon)
        {
            Configuration = configuration;
            Sqlcon = new SqlConnection(Configuration.GetConnectionString("TMGConnectionStr").ToString());
            //RCMUrl = Configuration.GetSection("ConnectionStrings").GetSection("RCMUrl").Value.ToString();
            sqlcon = Sqlcon;
            //EmailAddress = Configuration.GetSection("SMTPEmail").GetSection("EmailAddress").Value.ToString();
            //Password = Configuration.GetSection("SMTPEmail").GetSection("Password").Value.ToString();
        }

        /// <summary>
        /// CheckLoginUser is used to check user login credentials by querying a database stored procedure.
        /// </summary>
        /// <param name="Email">The email address provided by the user for login.</param>
        /// <param name="Password">The user's password provided for login (the password is encrypted before querying the database).</param>
        /// <param name="Token">An optional token provided for user authentication purposes.</param>
        /// <returns>
        /// Users: An object representing the user's details (if found in the database), containing properties such as Id,
        /// UserName, Email, RoleId, RoleName and Image.
        /// </returns>
        public async Task<Users> CheckLoginUser(string Email, string Password)
        {
            Users objuser = new Users();
            try
            {
                using (cmd = new SqlCommand("sp_GetUserLogin", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    //cmd.Parameters.AddWithValue("@Token", Token);
                    da = new SqlDataAdapter(cmd);
                    await Task.Run(() => da.Fill(dt));
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objuser.Id = Convert.ToInt32(dr["Id"]);
                        objuser.UserName = Convert.ToString(dr["UserName"]);
                        objuser.Email = Convert.ToString(dr["Email"]);
                        objuser.RoleId = Convert.ToInt32(dr["RoleId"]);
                        objuser.RoleName = Convert.ToString(dr["RoleName"]);
                        
                        objuser.Status = Convert.ToString(dr["Status"]);
                    }
                }
                else
                {
                    // Handle invalid login case here
                }
            }
            catch (Exception)
            {
                throw;
            }

            return objuser;
        }
    }

}
