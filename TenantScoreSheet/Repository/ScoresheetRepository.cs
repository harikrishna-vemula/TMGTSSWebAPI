using System.Data;
using System.Data.SqlClient;
using TenantScoreSheet.Models;
using static TenantScoreSheet.Repository.SecurePassword;

namespace TenantScoreSheet.Repository
{
    public class ScoresheetRepository
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
        public ScoresheetRepository(IConfiguration configuration, SqlConnection Sqlcon)
        {
            Configuration = configuration;
            Sqlcon = new SqlConnection(Configuration.GetConnectionString("TMGConnectionStr").ToString());
            //RCMUrl = Configuration.GetSection("ConnectionStrings").GetSection("RCMUrl").Value.ToString();
            sqlcon = Sqlcon;
            //EmailAddress = Configuration.GetSection("SMTPEmail").GetSection("EmailAddress").Value.ToString();
            //Password = Configuration.GetSection("SMTPEmail").GetSection("Password").Value.ToString();
        }

        /// <summary>
        /// GetAllUsers method retrieves a list of managers from the database by executing a stored procedure "sp_GetAllUsers".
        /// </summary>
        /// <returns>A list of Users objects representing the managers in the system. If no managers are found, an empty list is returned.</returns>
        public List<ApplicantInfo> GetAllApplicants()
        {
            List<ApplicantInfo> applicantslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetApplicants", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ApplicantInfo Objuser = new()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            ApplicantName = Convert.ToString(row["ApplicantName"]),
                            Property = Convert.ToString(row["Property"]),
                            ApplicantTypeId = Convert.ToInt32(row["ApplicantTypeId"]),
                            City = Convert.ToString(row["City"]),
                            State = Convert.ToString(row["State"]),
                            Zip = Convert.ToString(row["Zip"]),
                            MonthlyRent = Convert.ToString(row["MonthlyRent"]),
                            Section8Rent = Convert.ToString(row["Section8Rent"]),
                            StandardDepositProperty = Convert.ToString(row["StandardDepositProperty"]),
                            PetDeposit = Convert.ToString(row["PetDeposit"]),
                            PropertyTypeId = Convert.ToInt32(row["PropertyTypeId"]),
                            PropertyType = Convert.ToString(row["PropertyType"]),
                            ApplicantType = Convert.ToString(row["ApplicantType"]),
                            CreatedBy = Convert.ToString(row["CreatedBy"]),
                            CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                        };

                        applicantslist.Add(Objuser);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return applicantslist;
        }


        /// <summary>
        /// CreateApplicant method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateApplicant(ApplicantInfo objApplicant)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertApplicant", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantName", objApplicant.ApplicantName);
                    cmd.Parameters.AddWithValue("@Property", objApplicant.Property);
                    cmd.Parameters.AddWithValue("@ApplicantTypeId", objApplicant.ApplicantTypeId);
                    cmd.Parameters.AddWithValue("@City", objApplicant.City);
                    cmd.Parameters.AddWithValue("@State", objApplicant.State);
                    cmd.Parameters.AddWithValue("@Zip", objApplicant.Zip);
                    cmd.Parameters.AddWithValue("@MonthlyRent", objApplicant.MonthlyRent);
                    cmd.Parameters.AddWithValue("@Section8Rent", objApplicant.Section8Rent);
                    cmd.Parameters.AddWithValue("@StandardDepositProperty", objApplicant.StandardDepositProperty);
                    cmd.Parameters.AddWithValue("@PetDeposit", objApplicant.PetDeposit);
                    cmd.Parameters.AddWithValue("@PropertyTypeId", objApplicant.PropertyTypeId);
                    cmd.Parameters.AddWithValue("@CreatedBy", objApplicant.Id);
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
        /// CreateApplicant method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateApplicant(ApplicantInfo objApplicant)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateApplicant", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantName", objApplicant.ApplicantName);
                    cmd.Parameters.AddWithValue("@Property", objApplicant.Property);
                    cmd.Parameters.AddWithValue("@ApplicantTypeId", objApplicant.ApplicantTypeId);
                    cmd.Parameters.AddWithValue("@City", objApplicant.City);
                    cmd.Parameters.AddWithValue("@State", objApplicant.State);
                    cmd.Parameters.AddWithValue("@Zip", objApplicant.Zip);
                    cmd.Parameters.AddWithValue("@MonthlyRent", objApplicant.MonthlyRent);
                    cmd.Parameters.AddWithValue("@Section8Rent", objApplicant.Section8Rent);
                    cmd.Parameters.AddWithValue("@StandardDepositProperty", objApplicant.StandardDepositProperty);
                    cmd.Parameters.AddWithValue("@PetDeposit", objApplicant.PetDeposit);
                    cmd.Parameters.AddWithValue("@PropertyTypeId", objApplicant.PropertyTypeId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", objApplicant.Id);
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
    }
}
