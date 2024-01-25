namespace TenantScoreSheet.Models
{
    public class CreditSummary
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public bool? CreditLines { get; set; }
        public int? CreditScore { get; set; }
        public double? CreditScorePoints { get; set; }
        public bool? CreditScoreAvailable { get; set; }
        public double? CreditScoreAvailablePoints { get; set; }
        public int? AccountPastDue60Days { get; set; }
        public int? CollectionAccounts { get; set; }
        public double? CollectionAccountsPoints { get; set; }

        public double? CollectionMedicalAccountsPoints { get; set; }
        public int? MedicalCollections { get; set; }
        public bool? PropertyRelatedHousingRecord { get; set; }
        public double? PropertyRelatedHousingRecordPoints { get; set; }
        public int? Bankruptcy { get; set; }
        public double? BankruptcyPoints { get; set; }
        public bool? BankRuptyActive { get; set; }
        public double? BankRuptyActivePoints { get; set; }
        public DateTime? LiensRepossessions { get; set; }
        public double? LiensRepossessionsPoints { get; set; }
        public double? EvectionHistoryPoints { get; set; }
        public DateTime? EvectionHistory { get; set; }
        public bool? Class1Felonies { get; set; }
        public double? Class1FeloniesPoints { get; set; }
        public DateTime? Class2Felonies { get; set; }
        public double? Class2FeloniesPoints { get; set; }
        public DateTime? Class1Misdemeaners { get; set; }
        public double? Class1MisdemeanersPoints { get; set; }
        public DateTime? Class2Misdemeaners { get; set; }
        public double? Class2MisdemeanersPoints { get; set; }
        public bool? DepositApproved { get; set; }
        public double? DepositToHold { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
        public bool? CreditApproved { get; set; }
        public double? PaidByPrimaryTenant { get; set; }
    }
}
