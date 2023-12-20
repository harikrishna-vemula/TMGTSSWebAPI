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
                    cmd.Parameters.AddWithValue("@CreatedBy", objIncomeVerfication.Id);
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
                    cmd.Parameters.AddWithValue("@ModifiedBy", objIncomeVerfication.Id);
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
                   

                    cmd.Parameters.AddWithValue("@CreatedBy", objCreditSummary.Id);
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


                    cmd.Parameters.AddWithValue("@ModifiedBy", objCreditSummary.Id);
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
                    cmd.Parameters.AddWithValue("@LandlordType", objLandLordReferences.LandlordType);
                    cmd.Parameters.AddWithValue("@ProperNotice", objLandLordReferences.ProperNotice);
                    cmd.Parameters.AddWithValue("@ProperNoticePoints", objLandLordReferences.ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@NSF", objLandLordReferences.NSF);
                    cmd.Parameters.AddWithValue("@NSFPoints", objLandLordReferences.NSFPoints);
                    cmd.Parameters.AddWithValue("@LatePaymentsPoints", objLandLordReferences.LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@PaymentOrVacantNotices", objLandLordReferences.PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@TendayComplyNotice", objLandLordReferences.TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@TendayComplyNoticePoints", objLandLordReferences.TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@HOAViolations", objLandLordReferences.HOAViolations);
                    cmd.Parameters.AddWithValue("@HOAViolationsPoints", objLandLordReferences.HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@PropertyCleanliness", objLandLordReferences.PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@PropertyCleanlinessPoints", objLandLordReferences.PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@Pets", objLandLordReferences.Pets);
                    cmd.Parameters.AddWithValue("@PetsPoints", objLandLordReferences.PetsPoints);
                    cmd.Parameters.AddWithValue("@AdversePetReferance", objLandLordReferences.AdversePetReferance);
                    cmd.Parameters.AddWithValue("@AdversePetReferancePoints", objLandLordReferences.AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@Rerent", objLandLordReferences.Rerent);
                    cmd.Parameters.AddWithValue("@RerentPoints", objLandLordReferences.RerentPoints);
                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objLandLordReferences.RentalHistoryLength);
                    

                    cmd.Parameters.AddWithValue("@CreatedBy", objLandLordReferences.Id);
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
                    cmd.Parameters.AddWithValue("@LandlordType", objLandLordReferences.LandlordType);
                    cmd.Parameters.AddWithValue("@ProperNotice", objLandLordReferences.ProperNotice);
                    cmd.Parameters.AddWithValue("@ProperNoticePoints", objLandLordReferences.ProperNoticePoints);
                    cmd.Parameters.AddWithValue("@NSF", objLandLordReferences.NSF);
                    cmd.Parameters.AddWithValue("@NSFPoints", objLandLordReferences.NSFPoints);
                    cmd.Parameters.AddWithValue("@LatePaymentsPoints", objLandLordReferences.LatePaymentsPoints);
                    cmd.Parameters.AddWithValue("@PaymentOrVacantNotices", objLandLordReferences.PaymentOrVacantNotices);

                    cmd.Parameters.AddWithValue("@TendayComplyNotice", objLandLordReferences.TendayComplyNotice);
                    cmd.Parameters.AddWithValue("@TendayComplyNoticePoints", objLandLordReferences.TendayComplyNoticePoints);
                    cmd.Parameters.AddWithValue("@HOAViolations", objLandLordReferences.HOAViolations);
                    cmd.Parameters.AddWithValue("@HOAViolationsPoints", objLandLordReferences.HOAViolationsPoints);
                    cmd.Parameters.AddWithValue("@PropertyCleanliness", objLandLordReferences.PropertyCleanliness);

                    cmd.Parameters.AddWithValue("@PropertyCleanlinessPoints", objLandLordReferences.PropertyCleanlinessPoints);
                    cmd.Parameters.AddWithValue("@Pets", objLandLordReferences.Pets);
                    cmd.Parameters.AddWithValue("@PetsPoints", objLandLordReferences.PetsPoints);
                    cmd.Parameters.AddWithValue("@AdversePetReferance", objLandLordReferences.AdversePetReferance);
                    cmd.Parameters.AddWithValue("@AdversePetReferancePoints", objLandLordReferences.AdversePetReferancePoints);

                    cmd.Parameters.AddWithValue("@Rerent", objLandLordReferences.Rerent);
                    cmd.Parameters.AddWithValue("@RerentPoints", objLandLordReferences.RerentPoints);
                    cmd.Parameters.AddWithValue("@RentalHistoryLength", objLandLordReferences.RentalHistoryLength);


                    cmd.Parameters.AddWithValue("@ModifiedBy", objLandLordReferences.Id);
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
                    cmd.Parameters.AddWithValue("@CreatedBy", objRentalHistory.Id);
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
                    cmd.Parameters.AddWithValue("@ModifiedBy", objRentalHistory.Id);
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
                    
                    cmd.Parameters.AddWithValue("@CreatedBy", objPets.Id);
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

                        cmd.Parameters.AddWithValue("@ModifiedBy", objPets.Id);
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
                   
                    cmd.Parameters.AddWithValue("@CreatedBy", objPointsSummary.Id);
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

                    cmd.Parameters.AddWithValue("@ModifiedBy", objPointsSummary.Id);
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
