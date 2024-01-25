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
        /// GetAllApplicants method retrieves a list of managers from the database by executing a stored procedure "spGetApplicants".
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

                            Id = row["Id"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["Id"]),
                            ApplicantName = row["ApplicantName"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicantName"]),
                            ApplicationStatusId = row["ApplicationStatusId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["ApplicationStatusId"]),
                            ApplicationStatus = row["ApplicationStatus"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicationStatus"]),

                            Property = row["Property"] == DBNull.Value ? null : System.Convert.ToString(row["Property"]),
                            ApplicantTypeId = row["ApplicantTypeId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["ApplicantTypeId"]),
                            City = row["City"] == DBNull.Value ? null : System.Convert.ToString(row["City"]),
                            State = row["State"] == DBNull.Value ? null : System.Convert.ToString(row["State"]),
                            Zip = row["Zip"] == DBNull.Value ? null : System.Convert.ToString(row["Zip"]),
                            MonthlyRent = row["MonthlyRent"] == DBNull.Value ? null : System.Convert.ToDouble(row["MonthlyRent"]),
                            Section8Rent = row["Section8Rent"] == DBNull.Value ? null : System.Convert.ToDouble(row["Section8Rent"]),
                            StandardDepositProperty = row["StandardDepositProperty"] == DBNull.Value ? null : System.Convert.ToDouble(row["StandardDepositProperty"]),
                            PetDeposit = row["PetDeposit"] == DBNull.Value ? null : System.Convert.ToDouble(row["PetDeposit"]),
                            PropertyTypeId = row["PropertyTypeId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["PropertyTypeId"]),
                            PropertyType = row["PropertyType"] == DBNull.Value ? null : System.Convert.ToString(row["PropertyType"]),
                            ApplicantType = row["ApplicantType"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicantType"]),
                            CreatedBy = row["CreatedBy"] == DBNull.Value ? null : System.Convert.ToString(row["CreatedBy"]),
                            CreatedDate = row["CreatedDate"] == DBNull.Value ? null : System.Convert.IsDBNull(row["CreatedDate"]) ? null : Convert.ToDateTime(row["CreatedDate"]),
                            ModifiedDate = row["ModifiedDate"] == DBNull.Value ? null : System.Convert.IsDBNull(row["ModifiedDate"]) ? null : Convert.ToDateTime(row["ModifiedDate"])
                            //CreatedDate = row["CreatedDate"] == null ? null : Convert.ToDateTime(row["CreatedDate"]),
                            //    ModifiedDate = row["ModifiedDate"] == null ? null : Convert.ToDateTime(row["ModifiedDate"])

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
        /// GetUserByEmail method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public async Task<bool> GetApplicantByName(string ApplicantName)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spGetApplicantByName", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AppliacantName", ApplicantName);
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


        /// <summary>
        /// CreateApplicant method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a id value indicating whether the user creation operation was successful or not.</returns>
        public int CreateApplicant(ApplicantInfo objApplicant)
        {
            bool Result = false; int Id = 0;
            try
            {
                using (cmd = new SqlCommand("spInsertApplicant", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantName", objApplicant.ApplicantName);
                    cmd.Parameters.AddWithValue("@ApplicantId", objApplicant.ApplicantId);
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
                    cmd.Parameters.AddWithValue("@CreatedBy", objApplicant.CreatedBy);
                    cmd.Parameters.AddWithValue("@ApplicationStatusId", objApplicant.ApplicationStatusId);
                    cmd.Parameters.AddWithValue("@Id", objApplicant.Id);
                    //cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                    Id = cmd.Parameters["@Id"].Value is DBNull ? 0 : Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    Result = true;
                }
            }
            catch (Exception ex)
            {

                Id = 0;
                throw;
            }
            finally
            {
                sqlcon.Close();

            }
            return Id;
        }

        /// <summary>
        /// CreateApplicant method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a id value indicating whether the user creation operation was successful or not.</returns>
        public int CreateTenantInfo(int? ApplicantId, int? TenantSNo, int? CreatedBy, string ApplicantName)
        {
            bool Result = false; int TenantId = 0;
            try
            {
                using (cmd = new SqlCommand("spInsertTenantInfo", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantId", ApplicantId);
                    cmd.Parameters.AddWithValue("@TenantSNo", TenantSNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@ApplicantName", ApplicantName);
                    cmd.Parameters.Add("@TenantId", SqlDbType.Int);
                    cmd.Parameters["@TenantId"].Direction = ParameterDirection.Output;
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                    TenantId = cmd.Parameters["@TenantId"].Value is DBNull ? 0 : Convert.ToInt32(cmd.Parameters["@TenantId"].Value);
                    Result = true;
                }
            }
            catch (Exception)
            {
                TenantId = 0;
                throw;
            }
            finally
            {
                sqlcon.Close();

            }
            return TenantId;
        }

        /// <summary>
        /// UpdateApplicant method is used to create new users in the system.
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
                    cmd.Parameters.AddWithValue("@ModifiedBy", objApplicant.ModifiedBy);
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
        /// GetAllApplicants method retrieves a list of managers from the database by executing a stored procedure "spGetApplicants".
        /// </summary>
        /// <returns>A list of Users objects representing the managers in the system. If no managers are found, an empty list is returned.</returns>
        public List<IncomeVerfication> GetIncomeVerification(int? TenantId, int? ApplicantId, int? TenantSerialNumber)
        {
            List<IncomeVerfication> applicantslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetIncomeVerification", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", TenantId);
                    cmd.Parameters.AddWithValue("@ApplicantId", ApplicantId);
                    cmd.Parameters.AddWithValue("@TenantSerialNumber", TenantSerialNumber);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IncomeVerfication Objuser = new()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            TenantId = Convert.ToInt32(row["TenantId"]),
                            PaystubRecent = row["PaystubRecent"] == DBNull.Value ? null : System.Convert.ToDouble(row["PaystubRecent"]),
                            
                            YTD_Earnings = row["YTD_Earnings"] == DBNull.Value ? null : System.Convert.ToInt32(row["YTD_Earnings"]),
                            
                            PaystubRecentMonthly = row["PaystubRecentMonthly"] == DBNull.Value ? null : System.Convert.ToDouble(row["PaystubRecentMonthly"]),
                            BankStatement = row["BankStatement"] == DBNull.Value ? null : System.Convert.ToInt32(row["BankStatement"]),
                            secondPayStub = row["secondPayStub"] == DBNull.Value ? null : System.Convert.ToDouble(row["secondPayStub"]),
                            BankStatementMonthly = row["BankStatementMonthly"] == DBNull.Value ? null : System.Convert.ToDouble(row["BankStatementMonthly"]),
                            xRent = row["xRent"] == DBNull.Value ? null : System.Convert.ToDouble(row["xRent"]),
                            IncomeAdequate = Convert.ToBoolean(row["IncomeAdequate"]),
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
        /// CreateIncomeVerfication method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateIncomeVerfication(IncomeVerfication objIncomeVerfication)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertIncomeVerification", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objIncomeVerfication.TenantId);
                    cmd.Parameters.AddWithValue("@PaystubRecent", objIncomeVerfication.PaystubRecent);
                    cmd.Parameters.AddWithValue("@YTD_Earnings", objIncomeVerfication.YTD_Earnings);
                    cmd.Parameters.AddWithValue("@PaystubRecentMonthly", objIncomeVerfication.PaystubRecentMonthly);
                    cmd.Parameters.AddWithValue("@BankStatement", objIncomeVerfication.BankStatement);
                    cmd.Parameters.AddWithValue("@secondPayStub", objIncomeVerfication.secondPayStub);
                    cmd.Parameters.AddWithValue("@BankStatementMonthly", objIncomeVerfication.BankStatementMonthly);
                    cmd.Parameters.AddWithValue("@xRent", objIncomeVerfication.xRent);
                    cmd.Parameters.AddWithValue("@IncomeAdequate", objIncomeVerfication.IncomeAdequate);
                    cmd.Parameters.AddWithValue("@CreatedBy", objIncomeVerfication.CreatedBy);
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
        /// UpdateApplicant method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateIncomeVerfication(IncomeVerfication objIncomeVerfication)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateIncomeVerfication", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objIncomeVerfication.TenantId);
                    cmd.Parameters.AddWithValue("@PaystubRecent", objIncomeVerfication.PaystubRecent);
                    cmd.Parameters.AddWithValue("@YTD_Earnings", objIncomeVerfication.YTD_Earnings);
                    cmd.Parameters.AddWithValue("@PaystubRecentMonthly", objIncomeVerfication.PaystubRecentMonthly);
                    cmd.Parameters.AddWithValue("@BankStatement", objIncomeVerfication.BankStatement);
                    cmd.Parameters.AddWithValue("@secondPayStub", objIncomeVerfication.secondPayStub);
                    cmd.Parameters.AddWithValue("@BankStatementMonthly", objIncomeVerfication.BankStatementMonthly);
                    cmd.Parameters.AddWithValue("@xRent", objIncomeVerfication.xRent);
                    cmd.Parameters.AddWithValue("@IncomeAdequate", objIncomeVerfication.IncomeAdequate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", objIncomeVerfication.ModifiedBy);
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
        /// CreateCreditSummary method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateCreditSummary(CreditSummary objCreditSummary)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertCreditSummary", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenantId", objCreditSummary.TenantId);
                    cmd.Parameters.AddWithValue("@CreditLines", objCreditSummary.CreditLines);
                    cmd.Parameters.AddWithValue("@CreditScore", objCreditSummary.CreditScore);
                    cmd.Parameters.AddWithValue("@CreditScorePoints", objCreditSummary.CreditScorePoints);
                    cmd.Parameters.AddWithValue("@CreditScoreAvailable", objCreditSummary.CreditScoreAvailable);
                    cmd.Parameters.AddWithValue("@CreditScoreAvailablePoints", objCreditSummary.CreditScoreAvailablePoints);
                    cmd.Parameters.AddWithValue("@AccountPastDue60Days", objCreditSummary.AccountPastDue60Days);
                    cmd.Parameters.AddWithValue("@CollectionAccounts", objCreditSummary.CollectionAccounts);
                    cmd.Parameters.AddWithValue("@CollectionAccountsPoints", objCreditSummary.CollectionAccountsPoints);

                    cmd.Parameters.AddWithValue("@MedicalCollections", objCreditSummary.MedicalCollections);
                    cmd.Parameters.AddWithValue("@PropertyRelatedHousingRecord", objCreditSummary.PropertyRelatedHousingRecord);
                    cmd.Parameters.AddWithValue("@PropertyRelatedHousingRecordPoints", objCreditSummary.PropertyRelatedHousingRecordPoints);
                    cmd.Parameters.AddWithValue("@Bankruptcy", objCreditSummary.Bankruptcy);
                    cmd.Parameters.AddWithValue("@BankruptcyPoints", objCreditSummary.BankruptcyPoints);
                    cmd.Parameters.AddWithValue("@BankRuptyActive", objCreditSummary.BankRuptyActive);
                    cmd.Parameters.AddWithValue("@BankRuptyActivePoints", objCreditSummary.BankRuptyActivePoints);
                    cmd.Parameters.AddWithValue("@LiensRepossessions", objCreditSummary.LiensRepossessions);
                    cmd.Parameters.AddWithValue("@LiensRepossessionsPoints", objCreditSummary.LiensRepossessionsPoints);
                    cmd.Parameters.AddWithValue("@EvectionHistory", objCreditSummary.EvectionHistory);
                    cmd.Parameters.AddWithValue("@EvectionHistoryPoints", objCreditSummary.EvectionHistoryPoints);
                    cmd.Parameters.AddWithValue("@Class1Felonies", objCreditSummary.Class1Felonies);
                    cmd.Parameters.AddWithValue("@Class1FeloniesPoints", objCreditSummary.Class1FeloniesPoints);
                    cmd.Parameters.AddWithValue("@Class2Felonies", objCreditSummary.Class2Felonies);
                    cmd.Parameters.AddWithValue("@Class2FeloniesPoints", objCreditSummary.Class2FeloniesPoints);
                    cmd.Parameters.AddWithValue("@Class1Misdemeaners", objCreditSummary.Class1Misdemeaners);
                    cmd.Parameters.AddWithValue("@Class1MisdemeanersPoints", objCreditSummary.Class1MisdemeanersPoints);

                    cmd.Parameters.AddWithValue("@Class2Misdemeaners", objCreditSummary.Class2Misdemeaners);
                    cmd.Parameters.AddWithValue("@Class2MisdemeanersPoints", objCreditSummary.Class2MisdemeanersPoints);
                    cmd.Parameters.AddWithValue("@DepositApproved", objCreditSummary.DepositApproved);
                    cmd.Parameters.AddWithValue("@DepositToHold", objCreditSummary.DepositToHold);

                    
                    cmd.Parameters.AddWithValue("@CreditApproved", objCreditSummary.CreditApproved);
                    cmd.Parameters.AddWithValue("@PaidByPrimaryTenant", objCreditSummary.PaidByPrimaryTenant);
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
        /// UpdateCreditSummary method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateCreditSummary(CreditSummary objCreditSummary)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateIncomeVerfication", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objCreditSummary.TenantId);
                    cmd.Parameters.AddWithValue("@CreditLines", objCreditSummary.CreditLines);
                    cmd.Parameters.AddWithValue("@CreditScore", objCreditSummary.CreditScore);
                    cmd.Parameters.AddWithValue("@CreditScorePoints", objCreditSummary.CreditScorePoints);
                    cmd.Parameters.AddWithValue("@CreditScoreAvailable", objCreditSummary.CreditScoreAvailable);
                    cmd.Parameters.AddWithValue("@CreditScoreAvailablePoints", objCreditSummary.CreditScoreAvailablePoints);
                    cmd.Parameters.AddWithValue("@AccountPastDue60Days", objCreditSummary.AccountPastDue60Days);
                    cmd.Parameters.AddWithValue("@CollectionAccounts", objCreditSummary.CollectionAccounts);
                    cmd.Parameters.AddWithValue("@CollectionAccountsPoints", objCreditSummary.CollectionAccountsPoints);

                    cmd.Parameters.AddWithValue("@MedicalCollections", objCreditSummary.MedicalCollections);
                    cmd.Parameters.AddWithValue("@PropertyRelatedHousingRecord", objCreditSummary.PropertyRelatedHousingRecord);
                    cmd.Parameters.AddWithValue("@PropertyRelatedHousingRecordPoints", objCreditSummary.PropertyRelatedHousingRecordPoints);
                    cmd.Parameters.AddWithValue("@BankRuptyActive", objCreditSummary.BankRuptyActive);
                    cmd.Parameters.AddWithValue("@BankRuptyActivePoints", objCreditSummary.BankRuptyActivePoints);
                    cmd.Parameters.AddWithValue("@LiensRepossessions", objCreditSummary.LiensRepossessions);
                    cmd.Parameters.AddWithValue("@LiensRepossessionsPoints", objCreditSummary.LiensRepossessionsPoints);

                    cmd.Parameters.AddWithValue("@EvectionHistoryPoints", objCreditSummary.EvectionHistoryPoints);
                    cmd.Parameters.AddWithValue("@Class1Felonies", objCreditSummary.Class1Felonies);
                    cmd.Parameters.AddWithValue("@Class1FeloniesPoints", objCreditSummary.Class1FeloniesPoints);
                    cmd.Parameters.AddWithValue("@Class2Felonies", objCreditSummary.Class2Felonies);
                    cmd.Parameters.AddWithValue("@Class2FeloniesPoints", objCreditSummary.Class2FeloniesPoints);
                    cmd.Parameters.AddWithValue("@Class1Misdemeaners", objCreditSummary.Class1Misdemeaners);
                    cmd.Parameters.AddWithValue("@Class1MisdemeanersPoints", objCreditSummary.Class1MisdemeanersPoints);

                    cmd.Parameters.AddWithValue("@Class2Misdemeaners", objCreditSummary.Class2Misdemeaners);
                    cmd.Parameters.AddWithValue("@Class2MisdemeanersPoints", objCreditSummary.Class2MisdemeanersPoints);
                    cmd.Parameters.AddWithValue("@DepositApproved", objCreditSummary.DepositApproved);
                    cmd.Parameters.AddWithValue("@DepositToHold", objCreditSummary.DepositToHold);


                    cmd.Parameters.AddWithValue("@ModifiedBy", objCreditSummary.ModifiedBy);
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
        /// CreateLandLordReferences method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateLandLordReferences(LandLordReferences objLandLordReferences)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertLandLordReferences", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objLandLordReferences.TenantId);
                    cmd.Parameters.AddWithValue("@RentalReferance", objLandLordReferences.RentalReferance);
                    cmd.Parameters.AddWithValue("@RentalReferancePoints", objLandLordReferences.RentalReferancePoints);
                    cmd.Parameters.AddWithValue("@LandlordType", objLandLordReferences.LL1LandlordType);
                    cmd.Parameters.AddWithValue("@LL1ProperNotice", objLandLordReferences.LL1ProperNotice);
                    cmd.Parameters.AddWithValue("@LL1ProperNoticePoints", objLandLordReferences.LL1ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1NSF", objLandLordReferences.LL1NSF);
                    cmd.Parameters.AddWithValue("@LL1NSFPoints", objLandLordReferences.LL1NSFPoints);
                    cmd.Parameters.AddWithValue("@LL1LatePayments", objLandLordReferences.LL1LatePayments);
                    cmd.Parameters.AddWithValue("@LL1LatePaymentsPoints", objLandLordReferences.LL1LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL1PaymentOrVacantNotices", objLandLordReferences.LL1PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL1TendayComplyNotice", objLandLordReferences.lL1TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL1TendayComplyNoticePoints", objLandLordReferences.lL1TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1HOAViolations", objLandLordReferences.LL1HOAViolations);
                    cmd.Parameters.AddWithValue("@LL1HOAViolationsPoints", objLandLordReferences.LL1HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@LL1PropertyCleanliness", objLandLordReferences.LL1PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@LL1PropertyCleanlinessPoints", objLandLordReferences.LL1PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@LL1Pets", objLandLordReferences.LL1Pets);
                    cmd.Parameters.AddWithValue("@LL1PetsPoints", objLandLordReferences.LL1PetsPoints);
                    cmd.Parameters.AddWithValue("@LL1AdversePetReferance", objLandLordReferences.LL1AdversePetReferance);
                    cmd.Parameters.AddWithValue("@LL1AdversePetReferancePoints", objLandLordReferences.LL1AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@LL1Rerent", objLandLordReferences.LL1Rerent);
                    cmd.Parameters.AddWithValue("@LL1RerentPoints", objLandLordReferences.LL1RerentPoints);



                    cmd.Parameters.AddWithValue("@LL2ProperNotice", objLandLordReferences.LL2ProperNotice);
                    cmd.Parameters.AddWithValue("@LL2ProperNoticePoints", objLandLordReferences.LL2ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2NSF", objLandLordReferences.LL2NSF);
                    cmd.Parameters.AddWithValue("@LL2NSFPoints", objLandLordReferences.LL2NSFPoints);
                    cmd.Parameters.AddWithValue("@LL2LatePayments", objLandLordReferences.LL2LatePayments);
                    cmd.Parameters.AddWithValue("@LL2LatePaymentsPoints", objLandLordReferences.LL2LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL2PaymentOrVacantNotices", objLandLordReferences.LL2PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL2TendayComplyNotice", objLandLordReferences.lL2TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL2TendayComplyNoticePoints", objLandLordReferences.lL2TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2HOAViolations", objLandLordReferences.LL2HOAViolations);
                    cmd.Parameters.AddWithValue("@LL2HOAViolationsPoints", objLandLordReferences.LL2HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@LL2PropertyCleanliness", objLandLordReferences.LL2PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@LL2PropertyCleanlinessPoints", objLandLordReferences.LL2PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@LL2Pets", objLandLordReferences.LL2Pets);
                    cmd.Parameters.AddWithValue("@LL2PetsPoints", objLandLordReferences.LL2PetsPoints);
                    cmd.Parameters.AddWithValue("@LL2AdversePetReferance", objLandLordReferences.LL2AdversePetReferance);
                    cmd.Parameters.AddWithValue("@LL2AdversePetReferancePoints", objLandLordReferences.LL2AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@LL2Rerent", objLandLordReferences.LL2Rerent);
                    cmd.Parameters.AddWithValue("@LL2RerentPoints", objLandLordReferences.LL2RerentPoints);




                    cmd.Parameters.AddWithValue("@CreatedBy", objLandLordReferences.CreatedBy);
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
        /// UpdateLandLordReferences method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateLandLordReferences(LandLordReferences objLandLordReferences)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateLandLordReferences", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objLandLordReferences.TenantId);
                    cmd.Parameters.AddWithValue("@RentalReferance", objLandLordReferences.RentalReferance);
                    cmd.Parameters.AddWithValue("@LandlordType", objLandLordReferences.LL1LandlordType);
                    cmd.Parameters.AddWithValue("@LL1ProperNotice", objLandLordReferences.LL1ProperNotice);
                    cmd.Parameters.AddWithValue("@LL1ProperNoticePoints", objLandLordReferences.LL1ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1NSF", objLandLordReferences.LL1NSF);
                    cmd.Parameters.AddWithValue("@LL1NSFPoints", objLandLordReferences.LL1NSFPoints);
                    cmd.Parameters.AddWithValue("@LL1LatePaymentsPoints", objLandLordReferences.LL1LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL1PaymentOrVacantNotices", objLandLordReferences.LL1PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL1TendayComplyNotice", objLandLordReferences.lL1TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL1TendayComplyNoticePoints", objLandLordReferences.lL1TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1HOAViolations", objLandLordReferences.LL1HOAViolations);
                    cmd.Parameters.AddWithValue("@LL1HOAViolationsPoints", objLandLordReferences.LL1HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@PropertyCleanliness", objLandLordReferences.LL1PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@LL1PropertyCleanlinessPoints", objLandLordReferences.LL1PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@LL1Pets", objLandLordReferences.LL1Pets);
                    cmd.Parameters.AddWithValue("@LL1PetsPoints", objLandLordReferences.LL1PetsPoints);
                    cmd.Parameters.AddWithValue("@LL1AdversePetReferance", objLandLordReferences.LL1AdversePetReferance);
                    cmd.Parameters.AddWithValue("@LL1AdversePetReferancePoints", objLandLordReferences.LL1AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@LL1Rerent", objLandLordReferences.LL1Rerent);
                    cmd.Parameters.AddWithValue("@LL1RerentPoints", objLandLordReferences.LL1RerentPoints);



                    cmd.Parameters.AddWithValue("@LL2ProperNotice", objLandLordReferences.LL2ProperNotice);
                    cmd.Parameters.AddWithValue("@LL2ProperNoticePoints", objLandLordReferences.LL2ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2NSF", objLandLordReferences.LL2NSF);
                    cmd.Parameters.AddWithValue("@LL2NSFPoints", objLandLordReferences.LL2NSFPoints);
                    cmd.Parameters.AddWithValue("@LL2LatePaymentsPoints", objLandLordReferences.LL2LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL2PaymentOrVacantNotices", objLandLordReferences.LL2PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL2TendayComplyNotice", objLandLordReferences.lL2TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL2TendayComplyNoticePoints", objLandLordReferences.lL2TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2HOAViolations", objLandLordReferences.LL2HOAViolations);
                    cmd.Parameters.AddWithValue("@LL2HOAViolationsPoints", objLandLordReferences.LL2HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@PropertyCleanliness", objLandLordReferences.LL2PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@LL2PropertyCleanlinessPoints", objLandLordReferences.LL2PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@LL2Pets", objLandLordReferences.LL2Pets);
                    cmd.Parameters.AddWithValue("@LL2PetsPoints", objLandLordReferences.LL2PetsPoints);
                    cmd.Parameters.AddWithValue("@LL2AdversePetReferance", objLandLordReferences.LL2AdversePetReferance);
                    cmd.Parameters.AddWithValue("@LL2AdversePetReferancePoints", objLandLordReferences.LL2AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@LL2Rerent", objLandLordReferences.LL2Rerent);
                    cmd.Parameters.AddWithValue("@LL2RerentPoints", objLandLordReferences.LL2RerentPoints);



                    cmd.Parameters.AddWithValue("@ModifiedBy", objLandLordReferences.ModifiedBy);
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
        /// CreateRentalHistory method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateRentalHistory(RentalHistory objRentalHistory)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertRentalHistory", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objRentalHistory.TenantId);
                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objRentalHistory.RentalHistoryLength);
                    cmd.Parameters.AddWithValue("@CreatedBy", objRentalHistory.CreatedBy);
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
        /// UpdateLandLordReferences method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateRentalHistory(RentalHistory objRentalHistory)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateRentalHistory", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objRentalHistory.TenantId);
                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objRentalHistory.RentalHistoryLength);
                    cmd.Parameters.AddWithValue("@ModifiedBy", objRentalHistory.ModifiedBy);
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
        /// CreatePets method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreatePets(Pets objPets)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertPets", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objPets.TenantId);
                    cmd.Parameters.AddWithValue("@PetApprovedLandlordReferance1", objPets.PetApprovedLandlordReferance1);
                    cmd.Parameters.AddWithValue("@PetApprovedLandlordReferance2", objPets.PetApprovedLandlordReferance2);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanion", objPets.NoOfCatsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanions", objPets.NoOfCatsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanionPoints", objPets.NoOfCatsCompanionPoints);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanion", objPets.NoOfLargeDogsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanions", objPets.NoOfLargeDogsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanionPoints", objPets.NoOfLargeDogsCompanionPoints);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanion", objPets.NoOfSmallDogsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanions", objPets.NoOfSmallDogsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanionPoints", objPets.NoOfSmallDogsCompanionPoints);

                    cmd.Parameters.AddWithValue("@CreatedBy", objPets.CreatedBy);
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
        /// UpdatePets method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdatePets(Pets objPets)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdatePets", sqlcon))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objPets.TenantId);
                    cmd.Parameters.AddWithValue("@PetApprovedLandlordReferance1", objPets.PetApprovedLandlordReferance1);
                    cmd.Parameters.AddWithValue("@PetApprovedLandlordReferance2", objPets.PetApprovedLandlordReferance2);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanion", objPets.NoOfCatsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanions", objPets.NoOfCatsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfCatsCompanionPoints", objPets.NoOfCatsCompanionPoints);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanion", objPets.NoOfLargeDogsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanions", objPets.NoOfLargeDogsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfLargeDogsCompanionPoints", objPets.NoOfLargeDogsCompanionPoints);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanion", objPets.NoOfSmallDogsCompanion);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanions", objPets.NoOfSmallDogsCompanions);
                    cmd.Parameters.AddWithValue("@NoOfSmallDogsCompanionPoints", objPets.NoOfSmallDogsCompanionPoints);

                    cmd.Parameters.AddWithValue("@ModifiedBy", objPets.ModifiedBy);
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
        /// CreatePointsSummary method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreatePointsSummary(PointsSummary objPointsSummary)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertPointsSummary", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objPointsSummary.TenantId);
                    cmd.Parameters.AddWithValue("@TotalPoints", objPointsSummary.TotalPoints);
                    cmd.Parameters.AddWithValue("@FinalApproval", objPointsSummary.FinalApproval);
                    cmd.Parameters.AddWithValue("@TotalDeposit", objPointsSummary.TotalDeposit);
                    cmd.Parameters.AddWithValue("@DepositToHoldpaid", objPointsSummary.DepositToHoldpaid);
                    cmd.Parameters.AddWithValue("@PetDeposit", objPointsSummary.PetDeposit);
                    cmd.Parameters.AddWithValue("@AdditionalDeposit", objPointsSummary.AdditionalDeposit);
                    cmd.Parameters.AddWithValue("@BalanceDepositDue", objPointsSummary.BalanceDepositDue);
                    cmd.Parameters.AddWithValue("@BalanceDepositDuePoints", objPointsSummary.BalanceDepositDuePoints);

                    cmd.Parameters.AddWithValue("@CreatedBy", objPointsSummary.CreatedBy);
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
        /// UpdatePointsSummary method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdatePointsSummary(PointsSummary objPointsSummary)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdatePointsSummary", sqlcon))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objPointsSummary.TenantId);
                    cmd.Parameters.AddWithValue("@TotalPoints", objPointsSummary.TotalPoints);
                    cmd.Parameters.AddWithValue("@FinalApproval", objPointsSummary.FinalApproval);
                    cmd.Parameters.AddWithValue("@TotalDeposit", objPointsSummary.TotalDeposit);
                    cmd.Parameters.AddWithValue("@DepositToHoldpaid", objPointsSummary.DepositToHoldpaid);
                    cmd.Parameters.AddWithValue("@PetDeposit", objPointsSummary.PetDeposit);
                    cmd.Parameters.AddWithValue("@AdditionalDeposit", objPointsSummary.AdditionalDeposit);
                    cmd.Parameters.AddWithValue("@BalanceDepositDue", objPointsSummary.BalanceDepositDue);

                    cmd.Parameters.AddWithValue("@ModifiedBy", objPointsSummary.ModifiedBy);
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
        /// GetScroreSheetByApplicantId method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public List<Scoresheet> GetScroreSheetByApplicantId(int? ApplicantId, int? TenantSerialNumber)
        {
            List<Scoresheet> applicantslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetScroreSheetByApplicantId", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantId ", ApplicantId);
                    cmd.Parameters.AddWithValue("@Tenant_Serial_Number", TenantSerialNumber);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Scoresheet Objuser = new()
                        {
                            //Id = Convert.ToInt32(row["Id"]),
                            ApplicantId = ApplicantId,
                            TenantSNo = TenantSerialNumber,
                            ApplicantName = Convert.ToString(row["ApplicantName"]),
                            Property = Convert.ToString(row["Property"]),
                            ApplicantTypeId = System.Convert.IsDBNull(row["ApplicantTypeId"]) == true ? 0 : Convert.ToInt32(row["ApplicantTypeId"]),
                            ApplicationStatusId = System.Convert.IsDBNull(row["ApplicationStatusId"]) == true ? 0 : Convert.ToInt32(row["ApplicationStatusId"]),
                            City = Convert.ToString(row["City"]),
                            State = Convert.ToString(row["State"]),
                            Zip = Convert.ToString(row["Zip"]),
                            MonthlyRent = Convert.ToString(row["MonthlyRent"]),
                            Section8Rent = Convert.ToString(row["Section8Rent"]),
                            StandardDepositProperty = Convert.ToString(row["StandardDepositProperty"]),
                            //PetDeposit = Convert.ToString(row["PetDeposit"]),
                            PropertyTypeId = System.Convert.IsDBNull(row["PropertyTypeId"]) == true ? 0 : Convert.ToInt32(row["PropertyTypeId"]),
                            PropertyType = Convert.ToString(row["PropertyType"]),
                            ApplicantType = Convert.ToString(row["ApplicantType"]),

                            TenantId = System.Convert.IsDBNull(row["TenantId"]) == true ? 0 : Convert.ToInt32(row["TenantId"]),
                            PaystubRecent = Convert.ToString(row["PaystubRecent"]),
                            YTD_Earnings = Convert.ToString(row["YTD_Earnings"]),
                            PaystubRecentMonthly = Convert.ToString(row["PaystubRecentMonthly"]),
                            BankStatement = Convert.ToString(row["BankStatement"]),
                            SecondPayStub = Convert.ToString(row["SecondPayStub"]),
                            BankStatementMonthly = Convert.ToString(row["BankStatementMonthly"]),
                            xRent = Convert.ToString(row["xRent"]),
                            IncomeAdequate = System.Convert.IsDBNull(row["IncomeAdequate"]) == true ? false : Convert.ToBoolean(row["IncomeAdequate"]),
                            CreditLines = System.Convert.IsDBNull(row["CreditLines"]) == true ? false : Convert.ToBoolean(row["CreditLines"]),
                            CreditScore = Convert.ToString(row["CreditScore"]),
                            CreditScorePoints = Convert.ToString(row["CreditScorePoints"]),
                            CreditScoreAvailable = System.Convert.IsDBNull(row["CreditScoreAvailable"]) == true ? false : Convert.ToBoolean(row["CreditScoreAvailable"]),
                            CreditScoreAvailablePoints = Convert.ToString(row["CreditScoreAvailablePoints"]),
                            AccountPastDue60Days = Convert.ToString(row["AccountPastDue60Days"]),
                            CollectionAccounts = Convert.ToString(row["CollectionAccounts"]),
                            CollectionAccountsPoints = Convert.ToString(row["CollectionAccountsPoints"]),
                            MedicalCollections = Convert.ToString(row["MedicalCollections"]),
                            PropertyRelatedHousingRecord = System.Convert.IsDBNull(row["PropertyRelatedHousingRecord"]) == true ? null : Convert.ToBoolean(row["PropertyRelatedHousingRecord"]),
                            Bankruptcy = System.Convert.IsDBNull(row["Bankruptcy"]) == true ? 0 : Convert.ToInt32(row["Bankruptcy"]),
                            BankruptcyPoints = System.Convert.IsDBNull(row["BankruptcyPoints"]) == true ? 0 : Convert.ToInt32(row["BankruptcyPoints"]),
                            PropertyRelatedHousingRecordPoints = Convert.ToString(row["PropertyRelatedHousingRecordPoints"]),
                            BankRuptyActive = System.Convert.IsDBNull(row["BankRuptyActive"]) == true ? null : Convert.ToBoolean(row["BankRuptyActive"]),
                            //BankRuptyActivePoints = Convert.ToBoolean(row["BankRuptyActivePoints"]),
                            BankRuptyActivePoints = System.Convert.IsDBNull(row["BankRuptyActivePoints"]) == true ? null : Convert.ToInt32(row["BankRuptyActivePoints"]),
                            LiensRepossessions = System.Convert.IsDBNull(row["LiensRepossessions"]) == true ? null : Convert.ToDateTime(row["LiensRepossessions"]),
                            LiensRepossessionsPoints = Convert.ToString(row["LiensRepossessionsPoints"]),
                            EvectionHistory = System.Convert.IsDBNull(row["EvectionHistory"]) == true ? null : Convert.ToDateTime(row["EvectionHistory"]),
                            EvectionHistoryPoints = Convert.ToString(row["EvectionHistoryPoints"]),
                            Class1Felonies = System.Convert.IsDBNull(row["Class1Felonies"]) == true ? null : Convert.ToBoolean(row["Class1Felonies"]),
                            Class1FeloniesPoints = Convert.ToString(row["Class1FeloniesPoints"]),
                            Class2Felonies = System.Convert.IsDBNull(row["Class2Felonies"]) == true ? null : Convert.ToDateTime(row["Class2Felonies"]),
                            Class2FeloniesPoints = Convert.ToString(row["Class2FeloniesPoints"]),
                            Class1Misdemeaners = System.Convert.IsDBNull(row["Class1Misdemeaners"]) == true ? null : Convert.ToDateTime(row["Class1Misdemeaners"]),
                            Class1MisdemeanersPoints = Convert.ToString(row["Class1MisdemeanersPoints"]),
                            Class2Misdemeaners = System.Convert.IsDBNull(row["Class2Misdemeaners"]) == true ? null : Convert.ToDateTime(row["Class2Misdemeaners"]),
                            Class2MisdemeanersPoints = Convert.ToString(row["Class2MisdemeanersPoints"]),
                            DepositApproved = System.Convert.IsDBNull(row["DepositApproved"]) == true ? null : Convert.ToBoolean(row["DepositApproved"]),
                            DepositToHold = Convert.ToString(row["DepositToHold"]),
                            RentalReferance = System.Convert.IsDBNull(row["RentalReferance"]) == true ? null : Convert.ToBoolean(row["RentalReferance"]),

                            //LandlordType = Convert.ToString(row["LandlordType"]),
                            LL1ProperNotice = System.Convert.IsDBNull(row["LL1ProperNotice"]) == true ? null : Convert.ToBoolean(row["LL1ProperNotice"]),
                            LL1ProperNoticePoints = System.Convert.IsDBNull(row["LL1ProperNoticePoints"]) == true ? 0 : Convert.ToInt32(row["LL1ProperNoticePoints"]),
                            LL1NSF = System.Convert.IsDBNull(row["LL1NSF"]) == true ? 0 : Convert.ToInt32(row["LL1NSF"]),
                            LL1NSFPoints = System.Convert.IsDBNull(row["LL1NSFPoints"]) == true ? 0 : Convert.ToInt32(row["LL1NSFPoints"]),
                            LL1LatePayments = System.Convert.IsDBNull(row["LL1LatePayments"]) == true ? 0 : Convert.ToInt32(row["LL1LatePayments"]),
                            LL1LatePaymentsPoints = System.Convert.IsDBNull(row["LL1LatePaymentsPoints"]) == true ? 0 : Convert.ToInt32(row["LL1LatePaymentsPoints"]),
                            LL1PaymentOrVacantNotices = System.Convert.IsDBNull(row["LL1PaymentOrVacantNotices"]) == true ? 0 : Convert.ToInt32(row["LL1PaymentOrVacantNotices"]),
                            LL1PaymentOrVacantNoticesPoints = System.Convert.IsDBNull(row["LL1PaymentOrVacantNoticesPoints"]) == true ? 0 : Convert.ToInt32(row["LL1PaymentOrVacantNoticesPoints"]),
                            LL1TendayComplyNotice = System.Convert.IsDBNull(row["LL1TendayComplyNotice"]) == true ? 0 : Convert.ToInt32(row["LL1TendayComplyNotice"]),
                            LL1TendayComplyNoticePoints = System.Convert.IsDBNull(row["LL1TendayComplyNoticePoints"]) == true ? 0 : Convert.ToInt32(row["LL1TendayComplyNoticePoints"]),
                            LL1HOAViolations = System.Convert.IsDBNull(row["LL1HOAViolations"]) == true ? 0 : Convert.ToInt32(row["LL1HOAViolations"]),
                            LL1HOAViolationsPoints = System.Convert.IsDBNull(row["LL1HOAViolationsPoints"]) == true ? 0 : Convert.ToInt32(row["LL1HOAViolationsPoints"]),
                            LL1PropertyCleanliness = System.Convert.IsDBNull(row["LL1PropertyCleanliness"]) == true ? 0 : Convert.ToInt32(row["LL1PropertyCleanliness"]),
                            LL1PropertyCleanlinessPoints = System.Convert.IsDBNull(row["LL1PropertyCleanlinessPoints"]) == true ? 0 : Convert.ToInt32(row["LL1PropertyCleanlinessPoints"]),
                            LL1Pets = System.Convert.IsDBNull(row["LL1Pets"]) == true ? null : Convert.ToBoolean(row["LL1Pets"]),
                            LL1PetsPoints = System.Convert.IsDBNull(row["LL1PetsPoints"]) == true ? 0 : Convert.ToInt32(row["LL1PetsPoints"]),
                            LL1AdversePetReferance = System.Convert.IsDBNull(row["LL1AdversePetReferance"]) == true ? null : Convert.ToBoolean(row["LL1AdversePetReferance"]),
                            LL1AdversePetReferancePoints = System.Convert.IsDBNull(row["LL1AdversePetReferancePoints"]) == true ? 0 : Convert.ToInt32(row["LL1AdversePetReferancePoints"]),
                            LL1Rerent = System.Convert.IsDBNull(row["LL1Rerent"]) == true ? null : Convert.ToBoolean(row["LL1Rerent"]),
                            LL1RerentPoints = System.Convert.IsDBNull(row["LL1RerentPoints"]) == true ? 0 : Convert.ToInt32(row["LL1RerentPoints"]),

                            LL2ProperNotice = System.Convert.IsDBNull(row["LL2ProperNotice"]) == true ? null : Convert.ToBoolean(row["LL2ProperNotice"]),
                            LL2ProperNoticePoints = System.Convert.IsDBNull(row["LL2ProperNoticePoints"]) == true ? 0 : Convert.ToInt32(row["LL2ProperNoticePoints"]),
                            LL2NSF = System.Convert.IsDBNull(row["LL2NSF"]) == true ? 0 : Convert.ToInt32(row["LL2NSF"]),
                            LL2NSFPoints = System.Convert.IsDBNull(row["LL2NSFPoints"]) == true ? 0 : Convert.ToInt32(row["LL2NSFPoints"]),
                            LL2LatePayments = System.Convert.IsDBNull(row["LL2LatePayments"]) == true ? 0 : Convert.ToInt32(row["LL2LatePayments"]),
                            LL2LatePaymentsPoints = System.Convert.IsDBNull(row["LL2LatePaymentsPoints"]) == true ? 0 : Convert.ToInt32(row["LL2LatePaymentsPoints"]),
                            LL2PaymentOrVacantNotices = System.Convert.IsDBNull(row["LL2PaymentOrVacantNotices"]) == true ? 0 : Convert.ToInt32(row["LL2PaymentOrVacantNotices"]),
                            LL2PaymentOrVacantNoticesPoints = System.Convert.IsDBNull(row["LL2PaymentOrVacantNoticesPoints"]) == true ? 0 : Convert.ToInt32(row["LL2PaymentOrVacantNoticesPoints"]),
                            LL2TendayComplyNotice = System.Convert.IsDBNull(row["LL2TendayComplyNotice"]) == true ? 0 : Convert.ToInt32(row["LL2TendayComplyNotice"]),
                            LL2TendayComplyNoticePoints = System.Convert.IsDBNull(row["LL2TendayComplyNoticePoints"]) == true ? 0 : Convert.ToInt32(row["LL2TendayComplyNoticePoints"]),
                            LL2HOAViolations = System.Convert.IsDBNull(row["LL2HOAViolations"]) == true ? 0 : Convert.ToInt32(row["LL2HOAViolations"]),
                            LL2HOAViolationsPoints = System.Convert.IsDBNull(row["LL2HOAViolationsPoints"]) == true ? 0 : Convert.ToInt32(row["LL2HOAViolationsPoints"]),
                            LL2PropertyCleanliness = System.Convert.IsDBNull(row["LL2PropertyCleanliness"]) == true ? 0 : Convert.ToInt32(row["LL2PropertyCleanliness"]),
                            LL2PropertyCleanlinessPoints = System.Convert.IsDBNull(row["LL2PropertyCleanlinessPoints"]) == true ? 0 : Convert.ToInt32(row["LL2PropertyCleanlinessPoints"]),
                            LL2Pets = System.Convert.IsDBNull(row["LL2Pets"]) == true ? null : Convert.ToBoolean(row["LL2Pets"]),
                            LL2PetsPoints = System.Convert.IsDBNull(row["LL2PetsPoints"]) == true ? 0 : Convert.ToInt32(row["LL2PetsPoints"]),
                            LL2AdversePetReferance = System.Convert.IsDBNull(row["LL2AdversePetReferance"]) == true ? null : Convert.ToBoolean(row["LL2AdversePetReferance"]),
                            LL2AdversePetReferancePoints = System.Convert.IsDBNull(row["LL2AdversePetReferancePoints"]) == true ? 0 : Convert.ToInt32(row["LL2AdversePetReferancePoints"]),
                            LL2Rerent = System.Convert.IsDBNull(row["LL2Rerent"]) == true ? null : Convert.ToBoolean(row["LL2Rerent"]),
                            LL2RerentPoints = System.Convert.IsDBNull(row["LL2RerentPoints"]) == true ? 0 : Convert.ToInt32(row["LL2RerentPoints"]),



                            RentalHistoryLength = System.Convert.IsDBNull(row["RentalHistoryLength"]) == true ? null : Convert.ToBoolean(row["RentalHistoryLength"]),
                            PetApprovedLandlordReferance1 = Convert.ToString(row["PetApprovedLandlordReferance1"]),
                            PetApprovedLandlordReferance2 = Convert.ToString(row["PetApprovedLandlordReferance2"]),
                            NoOfCatsCompanion = System.Convert.IsDBNull(row["NoOfCatsCompanion"]) == true ? null : Convert.ToBoolean(row["NoOfCatsCompanion"]),
                            NoOfCatsCompanions = Convert.ToString(row["NoOfCatsCompanions"]),
                            NoOfCatsCompanionPoints = Convert.ToString(row["NoOfCatsCompanionPoints"]),
                            NoOfLargeDogsCompanion = System.Convert.IsDBNull(row["NoOfLargeDogsCompanion"]) == true ? null : Convert.ToBoolean(row["NoOfLargeDogsCompanion"]),
                            NoOfLargeDogsCompanions = Convert.ToString(row["NoOfLargeDogsCompanions"]),
                            NoOfLargeDogsCompanionPoints = Convert.ToString(row["NoOfLargeDogsCompanionPoints"]),
                            NoOfSmallDogsCompanion = System.Convert.IsDBNull(row["NoOfSmallDogsCompanion"]) == true ? null : Convert.ToBoolean(row["NoOfSmallDogsCompanion"]),
                            NoOfSmallDogsCompanions = Convert.ToString(row["NoOfSmallDogsCompanions"]),
                            NoOfSmallDogsCompanionPoints = Convert.ToString(row["NoOfSmallDogsCompanionPoints"]),
                            TotalPoints = System.Convert.IsDBNull(row["TotalPoints"]) == true ? null : Convert.ToDouble(row["TotalPoints"]),                           
                            FinalApproval = System.Convert.IsDBNull(row["FinalApproval"]) == true ? null : Convert.ToBoolean(row["FinalApproval"]),
                            TotalDeposit = System.Convert.IsDBNull(row["TotalDeposit"]) == true ? null : Convert.ToDouble(row["TotalDeposit"]),
                            PetDeposit = System.Convert.IsDBNull(row["PetDeposit"]) == true ? null : Convert.ToDouble(row["PetDeposit"]),
                            DepositToHoldpaid = System.Convert.IsDBNull(row["DepositToHoldpaid"]) == true ? null : Convert.ToDouble(row["DepositToHoldpaid"]),
                            AdditionalDeposit = System.Convert.IsDBNull(row["AdditionalDeposit"]) == true ? null : Convert.ToDouble(row["AdditionalDeposit"]),
                            BalanceDepositDue = System.Convert.IsDBNull(row["BalanceDepositDue"]) == true ? null : Convert.ToDouble(row["BalanceDepositDue"]),
                            //CreatedBy = Convert.ToString(row["CreatedBy"]),
                            //CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                            //ModifiedBy = Convert.ToString(row["ModifiedBy"]),
                            //ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])

                        };

                        applicantslist.Add(Objuser);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return applicantslist;
        }

        /// <summary>
        /// CreateCoverSheet method is used to create new users in the system.
        /// </summary>
        /// <param name="objuser">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool CreateCoverSheet(Coversheet objCoversheet)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spInsertCoverSheet", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantId", objCoversheet.ApplicantId);
                    cmd.Parameters.AddWithValue("@PropertyManager", objCoversheet.PropertyManager);
                    cmd.Parameters.AddWithValue("@PrimaryTenant", objCoversheet.PrimaryTenant);
                    cmd.Parameters.AddWithValue("@Tenant2", objCoversheet.Tenant2);
                    cmd.Parameters.AddWithValue("@Tenant3", objCoversheet.Tenant3);
                    cmd.Parameters.AddWithValue("@Tenant4", objCoversheet.Tenant4);
                    cmd.Parameters.AddWithValue("@PropertyAddress", objCoversheet.PropertyAddress);
                    cmd.Parameters.AddWithValue("@City", objCoversheet.City);

                    cmd.Parameters.AddWithValue("@State", objCoversheet.State);
                    cmd.Parameters.AddWithValue("@UnitCode", objCoversheet.UnitCode);
                    cmd.Parameters.AddWithValue("@BestPOC", objCoversheet.BestPOC);
                    cmd.Parameters.AddWithValue("@RentReadyDate", objCoversheet.RentReadyDate);
                    cmd.Parameters.AddWithValue("@DepositPaidDate", objCoversheet.DepositPaidDate);
                    cmd.Parameters.AddWithValue("@RentResponsibleDate", objCoversheet.RentResponsibleDate);
                    cmd.Parameters.AddWithValue("@AgreementType", objCoversheet.AgreementType);
                    cmd.Parameters.AddWithValue("@QCDate", objCoversheet.QCDate);
                    cmd.Parameters.AddWithValue("@SigningDate", objCoversheet.SigningDate);
                    cmd.Parameters.AddWithValue("@SigningTime", objCoversheet.SigningTime);
                    cmd.Parameters.AddWithValue("@WithWhom", objCoversheet.WithWhom);
                    cmd.Parameters.AddWithValue("@OtherTerms", objCoversheet.OtherTerms);
                    cmd.Parameters.AddWithValue("@ListPaidUtilities", objCoversheet.ListPaidUtilities);
                    cmd.Parameters.AddWithValue("@MonthlyRent", objCoversheet.MonthlyRent);
                    cmd.Parameters.AddWithValue("@MoveinRentCharge", objCoversheet.MoveinRentCharge);
                    cmd.Parameters.AddWithValue("@MoveinRentPaid", objCoversheet.MoveinRentPaid);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge11", objCoversheet.OtherMonthlyCharge11);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge12", objCoversheet.OtherMonthlyCharge12);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge21", objCoversheet.OtherMonthlyCharge21);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge22", objCoversheet.OtherMonthlyCharge22);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge31", objCoversheet.OtherMonthlyCharge31);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge32", objCoversheet.OtherMonthlyCharge32);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge41", objCoversheet.OtherMonthlyCharge41);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge42", objCoversheet.OtherMonthlyCharge42);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge1", objCoversheet.OtherMoveinCharge1);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid1", objCoversheet.OtherMoveinChargePaid1);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge2", objCoversheet.OtherMoveinCharge2);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid2", objCoversheet.OtherMoveinChargePaid2);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge3", objCoversheet.OtherMoveinCharge3);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid3", objCoversheet.OtherMoveinChargePaid3);
                    cmd.Parameters.AddWithValue("@RubsMoveinCharge", objCoversheet.RubsMoveinCharge);

                    cmd.Parameters.AddWithValue("@RubsMoveinChargePaid", objCoversheet.RubsMoveinChargePaid);
                    cmd.Parameters.AddWithValue("@PrepaidCleaningCharge", objCoversheet.PrepaidCleaningCharge);
                    cmd.Parameters.AddWithValue("@PrepaidCleaningPaid", objCoversheet.PrepaidCleaningPaid);
                    cmd.Parameters.AddWithValue("@SecurityDepositCharge", objCoversheet.SecurityDepositCharge);
                    cmd.Parameters.AddWithValue("@SecurityDepositPaid", objCoversheet.SecurityDepositPaid);
                    cmd.Parameters.AddWithValue("@NonRefProcessingFeeCharge", objCoversheet.NonRefProcessingFeeCharge);
                    cmd.Parameters.AddWithValue("@NonRefProcessingFeePaid", objCoversheet.NonRefProcessingFeePaid);
                    cmd.Parameters.AddWithValue("@PetDepositCharge", objCoversheet.PetDepositCharge);
                    cmd.Parameters.AddWithValue("@PetDepositPaid", objCoversheet.PetDepositPaid);
                    cmd.Parameters.AddWithValue("@PetNonRefFeeCharge", objCoversheet.PetNonRefFeeCharge);
                    cmd.Parameters.AddWithValue("@PetNonRefFeePaid", objCoversheet.PetNonRefFeePaid);
                    cmd.Parameters.AddWithValue("@AdditionDepositCharge", objCoversheet.AdditionDepositCharge);
                    cmd.Parameters.AddWithValue("@AdditionDepositPaid", objCoversheet.AdditionDepositPaid);
                    cmd.Parameters.AddWithValue("@SubTotal", objCoversheet.SubTotal);
                    cmd.Parameters.AddWithValue("@Paid", objCoversheet.Paid);
                    cmd.Parameters.AddWithValue("@DueatMoveinKeyPickup", objCoversheet.DueatMoveinKeyPickup);

                    //cmd.Parameters.AddWithValue("@CreatedBy", objCoversheet.CreatedBy);


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
        /// UpdateCoverSheet method is used to create new users in the system.
        /// </summary>
        /// <param name="objCoversheet">Represents an instance of the 'Users' class containing various properties related to the user being created.</param>
        /// <returns>Returns a boolean value indicating whether the user creation operation was successful or not.</returns>
        public bool UpdateCoverSheet(Coversheet objCoversheet)
        {
            bool Result = false;
            try
            {
                using (cmd = new SqlCommand("spUpdateCoverSheet", sqlcon))
                {
                    cmd.Parameters.AddWithValue("@Id", objCoversheet.Id);
                    cmd.Parameters.AddWithValue("@ApplicantId", objCoversheet.ApplicantId);
                    cmd.Parameters.AddWithValue("@PropertyManager", objCoversheet.PropertyManager);
                    cmd.Parameters.AddWithValue("@PrimaryTenant", objCoversheet.PrimaryTenant);
                    cmd.Parameters.AddWithValue("@Tenant2", objCoversheet.Tenant2);
                    cmd.Parameters.AddWithValue("@Tenant3", objCoversheet.Tenant3);
                    cmd.Parameters.AddWithValue("@Tenant4", objCoversheet.Tenant4);

                    cmd.Parameters.AddWithValue("@PropertyAddress", objCoversheet.PropertyAddress);
                    cmd.Parameters.AddWithValue("@PrimaryTenant", objCoversheet.PrimaryTenant);
                    cmd.Parameters.AddWithValue("@City", objCoversheet.City);
                    cmd.Parameters.AddWithValue("@PropertyManager", objCoversheet.PropertyManager);
                    cmd.Parameters.AddWithValue("@State", objCoversheet.State);
                    cmd.Parameters.AddWithValue("@UnitCode", objCoversheet.UnitCode);
                    cmd.Parameters.AddWithValue("@BestPOC", objCoversheet.BestPOC);
                    cmd.Parameters.AddWithValue("@RentReadyDate", objCoversheet.RentReadyDate);
                    cmd.Parameters.AddWithValue("@DepositPaidDate", objCoversheet.DepositPaidDate);
                    cmd.Parameters.AddWithValue("@RentResponsibleDate", objCoversheet.RentResponsibleDate);
                    cmd.Parameters.AddWithValue("@AgreementType", objCoversheet.AgreementType);
                    cmd.Parameters.AddWithValue("@QCDate", objCoversheet.QCDate);
                    cmd.Parameters.AddWithValue("@SigningDate", objCoversheet.SigningDate);
                    cmd.Parameters.AddWithValue("@SigningTime", objCoversheet.SigningTime);
                    cmd.Parameters.AddWithValue("@WithWhom", objCoversheet.WithWhom);
                    cmd.Parameters.AddWithValue("@OtherTerms", objCoversheet.OtherTerms);
                    cmd.Parameters.AddWithValue("@ListPaidUtilities", objCoversheet.ListPaidUtilities);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge11", objCoversheet.OtherMonthlyCharge11);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge12", objCoversheet.OtherMonthlyCharge12);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge21", objCoversheet.OtherMonthlyCharge21);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge22", objCoversheet.OtherMonthlyCharge22);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge31", objCoversheet.OtherMonthlyCharge31);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge32", objCoversheet.OtherMonthlyCharge32);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge41", objCoversheet.OtherMonthlyCharge41);
                    cmd.Parameters.AddWithValue("@OtherMonthlyCharge42", objCoversheet.OtherMonthlyCharge42);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge1", objCoversheet.OtherMoveinCharge1);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid1", objCoversheet.OtherMoveinChargePaid1);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge2", objCoversheet.OtherMoveinCharge2);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid2", objCoversheet.OtherMoveinChargePaid2);
                    cmd.Parameters.AddWithValue("@OtherMoveinCharge3", objCoversheet.OtherMoveinCharge3);
                    cmd.Parameters.AddWithValue("@OtherMoveinChargePaid3", objCoversheet.OtherMoveinChargePaid3);
                    cmd.Parameters.AddWithValue("@RubsMoveinCharge", objCoversheet.RubsMoveinCharge);

                    cmd.Parameters.AddWithValue("@RubsMoveinChargePaid", objCoversheet.RubsMoveinChargePaid);
                    cmd.Parameters.AddWithValue("@PrepaidCleaningCharge", objCoversheet.PrepaidCleaningCharge);
                    cmd.Parameters.AddWithValue("@PrepaidCleaningPaid", objCoversheet.PrepaidCleaningPaid);
                    cmd.Parameters.AddWithValue("@SecurityDepositCharge", objCoversheet.SecurityDepositCharge);
                    cmd.Parameters.AddWithValue("@SecurityDepositPaid", objCoversheet.SecurityDepositPaid);
                    cmd.Parameters.AddWithValue("@NonRefProcessingFeeCharge", objCoversheet.NonRefProcessingFeeCharge);
                    cmd.Parameters.AddWithValue("@NonRefProcessingFeePaid", objCoversheet.NonRefProcessingFeePaid);
                    cmd.Parameters.AddWithValue("@PetDepositCharge", objCoversheet.PetDepositCharge);
                    cmd.Parameters.AddWithValue("@PetDepositPaid", objCoversheet.PetDepositPaid);
                    cmd.Parameters.AddWithValue("@PetNonRefFeeCharge", objCoversheet.PetNonRefFeeCharge);
                    cmd.Parameters.AddWithValue("@PetNonRefFeePaid", objCoversheet.PetNonRefFeePaid);
                    cmd.Parameters.AddWithValue("@AdditionDepositCharge", objCoversheet.AdditionDepositCharge);
                    cmd.Parameters.AddWithValue("@AdditionDepositPaid", objCoversheet.AdditionDepositPaid);
                    cmd.Parameters.AddWithValue("@SubTotal", objCoversheet.SubTotal);
                    cmd.Parameters.AddWithValue("@Paid", objCoversheet.Paid);
                    cmd.Parameters.AddWithValue("@DueatMoveinKeyPickup", objCoversheet.DueatMoveinKeyPickup);


                    cmd.Parameters.AddWithValue("@ModifiedBy", objCoversheet.ModifiedBy);

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
        /// GetCoverSheetByApplicantId method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public List<Coversheet> GetCoverSheetByApplicantId(int? ApplicantId)
        {
            List<Coversheet> coverSheetlist = new();
            try
            {
                using (cmd = new SqlCommand("spGetCoversheetByApplicantId", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantId ", ApplicantId);
                    //cmd.Parameters.AddWithValue("@Tenant_Serial_Number", TenantSerialNumber);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Coversheet Objuser = new()
                        {
                            ApplicantId = ApplicantId,

                            PrimaryTenant = Convert.ToString(row["PrimaryTenant"]),
                            PropertyManager = Convert.ToString(row["PropertyManager"]),
                            Tenant2 = Convert.ToString(row["Tenant2"]),
                            Tenant3 = Convert.ToString(row["Tenant3"]),
                            Tenant4 = Convert.ToString(row["Tenant4"]),
                            PropertyAddress = Convert.ToString(row["PropertyAddress"]),
                            City = Convert.ToString(row["City"]),
                            State = Convert.ToString(row["State"]),
                            UnitCode = Convert.ToString(row["UnitCode"]),
                            BestPOC = Convert.ToString(row["BestPOC"]),
                            //PetDeposit = Convert.ToString(row["PetDeposit"]),
                            RentReadyDate = System.Convert.IsDBNull(row["RentReadyDate"]) == true ? null : Convert.ToDateTime(row["RentReadyDate"]),
                            DepositPaidDate = System.Convert.IsDBNull(row["DepositPaidDate"]) == true ? null : Convert.ToDateTime(row["DepositPaidDate"]),
                            RentResponsibleDate = System.Convert.IsDBNull(row["RentResponsibleDate"]) == true ? null : Convert.ToDateTime(row["RentResponsibleDate"]),

                            AgreementType = System.Convert.IsDBNull(row["AgreementType"]) == true ? null : Convert.ToInt32(row["AgreementType"]),
                            QCDate = System.Convert.IsDBNull(row["QCDate"]) == true ? null : Convert.ToDateTime(row["QCDate"]),
                            SigningDate = System.Convert.IsDBNull(row["SigningDate"]) == true ? null : Convert.ToDateTime(row["SigningDate"]),
                            //SigningTime = System.Convert.IsDBNull(row["SigningTime"]) == true ? null : row["SigningTime"], //DateTime.TryParseExact(Convert.ToString(row["SigningTime"]), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime)
                            WithWhom = Convert.ToString(row["WithWhom"]),
                            OtherTerms = Convert.ToString(row["OtherTerms"]),
                            ListPaidUtilities = Convert.ToString(row["ListPaidUtilities"]),
                            MonthlyRent = System.Convert.IsDBNull(row["MonthlyRent"]) == true ? null : Convert.ToDouble(row["MonthlyRent"]),

                            OtherMonthlyCharge11 = System.Convert.IsDBNull(row["OtherMonthlyCharge11"]) == true ? null : Convert.ToDouble(row["OtherMonthlyCharge11"]),
                            MoveinRentCharge = System.Convert.IsDBNull(row["MoveinRentCharge"]) == true ? null : Convert.ToDouble(row["MoveinRentCharge"]),
                            MoveinRentPaid = System.Convert.IsDBNull(row["MoveinRentPaid"]) == true ? null : Convert.ToDouble(row["MoveinRentPaid"]),
                            OtherMonthlyCharge12 = Convert.ToString(row["OtherMonthlyCharge12"]),
                            OtherMonthlyCharge21 = System.Convert.IsDBNull(row["OtherMonthlyCharge21"]) == true ? null : Convert.ToDouble(row["OtherMonthlyCharge21"]),
                            OtherMonthlyCharge22 = Convert.ToString(row["OtherMonthlyCharge22"]),
                            OtherMonthlyCharge31 = System.Convert.IsDBNull(row["OtherMonthlyCharge31"]) == true ? null : Convert.ToDouble(row["OtherMonthlyCharge31"]),
                            OtherMonthlyCharge32 = Convert.ToString(row["OtherMonthlyCharge32"]),
                            OtherMonthlyCharge41 = System.Convert.IsDBNull(row["OtherMonthlyCharge41"]) == true ? null : Convert.ToDouble(row["OtherMonthlyCharge41"]),
                            OtherMonthlyCharge42 = Convert.ToString(row["OtherMonthlyCharge42"]),
                            OtherMoveinCharge1 = System.Convert.IsDBNull(row["OtherMoveinCharge1"]) == true ? null : Convert.ToDouble(row["OtherMoveinCharge1"]),
                            OtherMoveinChargePaid1 = System.Convert.IsDBNull(row["OtherMoveinChargePaid1"]) == true ? null : Convert.ToDouble(row["OtherMoveinChargePaid1"]),
                            OtherMoveinCharge2 = System.Convert.IsDBNull(row["OtherMoveinCharge2"]) == true ? null : Convert.ToDouble(row["OtherMoveinCharge2"]),
                            OtherMoveinChargePaid2 = System.Convert.IsDBNull(row["OtherMoveinChargePaid2"]) == true ? null : Convert.ToDouble(row["OtherMoveinChargePaid2"]),
                            OtherMoveinCharge3 = System.Convert.IsDBNull(row["OtherMoveinCharge3"]) == true ? null : Convert.ToDouble(row["OtherMoveinCharge3"]),
                            OtherMoveinChargePaid3 = System.Convert.IsDBNull(row["OtherMoveinChargePaid3"]) == true ? null : Convert.ToDouble(row["OtherMoveinChargePaid3"]),
                            RubsMoveinCharge = System.Convert.IsDBNull(row["RubsMoveinCharge"]) == true ? null : Convert.ToDouble(row["RubsMoveinCharge"]),
                            RubsMoveinChargePaid = System.Convert.IsDBNull(row["RubsMoveinChargePaid"]) == true ? null : Convert.ToDouble(row["RubsMoveinChargePaid"]),
                            PrepaidCleaningCharge = System.Convert.IsDBNull(row["PrepaidCleaningCharge"]) == true ? null : Convert.ToDouble(row["PrepaidCleaningCharge"]),
                            PrepaidCleaningPaid = System.Convert.IsDBNull(row["PrepaidCleaningPaid"]) == true ? null : Convert.ToDouble(row["PrepaidCleaningPaid"]),
                            SecurityDepositCharge = System.Convert.IsDBNull(row["SecurityDepositCharge"]) == true ? null : Convert.ToDouble(row["SecurityDepositCharge"]),
                            SecurityDepositPaid = System.Convert.IsDBNull(row["SecurityDepositPaid"]) == true ? null : Convert.ToDouble(row["SecurityDepositPaid"]),
                            NonRefProcessingFeeCharge = System.Convert.IsDBNull(row["NonRefProcessingFeeCharge"]) == true ? null : Convert.ToDouble(row["NonRefProcessingFeeCharge"]),
                            NonRefProcessingFeePaid = System.Convert.IsDBNull(row["NonRefProcessingFeePaid"]) == true ? null : Convert.ToDouble(row["NonRefProcessingFeePaid"]),
                            PetDepositCharge = System.Convert.IsDBNull(row["PetDepositCharge"]) == true ? null : Convert.ToDouble(row["PetDepositCharge"]),

                            PetDepositPaid = System.Convert.IsDBNull(row["PetDepositPaid"]) == true ? null : Convert.ToDouble(row["PetDepositPaid"]),
                            PetNonRefFeeCharge = System.Convert.IsDBNull(row["PetNonRefFeeCharge"]) == true ? null : Convert.ToDouble(row["PetNonRefFeeCharge"]),
                            PetNonRefFeePaid = System.Convert.IsDBNull(row["PetNonRefFeePaid"]) == true ? null : Convert.ToDouble(row["PetNonRefFeePaid"]),
                            AdditionDepositCharge = System.Convert.IsDBNull(row["AdditionDepositCharge"]) == true ? null : Convert.ToDouble(row["AdditionDepositCharge"]),
                            AdditionDepositPaid = System.Convert.IsDBNull(row["AdditionDepositPaid"]) == true ? null : Convert.ToDouble(row["AdditionDepositPaid"]),
                            SubTotal = System.Convert.IsDBNull(row["SubTotal"]) == true ? null : Convert.ToDouble(row["SubTotal"]),
                            Paid = System.Convert.IsDBNull(row["Paid"]) == true ? null : Convert.ToDouble(row["Paid"]),
                            DueatMoveinKeyPickup = System.Convert.IsDBNull(row["DueatMoveinKeyPickup"]) == true ? null : Convert.ToDouble(row["DueatMoveinKeyPickup"]),
                        };

                        coverSheetlist.Add(Objuser);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return coverSheetlist;
        }

        /// <summary>
        /// GetArchivedScoreSheets method retrieves a list of managers from the database by executing a stored procedure "spGetApplicants".
        /// </summary>
        /// <returns>A list of Users objects representing the managers in the system. If no managers are found, an empty list is returned.</returns>
        public List<ApplicantInfo> GetArchivedScoreSheets()
        {
            List<ApplicantInfo> applicantslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetArchivedScoreSheets", sqlcon))
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

                            Id = row["Id"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["Id"]),
                            ApplicantName = row["ApplicantName"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicantName"]),
                            ApplicationStatusId = row["ApplicationStatusId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["ApplicationStatusId"]),
                            ApplicationStatus = row["ApplicationStatus"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicationStatus"]),

                            Property = row["Property"] == DBNull.Value ? null : System.Convert.ToString(row["Property"]),
                            ApplicantTypeId = row["ApplicantTypeId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["ApplicantTypeId"]),
                            City = row["City"] == DBNull.Value ? null : System.Convert.ToString(row["City"]),
                            State = row["State"] == DBNull.Value ? null : System.Convert.ToString(row["State"]),
                            Zip = row["Zip"] == DBNull.Value ? null : System.Convert.ToString(row["Zip"]),
                            MonthlyRent = row["MonthlyRent"] == DBNull.Value ? 0 : System.Convert.ToDouble(row["MonthlyRent"]),
                            Section8Rent = row["Section8Rent"] == DBNull.Value ? 0 : System.Convert.ToDouble(row["Section8Rent"]),
                            StandardDepositProperty = row["StandardDepositProperty"] == DBNull.Value ? null : System.Convert.ToDouble(row["StandardDepositProperty"]),
                            PetDeposit = row["PetDeposit"] == DBNull.Value ? null : System.Convert.ToDouble(row["PetDeposit"]),
                            PropertyTypeId = row["PropertyTypeId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["PropertyTypeId"]),
                            PropertyType = row["PropertyType"] == DBNull.Value ? null : System.Convert.ToString(row["PropertyType"]),
                            ApplicantType = row["ApplicantType"] == DBNull.Value ? null : System.Convert.ToString(row["ApplicantType"]),
                            CreatedBy = row["CreatedBy"] == DBNull.Value ? null : System.Convert.ToString(row["CreatedBy"]),
                            CreatedDate = row["CreatedDate"] == DBNull.Value ? null : System.Convert.IsDBNull(row["CreatedDate"]) ? null : Convert.ToDateTime(row["CreatedDate"]),
                            ModifiedDate = row["ModifiedDate"] == DBNull.Value ? null : System.Convert.IsDBNull(row["ModifiedDate"]) ? null : Convert.ToDateTime(row["ModifiedDate"])
                            //CreatedDate = row["CreatedDate"] == null ? null : Convert.ToDateTime(row["CreatedDate"]),
                            //    ModifiedDate = row["ModifiedDate"] == null ? null : Convert.ToDateTime(row["ModifiedDate"])

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
        /// GetApprovalSummaryByApplicantId method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public List<Scoresheet> GetApprovalSummaryByApplicantId(int? ApplicantId)
        {
            List<Scoresheet> applicantslist = new();
            try
            {
                using (cmd = new SqlCommand("spGetApprovalSummaryByApplicantId", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantId ", ApplicantId);
                  
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Scoresheet Objuser = new()
                        {
                            //Id = Convert.ToInt32(row["Id"]),
                            ApplicantId = ApplicantId,
                            
                            ApplicantName = Convert.ToString(row["ApplicantName"]),
                            Property = Convert.ToString(row["Property"]),
                           
                            City = Convert.ToString(row["City"]),
                            State = Convert.ToString(row["State"]),
                             
                           

                            TenantId = System.Convert.IsDBNull(row["TenantId"]) == true ? 0 : Convert.ToInt32(row["TenantId"]),

                            TotalPoints = System.Convert.IsDBNull(row["TotalPoints"]) == true ? null : Convert.ToDouble(row["TotalPoints"]),
                            FinalApproval = System.Convert.IsDBNull(row["FinalApproval"]) == true ? null : Convert.ToBoolean(row["FinalApproval"]),
                            TotalDeposit = System.Convert.IsDBNull(row["TotalDeposit"]) == true ? null : Convert.ToDouble(row["TotalDeposit"]),
                            PetDeposit = System.Convert.IsDBNull(row["PetDeposit"]) == true ? null : Convert.ToDouble(row["PetDeposit"]),
                            DepositToHoldpaid = System.Convert.IsDBNull(row["DepositToHoldpaid"]) == true ? null : Convert.ToDouble(row["DepositToHoldpaid"]),
                            AdditionalDeposit = System.Convert.IsDBNull(row["AdditionalDeposit"]) == true ? null : Convert.ToDouble(row["AdditionalDeposit"]),
                            BalanceDepositDue = System.Convert.IsDBNull(row["BalanceDepositDue"]) == true ? null : Convert.ToDouble(row["BalanceDepositDue"]),

                            //CreatedBy = Convert.ToString(row["CreatedBy"]),
                            //CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                            //ModifiedBy = Convert.ToString(row["ModifiedBy"]),
                            //ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])

                        };

                        applicantslist.Add(Objuser);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return applicantslist;
        }

        /// <summary>
        /// GetFormulae method retrieves user details from the database based on the provided email address.
        /// </summary>
        /// <param name="email">The email address of the user whose details are to be retrieved.</param>
        /// <returns>An object of the Users class containing the details of the specified user.</returns>
        public List<Formulae> GetFormulae()
        {
            List<Formulae> formulae = new();
            try
            {
                using (cmd = new SqlCommand("spGetFormulae", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Formulae Objuser = new()
                        {
                            ApplicantTypeId = System.Convert.IsDBNull(row["ApplicantTypeId"]) == true ? 0 : Convert.ToInt32(row["ApplicantTypeId"]),
                            PropertyTypeId = System.Convert.IsDBNull(row["PropertyTypeId"]) == true ? 0 : Convert.ToInt32(row["PropertyTypeId"]),
                            Description= Convert.ToString(row["Description"]),
                            StartValue = Convert.ToString(row["StartValue"]),
                            EndValue = Convert.ToString(row["EndValue"]),
                            Calculation = Convert.ToString(row["Calculation"]),

                        };

                        formulae.Add(Objuser);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { sqlcon.Close(); }

            return formulae;
        }
    }
}
