using System.Data;
using System.Data.SqlClient;
using TenantScoreSheet.Models;
using static TenantScoreSheet.Repository.SecurePassword;

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
                using (cmd = new SqlCommand("spGetUserLogin", sqlcon))
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

        /// <summary>
        /// GetAllUsers method retrieves a list of managers from the database by executing a stored procedure "sp_GetAllUsers".
        /// </summary>
        /// <returns>A list of Users objects representing the managers in the system. If no managers are found, an empty list is returned.</returns>
        public List<Users> GetAllUsers()
        {
            List<Users> userslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetAllUsers", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Users Objuser = new()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            UserName = Convert.ToString(row["UserName"]),
                            RoleId = Convert.ToInt32(row["RoleId"]),
                            FirstName = Convert.ToString(row["FirstName"]),
                            LastName = Convert.ToString(row["LastName"]),
                            MiddleName = Convert.ToString(row["MiddleName"]),
                            Email = Convert.ToString(row["Email"]),
                            PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                            Status = Convert.ToString(row["UserName"]),
                            CreatedBy = Convert.ToString(row["CreatedBy"]),
                            CreatedDate = Convert.ToDateTime(row["CreatedDate"])

                        };

                        userslist.Add(Objuser);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return userslist;
        }

        /// <summary>
        /// CreateUser method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateUser(Users objuser)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", objuser.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", objuser.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", objuser.LastName);
                    cmd.Parameters.AddWithValue("@UserName", objuser.UserName);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword(objuser.Password));
                    cmd.Parameters.AddWithValue("@Email", objuser.Email);
                    cmd.Parameters.AddWithValue("@Phone", objuser.Phone);
                    cmd.Parameters.AddWithValue("@Address", objuser.Address);
                    cmd.Parameters.AddWithValue("@Status", objuser.Status);
                    cmd.Parameters.AddWithValue("@RoleId", objuser.RoleId);

                    cmd.Parameters.AddWithValue("@CreatedBy", objuser.Id);
                    sqlcon.Open();

                    cmd.ExecuteNonQuery();
                    Result = true;
                }
            }
            catch (Exception)
            {
                Result = false;
                throw;
            }
            finally
            {
                sqlcon.Close();

            }
            return Result;
        }


        /// <summary>
        /// RegisterUser method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool RegisterUser(Users objuser)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spRegisterUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", objuser.UserName);
                    cmd.Parameters.AddWithValue("@Email", objuser.Email);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword(objuser.Password));
                    cmd.Parameters.AddWithValue("@RoleId", objuser.RoleId);
                    sqlcon.Open();

                    cmd.ExecuteNonQuery();
                    Result = true;
                }
            }
            catch (Exception)
            {
                Result = false;
                throw;
            }
            finally
            {
                sqlcon.Close();

            }
            return Result;
        }
        /// <summary>
        /// UpdateUser method is used to update existing user records in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user to be updated.</param>
        /// <returns>Returns a boolean value indicating whether the user update operation was successful or not.</returns>
        public bool UpdateUser(Users objuser)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", objuser.Id);
                    cmd.Parameters.AddWithValue("@FirstName", objuser.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", objuser.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", objuser.LastName);
                    cmd.Parameters.AddWithValue("@UserName", objuser.UserName);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword(objuser.Password));
                    cmd.Parameters.AddWithValue("@Email", objuser.Email);
                    cmd.Parameters.AddWithValue("@Phone", objuser.Phone);
                    cmd.Parameters.AddWithValue("@Address", objuser.Address);
                    cmd.Parameters.AddWithValue("@Status", objuser.Status);
                    cmd.Parameters.AddWithValue("@RoleId", objuser.RoleId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", objuser.Id);

                    sqlcon.Open();

                    cmd.ExecuteNonQuery();
                    Result = true;
                }
            }
            catch (Exception)
            {
                Result = false;
                throw;
            }
            finally
            {
                sqlcon.Close();

            }
            return Result;
        }



        /// <summary>
        /// EncryptPassword method is used to encrypt a given password using the SecurePassword class with BASE64 encoding.
        /// </summary>
        /// <param name="strPassword">The password to be encrypted.</param>
        /// <returns>A string representing the encrypted password. If the input password is null or empty, an empty string is returned.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException exception is thrown, if the input password is null or empty.</exception>
        public string EncryptPassword(string strPassword)
        {
            string strReturnPassword = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(strPassword))
                {
                    throw new ArgumentNullException("Password should not be null or empty...");
                }
                else
                {
                    strReturnPassword = SecurePassword.EncryptPassword(strPassword, SecurePassword.EncDecType.BASE64);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strReturnPassword;
        }


        /// <summary>
        /// DecryptPassword method is used to decrypt a given password using the SecurePassword class with BASE64 decoding.
        /// </summary>
        /// <param name="strPassword">The encrypted password to be decrypted.</param>
        /// <returns>A string representing the decrypted password. If the input password is null or empty, an empty string is returned.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException exception is thrown, if the input password is null or empty.</exception>
        public string DecryptPassword(string? strPassword)
        {
            string strReturnPassword;

            try
            {
                if (string.IsNullOrEmpty(strPassword))
                {
                    throw new ArgumentNullException(null, "Password should not be null or empty...");
                }
                else
                {
                    strReturnPassword = SecurePassword.DecryptPassword(strPassword, SecurePassword.EncDecType.BASE64);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strReturnPassword;
        }

        /// <summary>
        /// GetUserByEmail method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public async Task<bool> GetUsersDetailsByEmail(int Id, string UserName)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spGetUserDetailsByEmail", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Email", UserName);
                    da = new SqlDataAdapter(cmd);
                    await Task.Run(() => da.Fill(dt));
                }

                if (dt.Rows.Count > 0)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception)
            {
                Result = false;
            }
            finally
            {
                sqlcon.Close();
            }

            return Result;
        }
    }

}
