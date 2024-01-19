namespace TenantScoreSheet.Models
{
    public class Scoresheet
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public string? PaystubRecent { get; set; }
        public string? YTD_Earnings { get; set; }
        public string? PaystubRecentMonthly { get; set; }
        public string? BankStatement { get; set; }
        public string? SecondPayStub { get; set; }
        public string? BankStatementMonthly { get; set; }
        public string? xRent { get; set; }
        public bool? IncomeAdequate { get; set; }
        public bool? CreditLines { get; set; }
        public string? CreditScore { get; set; }
        public string? CreditScorePoints { get; set; }
        public bool? CreditScoreAvailable { get; set; }
        public string? CreditScoreAvailablePoints { get; set; }
        public string? AccountPastDue60Days { get; set; }
        public string? CollectionAccounts { get; set; }
        public string? CollectionAccountsPoints { get; set; }
        public string? MedicalCollections { get; set; }
        public bool? PropertyRelatedHousingRecord { get; set; }
        public string? PropertyRelatedHousingRecordPoints { get; set; }

        public int? Bankruptcy { get; set; }
        public int? BankruptcyPoints { get; set; }
        public bool? BankRuptyActive { get; set; }
        public int? BankRuptyActivePoints { get; set; }
        public DateTime? LiensRepossessions { get; set; }
        public string? LiensRepossessionsPoints { get; set; }
        public DateTime? EvectionHistory { get; set; }

        public string? EvectionHistoryPoints { get; set; }
        public bool? Class1Felonies { get; set; }
        public string? Class1FeloniesPoints { get; set; }
        public DateTime? Class2Felonies { get; set; }
        public string? Class2FeloniesPoints { get; set; }
        public DateTime? Class1Misdemeaners { get; set; }
        public string? Class1MisdemeanersPoints { get; set; }
        public DateTime? Class2Misdemeaners { get; set; }
        public string? Class2MisdemeanersPoints { get; set; }
        public bool? DepositApproved { get; set; }
        public string? DepositToHold { get; set; }
        public bool? RentalReferance { get; set; }
        public string? LandlordType { get; set; }

        public bool? LL1ProperNotice { get; set; }
        public int? LL1ProperNoticePoints { get; set; }
        public int? LL1NSF { get; set; }
        public int? LL1NSFPoints { get; set; }
        public int? LL1LatePayments { get; set; }
        public int? LL1LatePaymentsPoints { get; set; }
        public int? LL1PaymentOrVacantNotices { get; set; }
        public int? LL1PaymentOrVacantNoticesPoints { get; set; }
        public int? LL1TendayComplyNotice { get; set; }
        public int? LL1TendayComplyNoticePoints { get; set; }
        public int? LL1HOAViolations { get; set; }
        public int? LL1HOAViolationsPoints { get; set; }
        public int? LL1PropertyCleanliness { get; set; }
        public int? LL1PropertyCleanlinessPoints { get; set; }
        public bool? LL1Pets { get; set; }
        public int? LL1PetsPoints { get; set; }
        public bool? LL1AdversePetReferance { get; set; }
        public int? LL1AdversePetReferancePoints { get; set; }
        public bool? LL1Rerent { get; set; }
        public int? LL1RerentPoints { get; set; }

        public bool? LL2ProperNotice { get; set; }
        public int? LL2ProperNoticePoints { get; set; }
        public int? LL2NSF { get; set; }
        public int? LL2NSFPoints { get; set; }
        public int? LL2LatePayments { get; set; }
        public int? LL2LatePaymentsPoints { get; set; }
        public int? LL2PaymentOrVacantNotices { get; set; }
        public int? LL2PaymentOrVacantNoticesPoints { get; set; }
        public int? LL2TendayComplyNotice { get; set; }
        public int? LL2TendayComplyNoticePoints { get; set; }
        public int? LL2HOAViolations { get; set; }
        public int? LL2HOAViolationsPoints { get; set; }
        public int? LL2PropertyCleanliness { get; set; }
        public int? LL2PropertyCleanlinessPoints { get; set; }
        public bool? LL2Pets { get; set; }
        public int? LL2PetsPoints { get; set; }
        public bool? LL2AdversePetReferance { get; set; }
        public int? LL2AdversePetReferancePoints { get; set; }
        public bool? LL2Rerent { get; set; }
        public int? LL2RerentPoints { get; set; }
        public bool? RentalHistoryLength { get; set; }
        public string? PetApprovedLandlordReferance1 { get; set; }

        public string? PetApprovedLandlordReferance2 { get; set; }
        public bool? NoOfCatsCompanion { get; set; }
        public string? NoOfCatsCompanions { get; set; }
        public string? NoOfCatsCompanionPoints { get; set; }
        public bool? NoOfLargeDogsCompanion { get; set; }

        public string? NoOfLargeDogsCompanions { get; set; }
        public string? NoOfLargeDogsCompanionPoints { get; set; }
        public bool? NoOfSmallDogsCompanion { get; set; }
        public string? NoOfSmallDogsCompanions { get; set; }
        public string? NoOfSmallDogsCompanionPoints { get; set; }
        public double? TotalPoints { get; set; }
        public bool? FinalApproval { get; set; }
        public double? TotalDeposit { get; set; }
        public double? DepositToHoldpaid { get; set; }

        public double? PetDeposit { get; set; }
        public double? AdditionalDeposit { get; set; }
        public double? BalanceDepositDue { get; set; }

        public string? ApplicantName { get; set; }
        public string? Property { get; set; }
        public int? ApplicantTypeId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public string? Zip { get; set; }
        public string? MonthlyRent { get; set; }
        public string? Section8Rent { get; set; }

        public string? StandardDepositProperty { get; set; }
        
        public int? PropertyTypeId { get; set; }
        public string? ApplicantType { get; set; }
        public string? PropertyType { get; set; }
        public int? ApplicationStatusId { get; set; }

      
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }


    }
}
