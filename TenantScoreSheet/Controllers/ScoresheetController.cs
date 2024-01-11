using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
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

        /// <summary>
        /// Retrieves a list of all applicants from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetAllApplicants")]
        public List<ApplicantInfo> GetAllApplicants()
        {
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            List<ApplicantInfo> applicantList;
            try
            {
                applicantList = scoresheetRepository.GetAllApplicants();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return applicantList;
        }

        /// <summary>
        /// Retrieves a list of all applicants from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetArchivedScoreSheets")]
        public List<ApplicantInfo> GetArchivedScoreSheets()
        {
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            List<ApplicantInfo> applicantList;
            try
            {
                applicantList = scoresheetRepository.GetArchivedScoreSheets();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return applicantList;
        }


        /// <summary>
        /// Creates an applicant.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateApplicant")]
        public async Task<Dictionary<string, object>> CreateApplicant([FromBody] ApplicantInfo value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                bool applicantExisted = false; int Id = 0;

                //applicantExisted = await scoresheetRepository.GetApplicantByName(value.ApplicantName);

                //if (applicantExisted == false)
                //{
                if (value.TenantSNo == "1")
                {
                    Id = scoresheetRepository.CreateApplicant(value);
                }


                int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId > 0 ? value.ApplicantId : Id, Convert.ToInt32(value.TenantSNo), 1, value.ApplicantName);

                //if (Id > 0)
                //{
                value.Id = Id;
                response.Add("Status", "Success");
                response.Add("Message", "Applicant is added successfully...");
                response.Add("ApplicantId", value.ApplicantId > 0 ? value.ApplicantId : Id);
                response.Add("TenantId", TenantId);
                //}
                //else
                //{
                //    response.Add("Status", "Error");
                //    response.Add("Message", "There is something happend while inserting record");
                //}
                //}
                //else
                //{
                //    response.Add("Status", "Error");
                //    response.Add("Message", "Applicant is Already Exists");
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
        /// Updates an applicant.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateApplicant")]
        public async Task<Dictionary<string, object>> UpdateApplicant([FromBody] ApplicantInfo value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                bool applicantExisted = false;

                applicantExisted = await scoresheetRepository.GetApplicantByName(value.ApplicantName);

                if (applicantExisted == false)
                {
                    bool isApplicantExist = scoresheetRepository.UpdateApplicant(value);
                    if (isApplicantExist == true)
                    {
                        response.Add("Status", "Success");
                        response.Add("Message", "Applicant is updated successfully...");
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
                    response.Add("Message", "Applicant is Already Exists");
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
        /// Creates a new income verification in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateIncomeVerfication")]
        public async Task<Dictionary<string, object>> CreateIncomeVerfication([FromBody] IncomeVerfication value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }
                bool incomeVerificationExisted = scoresheetRepository.CreateIncomeVerfication(value);
                if (incomeVerificationExisted == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Income Verfication is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Updates income verifcation in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateIncomeVerfication")]
        public async Task<Dictionary<string, object>> UpdateIncomeVerfication([FromBody] IncomeVerfication value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;

                bool isIncomeVerficationExist = scoresheetRepository.UpdateIncomeVerfication(value);
                if (isIncomeVerficationExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Income Verfication is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Creates credit summary in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateCreditSummary")]
        public async Task<Dictionary<string, object>> CreateCreditSummary([FromBody] CreditSummary value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }

                bool isCreditSummaryExist = scoresheetRepository.CreateCreditSummary(value);
                if (isCreditSummaryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Credit Summary is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Updates credit summary in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateCreditSummary")]
        public async Task<Dictionary<string, object>> UpdateCreditSummary([FromBody] CreditSummary value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {


                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;
                bool isCreditSummaryExist = scoresheetRepository.UpdateCreditSummary(value);
                if (isCreditSummaryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Credit Summary is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// creates landlord in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateLandLordReferences")]
        public async Task<Dictionary<string, object>> CreateLandLordReferences([FromBody] LandLordReferences value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }

                bool isLandLordReferencesExist = scoresheetRepository.CreateLandLordReferences(value);
                if (isLandLordReferencesExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "LandLord References is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Updates landlord in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateLandLordReferences")]
        public async Task<Dictionary<string, object>> UpdateLandLordReferences([FromBody] LandLordReferences value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;

                bool isLandLordReferencesExist = scoresheetRepository.UpdateLandLordReferences(value);
                if (isLandLordReferencesExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "LandLord References is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Creates rental history in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateRentalHistory")]
        public async Task<Dictionary<string, object>> CreateRentalHistory([FromBody] RentalHistory value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }

                bool isRentalHistoryExist = scoresheetRepository.CreateRentalHistory(value);
                if (isRentalHistoryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Rental History is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Updates rental history in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdateRentalHistory")]
        public async Task<Dictionary<string, object>> UpdateRentalHistory([FromBody] RentalHistory value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;

                bool isRentalHistoryExist = scoresheetRepository.UpdateRentalHistory(value);
                if (isRentalHistoryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Rental History is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Create pets in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreatePets")]
        public async Task<Dictionary<string, object>> CreatePets([FromBody] Pets value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }

                bool isPetsExist = scoresheetRepository.CreatePets(value);
                if (isPetsExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Pets is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Update pets in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdatePets")]
        public async Task<Dictionary<string, object>> UpdatePets([FromBody] Pets value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;

                bool isPetsExist = scoresheetRepository.UpdatePets(value);
                if (isPetsExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Pets is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Create points summary in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreatePointsSummary")]
        public async Task<Dictionary<string, object>> CreatePointsSummary([FromBody] PointsSummary value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {

                if (value.TenantId == null)
                {
                    int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy), "");
                    value.TenantId = TenantId;
                }

                bool isPointsSummaryExist = scoresheetRepository.CreatePointsSummary(value);
                if (isPointsSummaryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Points Summary is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Update point summary in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("UpdatePointsSummary")]
        public async Task<Dictionary<string, object>> UpdatePointsSummary([FromBody] PointsSummary value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, Convert.ToInt32(value.CreatedBy));
                //value.TenantId = TenantId;

                bool isPointsSummaryExist = scoresheetRepository.UpdatePointsSummary(value);
                if (isPointsSummaryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Points Summary is updated successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
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
        /// Retrieves a list of all score sheets specific to applicant from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetScroreSheetByApplicantId/{ApplicantId}/{TenantSerialNumber}")]
        public List<Scoresheet> GetScroreSheetByApplicantId(int? ApplicantId, int? TenantSerialNumber)
        {
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            List<Scoresheet> scoresheetList;
            try
            {
                scoresheetList = scoresheetRepository.GetScroreSheetByApplicantId(ApplicantId, TenantSerialNumber);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return scoresheetList;
        }

        /// <summary>
        /// Retrieves a list of all score sheets specific to applicant from the database.
        /// </summary>
        /// <returns>
        /// A list of Users objects, representing all users stored in the database.
        /// </returns>
        [HttpGet]
        [Route("GetCoversheettByApplicantId/{ApplicantId}/")]
        public List<Coversheet> GetCoversheettByApplicantId(int? ApplicantId)
        {
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            List<Coversheet> scoresheetList;
            try
            {
                scoresheetList = scoresheetRepository.GetCoverSheetByApplicantId(ApplicantId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
            return scoresheetList;
        }

        /// <summary>
        /// CreateCoverSheet in the database.
        /// </summary>
        /// <param name="value">The Users object containing the details of the user to be created.</param>
        /// <returns>
        /// A dictionary containing the status and message of the operation.
        /// </returns>
        [HttpPost]
        [Route("CreateCoverSheet")]
        public async Task<Dictionary<string, object>> CreateCoverSheet([FromBody] Coversheet value)
        {
            Dictionary<string, object> response = new();
            ScoresheetRepository scoresheetRepository = new(configuration, connection);
            try
            {
                //int TenantId = scoresheetRepository.CreateTenantInfo(value.ApplicantId, value.TenantSNo, 4);
                //value.TenantId = TenantId;

                bool isPointsSummaryExist = scoresheetRepository.CreateCoverSheet(value);
                if (isPointsSummaryExist == true)
                {
                    response.Add("Status", "Success");
                    response.Add("Message", "Points Summary is added successfully...");
                }
                else
                {
                    response.Add("Status", "Error");
                    response.Add("Message", "There is something happend while inserting record");
                }

            }
            catch (Exception ex)
            {
                response.Add("status", "Error");
                response.Add("Message", ex.Message);
            }
            return response;
        }
    }

    // Other actions for Coversheet if needed...
}



