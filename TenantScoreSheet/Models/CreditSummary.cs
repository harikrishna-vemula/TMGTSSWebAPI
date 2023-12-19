namespace TenantScoreSheet.Models
{
    public class CreditSummary
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? CreditLines { get; set; }
        public int? CreditScore { get; set; }
        public int? CreditScorePoints { get; set; }
        public bool? CreditScoreAvailable { get; set; }
        public int? CreditScoreAvailablePoints { get; set; }
        public int? AccountPastDue60Days { get; set; }
        public int? CollectionAccounts { get; set; }
        public int? CollectionAccountsPoints { get; set; }
        public int? MedicalCollections { get; set; }
        public bool? PropertyRelatedHousingRecord { get; set; }
        public int? PropertyRelatedHousingRecordPoints { get; set; }
        public bool? BankRuptyActive { get; set; }
        public bool? BankRuptyActivePoints { get; set; }
        public int? LiensRepossessions { get; set; }
        public int? LiensRepossessionsPoints { get; set; }
        public int? EvectionHistoryPoints { get; set; }
        public bool? Class1Felonies { get; set; }
        public int? Class1FeloniesPoints { get; set; }
        public DateTime? Class2Felonies { get; set; }
        public int? Class2FeloniesPoints { get; set; }
        public DateTime? Class1Misdemeaners { get; set; }
        public int? Class1MisdemeanersPoints { get; set; }
        public DateTime? Class2Misdemeaners { get; set; }
        public int? Class2MisdemeanersPoints { get; set; }
        public bool? DepositApproved { get; set; }
        public int? DepositToHold { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
