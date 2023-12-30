﻿using System.Data;
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

                            Property = row["Property"] == DBNull.Value ? null : System.Convert.ToString(row["Property"]),
                            ApplicantTypeId = row["ApplicantTypeId"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["ApplicantTypeId"]),
                            City = row["City"] == DBNull.Value ? null : System.Convert.ToString(row["City"]),
                            State = row["State"] == DBNull.Value ? null : System.Convert.ToString(row["State"]),
                            Zip = row["Zip"] == DBNull.Value ? null : System.Convert.ToString(row["Zip"]),
                            MonthlyRent = row["MonthlyRent"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["MonthlyRent"]),
                            Section8Rent = row["Section8Rent"] == DBNull.Value ? 0 : System.Convert.ToInt32(row["Section8Rent"]),
                            StandardDepositProperty = row["StandardDepositProperty"] == DBNull.Value ? null : System.Convert.ToString(row["StandardDepositProperty"]),
                            PetDeposit = row["PetDeposit"] == DBNull.Value ? null : System.Convert.ToString(row["PetDeposit"]),
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
                    cmd.Parameters.AddWithValue("@Property", objApplicant.Property);
                    cmd.Parameters.AddWithValue("@ApplicantTypeId", objApplicant.ApplicantTypeId);
                    cmd.Parameters.AddWithValue("@City", objApplicant.City);
                    cmd.Parameters.AddWithValue("@State", objApplicant.State);
                    cmd.Parameters.AddWithValue("@Zip", objApplicant.Zip);
                    cmd.Parameters.AddWithValue("@MonthlyRent", objApplicant.MonthlyRent);
                    cmd.Parameters.AddWithValue("@Section8Rent", objApplicant.Section8Rent);
                    cmd.Parameters.AddWithValue("@StandardDepositProperty", objApplicant.StandardDepositProperty);
                    cmd.Parameters.AddWithValue("@PropertyTypeId", objApplicant.PropertyTypeId);
                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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
        public int CreateTenantInfo(int? ApplicantId, int? TenantSNo, int? CreatedBy)
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
                            PaystubRecent = Convert.ToInt32(row["PaystubRecent"]),
                            YTD_Earnings = Convert.ToInt32(row["YTD_Earnings"]),
                            PaystubRecentMonthly = Convert.ToInt32(row["PaystubRecentMonthly"]),
                            BankStatement = Convert.ToInt32(row["BankStatement"]),
                            secondPayStub = Convert.ToInt32(row["secondPayStub"]),
                            BankStatementMonthly = Convert.ToInt32(row["BankStatementMonthly"]),
                            xRent = Convert.ToInt32(row["xRent"]),
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
                using (cmd = new SqlCommand("spInsertIncomeVerfication", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", objIncomeVerfication.TenantId);
                    cmd.Parameters.AddWithValue("@PaystubRecent", objIncomeVerfication.PaystubRecent);
                    cmd.Parameters.AddWithValue("@YTD_Earnings", objIncomeVerfication.YTD_Earnings);
                    cmd.Parameters.AddWithValue("@PaystubRecentMonthly", objIncomeVerfication.PaystubRecentMonthly);
                    cmd.Parameters.AddWithValue("@BankStatement", objIncomeVerfication.BankStatement);
                    cmd.Parameters.AddWithValue("@2ndPayStub", objIncomeVerfication.secondPayStub);
                    cmd.Parameters.AddWithValue("@BankStatementMonthly", objIncomeVerfication.BankStatementMonthly);
                    cmd.Parameters.AddWithValue("@xRent", objIncomeVerfication.xRent);
                    cmd.Parameters.AddWithValue("@IncomeAdequate", objIncomeVerfication.IncomeAdequate);
                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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
                    cmd.Parameters.AddWithValue("@2ndPayStub", objIncomeVerfication.secondPayStub);
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


                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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
                    cmd.Parameters.AddWithValue("@LL1LandlordType", objLandLordReferences.LL1LandlordType);
                    cmd.Parameters.AddWithValue("@LL1ProperNotice", objLandLordReferences.LL1ProperNotice);
                    cmd.Parameters.AddWithValue("@LL1ProperNoticePoints", objLandLordReferences.LL1ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1NSF", objLandLordReferences.LL1NSF);
                    cmd.Parameters.AddWithValue("@LL1NSFPoints", objLandLordReferences.LL1NSFPoints);
                    cmd.Parameters.AddWithValue("@LL1LatePaymentsPoints", objLandLordReferences.LL1LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL1PaymentOrVacantNotices", objLandLordReferences.LL1PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL110dayComplyNotice", objLandLordReferences.LL110dayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL110dayComplyNoticePoints", objLandLordReferences.LL110dayComplyNoticePoints);
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


                    cmd.Parameters.AddWithValue("@LL2LandlordType", objLandLordReferences.LL2LandlordType);
                    cmd.Parameters.AddWithValue("@LL2ProperNotice", objLandLordReferences.LL2ProperNotice);
                    cmd.Parameters.AddWithValue("@LL2ProperNoticePoints", objLandLordReferences.LL2ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2NSF", objLandLordReferences.LL2NSF);
                    cmd.Parameters.AddWithValue("@LL2NSFPoints", objLandLordReferences.LL2NSFPoints);
                    cmd.Parameters.AddWithValue("@LL2LatePaymentsPoints", objLandLordReferences.LL2LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL2PaymentOrVacantNotices", objLandLordReferences.LL2PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL210dayComplyNotice", objLandLordReferences.LL210dayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL210dayComplyNoticePoints", objLandLordReferences.LL210dayComplyNoticePoints);
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

                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objLandLordReferences.RentalHistoryLength);


                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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
                    cmd.Parameters.AddWithValue("@LL1LandlordType", objLandLordReferences.LL1LandlordType);
                    cmd.Parameters.AddWithValue("@LL1ProperNotice", objLandLordReferences.LL1ProperNotice);
                    cmd.Parameters.AddWithValue("@LL1ProperNoticePoints", objLandLordReferences.LL1ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL1NSF", objLandLordReferences.LL1NSF);
                    cmd.Parameters.AddWithValue("@LL1NSFPoints", objLandLordReferences.LL1NSFPoints);
                    cmd.Parameters.AddWithValue("@LL1LatePaymentsPoints", objLandLordReferences.LL1LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL1PaymentOrVacantNotices", objLandLordReferences.LL1PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL1TendayComplyNotice", objLandLordReferences.LL110dayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL1TendayComplyNoticePoints", objLandLordReferences.LL110dayComplyNoticePoints);
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


                    cmd.Parameters.AddWithValue("@LL2LandlordType", objLandLordReferences.LL2LandlordType);
                    cmd.Parameters.AddWithValue("@LL2ProperNotice", objLandLordReferences.LL2ProperNotice);
                    cmd.Parameters.AddWithValue("@LL2ProperNoticePoints", objLandLordReferences.LL2ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@LL2NSF", objLandLordReferences.LL2NSF);
                    cmd.Parameters.AddWithValue("@LL2NSFPoints", objLandLordReferences.LL2NSFPoints);
                    cmd.Parameters.AddWithValue("@LL2LatePaymentsPoints", objLandLordReferences.LL2LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@LL2PaymentOrVacantNotices", objLandLordReferences.LL2PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@LL2TendayComplyNotice", objLandLordReferences.LL210dayComplyNotice);
                    cmd.Parameters.AddWithValue("@LL2TendayComplyNoticePoints", objLandLordReferences.LL210dayComplyNoticePoints);
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
                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objLandLordReferences.RentalHistoryLength);


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
                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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

                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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

                    cmd.Parameters.AddWithValue("@CreatedBy", 4);
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
                            ApplicantTypeId = Convert.ToInt32(row["ApplicantTypeId"]),
                            City = Convert.ToString(row["City"]),
                            State = Convert.ToString(row["State"]),
                            Zip = Convert.ToString(row["Zip"]),
                            MonthlyRent = Convert.ToString(row["MonthlyRent"]),
                            Section8Rent = Convert.ToString(row["Section8Rent"]),
                            StandardDepositProperty = Convert.ToString(row["StandardDepositProperty"]),
                            //PetDeposit = Convert.ToString(row["PetDeposit"]),
                            PropertyTypeId = Convert.ToInt32(row["PropertyTypeId"]),
                            PropertyType = Convert.ToString(row["PropertyType"]),
                            ApplicantType = Convert.ToString(row["ApplicantType"]),

                            TenantId = Convert.ToInt32(row["TenantId"]),
                            PaystubRecent = Convert.ToString(row["PaystubRecent"]),
                            YTD_Earnings = Convert.ToString(row["YTD_Earnings"]),
                            PaystubRecentMonthly = Convert.ToString(row["PaystubRecentMonthly"]),
                            BankStatement = Convert.ToString(row["BankStatement"]),
                            SecondPayStub = Convert.ToString(row["SecondPayStub"]),
                            BankStatementMonthly = Convert.ToString(row["BankStatementMonthly"]),
                            xRent = Convert.ToString(row["xRent"]),
                            IncomeAdequate = Convert.ToString(row["IncomeAdequate"]),
                            CreditLines = Convert.ToString(row["CreditLines"]),
                            CreditScore = Convert.ToString(row["CreditScore"]),
                            CreditScorePoints = Convert.ToString(row["CreditScorePoints"]),
                            CreditScoreAvailable = Convert.ToString(row["CreditScoreAvailable"]),
                            CreditScoreAvailablePoints = Convert.ToString(row["CreditScoreAvailablePoints"]),
                            AccountPastDue60Days = Convert.ToString(row["AccountPastDue60Days"]),
                            CollectionAccounts = Convert.ToString(row["CollectionAccounts"]),
                            CollectionAccountsPoints = Convert.ToString(row["CollectionAccountsPoints"]),
                            MedicalCollections = Convert.ToString(row["MedicalCollections"]),
                            PropertyRelatedHousingRecord = Convert.ToString(row["PropertyRelatedHousingRecord"]),

                            PropertyRelatedHousingRecordPoints = Convert.ToString(row["PropertyRelatedHousingRecordPoints"]),
                            BankRuptyActive = Convert.ToString(row["BankRuptyActive"]),
                            BankRuptyActivePoints = Convert.ToString(row["BankRuptyActivePoints"]),
                            LiensRepossessions = Convert.ToString(row["LiensRepossessions"]),
                            LiensRepossessionsPoints = Convert.ToString(row["LiensRepossessionsPoints"]),
                            EvectionHistory = Convert.ToString(row["EvectionHistory"]),
                            EvectionHistoryPoints = Convert.ToString(row["EvectionHistoryPoints"]),
                            Class1Felonies = Convert.ToString(row["Class1Felonies"]),
                            Class1FeloniesPoints = Convert.ToString(row["Class1FeloniesPoints"]),
                            Class2Felonies = Convert.ToString(row["Class2Felonies"]),
                            Class2FeloniesPoints = Convert.ToString(row["Class2FeloniesPoints"]),
                            Class1Misdemeaners = Convert.ToString(row["Class1Misdemeaners"]),
                            Class1MisdemeanersPoints = Convert.ToString(row["Class1MisdemeanersPoints"]),
                            Class2Misdemeaners = Convert.ToString(row["Class2Misdemeaners"]),
                            Class2MisdemeanersPoints = Convert.ToString(row["Class2MisdemeanersPoints"]),
                            DepositApproved = Convert.ToString(row["DepositApproved"]),
                            DepositToHold = Convert.ToString(row["DepositToHold"]),
                            RentalReferance = Convert.ToString(row["RentalReferance"]),

                            //LandlordType = Convert.ToString(row["LandlordType"]),
                            LL1ProperNotice = Convert.ToString(row["LL1ProperNotice"]),
                            LL1ProperNoticePoints = Convert.ToString(row["LL1ProperNoticePoints"]),
                            LL1NSF = Convert.ToString(row["LL1NSF"]),
                            LL1NSFPoints = Convert.ToString(row["LL1NSFPoints"]),
                            LL1LatePayments = Convert.ToString(row["LL1LatePayments"]),
                            LL1LatePaymentsPoints = Convert.ToString(row["LL1LatePaymentsPoints"]),
                            LL1PaymentOrVacantNotices = Convert.ToString(row["LL1PaymentOrVacantNotices"]),
                            LL1PaymentOrVacantNoticesPoints = Convert.ToString(row["LL1PaymentOrVacantNoticesPoints"]),
                            LL1TendayComplyNotice = Convert.ToString(row["LL1TendayComplyNotice"]),
                            LL1TendayComplyNoticePoints = Convert.ToString(row["LL1TendayComplyNoticePoints"]),
                            LL1HOAViolations = Convert.ToString(row["LL1HOAViolations"]),
                            LL1HOAViolationsPoints = Convert.ToString(row["LL1HOAViolationsPoints"]),
                            LL1PropertyCleanliness = Convert.ToString(row["LL1PropertyCleanliness"]),
                            LL1PropertyCleanlinessPoints = Convert.ToString(row["LL1PropertyCleanlinessPoints"]),
                            LL1Pets = Convert.ToString(row["LL1Pets"]),
                            LL1PetsPoints = Convert.ToString(row["LL1PetsPoints"]),
                            LL1AdversePetReferance = Convert.ToString(row["LL1AdversePetReferance"]),

                            LL1AdversePetReferancePoints = Convert.ToString(row["LL1AdversePetReferancePoints"]),
                            LL1Rerent = Convert.ToString(row["LL1Rerent"]),
                            LL1RerentPoints = Convert.ToString(row["LL1RerentPoints"]),

                            LL2ProperNotice = Convert.ToString(row["LL2ProperNotice"]),
                            LL2ProperNoticePoints = Convert.ToString(row["LL2ProperNoticePoints"]),
                            LL2NSF = Convert.ToString(row["LL2NSF"]),
                            LL2NSFPoints = Convert.ToString(row["LL2NSFPoints"]),
                            LL2LatePayments = Convert.ToString(row["LL2LatePayments"]),
                            LL2LatePaymentsPoints = Convert.ToString(row["LL2LatePaymentsPoints"]),
                            LL2PaymentOrVacantNotices = Convert.ToString(row["LL2PaymentOrVacantNotices"]),
                            LL2PaymentOrVacantNoticesPoints = Convert.ToString(row["LL2PaymentOrVacantNoticesPoints"]),
                            LL2TendayComplyNotice = Convert.ToString(row["LL2TendayComplyNotice"]),
                            LL2TendayComplyNoticePoints = Convert.ToString(row["LL2TendayComplyNoticePoints"]),
                            LL2HOAViolations = Convert.ToString(row["LL2HOAViolations"]),
                            LL2HOAViolationsPoints = Convert.ToString(row["LL2HOAViolationsPoints"]),
                            LL2PropertyCleanliness = Convert.ToString(row["LL2PropertyCleanliness"]),
                            LL2PropertyCleanlinessPoints = Convert.ToString(row["LL2PropertyCleanlinessPoints"]),
                            LL2Pets = Convert.ToString(row["LL2Pets"]),
                            LL2PetsPoints = Convert.ToString(row["LL2PetsPoints"]),
                            LL2AdversePetReferance = Convert.ToString(row["LL2AdversePetReferance"]),

                            LL2AdversePetReferancePoints = Convert.ToString(row["LL2AdversePetReferancePoints"]),
                            LL2Rerent = Convert.ToString(row["LL2Rerent"]),
                            LL2RerentPoints = Convert.ToString(row["LL2RerentPoints"]),



                            RentalHistoryLength = Convert.ToString(row["RentalHistoryLength"]),
                            PetApprovedLandlordReferance1 = Convert.ToString(row["PetApprovedLandlordReferance1"]),
                            PetApprovedLandlordReferance2 = Convert.ToString(row["PetApprovedLandlordReferance2"]),
                            NoOfCatsCompanion = Convert.ToString(row["NoOfCatsCompanion"]),
                            NoOfCatsCompanions = Convert.ToString(row["NoOfCatsCompanions"]),
                            NoOfCatsCompanionPoints = Convert.ToString(row["NoOfCatsCompanionPoints"]),
                            NoOfLargeDogsCompanion = Convert.ToString(row["NoOfLargeDogsCompanion"]),
                            NoOfLargeDogsCompanions = Convert.ToString(row["NoOfLargeDogsCompanions"]),
                            NoOfLargeDogsCompanionPoints = Convert.ToString(row["NoOfLargeDogsCompanionPoints"]),
                            NoOfSmallDogsCompanion = Convert.ToString(row["NoOfSmallDogsCompanion"]),
                            NoOfSmallDogsCompanions = Convert.ToString(row["NoOfSmallDogsCompanions"]),
                            NoOfSmallDogsCompanionPoints = Convert.ToString(row["NoOfSmallDogsCompanionPoints"]),
                            TotalPoints = Convert.ToString(row["TotalPoints"]),
                            FinalApproval = Convert.ToString(row["FinalApproval"]),
                            TotalDeposit = Convert.ToString(row["TotalDeposit"]),

                            DepositToHoldpaid = Convert.ToString(row["DepositToHoldpaid"]),
                            AdditionalDeposit = Convert.ToString(row["AdditionalDeposit"]),
                            BalanceDepositDue = Convert.ToString(row["BalanceDepositDue"]),
                            //CreatedBy = Convert.ToString(row["CreatedBy"]),
                            //CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                            //ModifiedBy = Convert.ToString(row["ModifiedBy"]),
                            //ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])

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
    }
}
