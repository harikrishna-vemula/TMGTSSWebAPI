namespace TenantScoreSheet.Models
{
    public class Scoresheet
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public double? PaystubRecent { get; set; }
        public int? YTD_Earnings { get; set; }
        public double? PaystubRecentMonthly { get; set; }
        public int? BankStatement { get; set; }
        public double? secondPayStub { get; set; }
        public double? BankStatementMonthly { get; set; }
        public double? xRent { get; set; }
        public bool? IncomeAdequate { get; set; }
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
        
        public bool? CreditApproved { get; set; }
        public double? PaidByPrimaryTenant { get; set; }
        public bool? RentalReferance { get; set; }
        public double? RentalReferancePoints { get; set; }
        public int? LL1LandlordType { get; set; }
        public bool? LL1ProperNotice { get; set; }
        public double? LL1ProperNoticePoints { get; set; }
        public int? LL1NSF { get; set; }
        public double? LL1NSFPoints { get; set; }
        public int? LL1LatePayments { get; set; }
        public double? LL1LatePaymentsPoints { get; set; }
        public int? LL1PaymentOrVacantNotices { get; set; }
        public double? LL1PaymentOrVacantNoticesPoints { get; set; }
        public int? lL1TendayComplyNotice { get; set; }
        public double? lL1TendayComplyNoticePoints { get; set; }
        public int? LL1HOAViolations { get; set; }
        public double? LL1HOAViolationsPoints { get; set; }
        public int? LL1PropertyCleanliness { get; set; }
        public double? LL1PropertyCleanlinessPoints { get; set; }
        public bool? LL1Pets { get; set; }
        public double? LL1PetsPoints { get; set; }
        public bool? LL1AdversePetReferance { get; set; }
        public double? LL1AdversePetReferancePoints { get; set; }
        public bool? LL1Rerent { get; set; }
        public double? LL1RerentPoints { get; set; }


        public int? LL2LandlordType { get; set; }
        public bool? LL2ProperNotice { get; set; }
        public double? LL2ProperNoticePoints { get; set; }
        public int? LL2NSF { get; set; }
        public double? LL2NSFPoints { get; set; }
        public int? LL2LatePayments { get; set; }
        public double? LL2LatePaymentsPoints { get; set; }
        public int? LL2PaymentOrVacantNotices { get; set; }
        public double? LL2PaymentOrVacantNoticesPoints { get; set; }
        public int? lL2TendayComplyNotice { get; set; }
        public double? lL2TendayComplyNoticePoints { get; set; }
        public int? LL2HOAViolations { get; set; }
        public double? LL2HOAViolationsPoints { get; set; }
        public int? LL2PropertyCleanliness { get; set; }
        public double? LL2PropertyCleanlinessPoints { get; set; }
        public bool? LL2Pets { get; set; }
        public int? LL2PetsPoints { get; set; }
        public bool? LL2AdversePetReferance { get; set; }
        public double? LL2AdversePetReferancePoints { get; set; }
        public bool? LL2Rerent { get; set; }
        public double? LL2RerentPoints { get; set; }
        public bool? RentalHistoryLength { get; set; }
        public int? PetApprovedLandlordReferance1 { get; set; }

        public int? PetApprovedLandlordReferance2 { get; set; }

        public bool? NoOfCatsCompanion { get; set; }
        public int? NoOfCatsCompanions { get; set; }
        public double? NoOfCatsCompanionPoints { get; set; }
        public bool? NoOfLargeDogsCompanion { get; set; }
        public int? NoOfLargeDogsCompanions { get; set; }
        public double? NoOfLargeDogsCompanionPoints { get; set; }
        public bool? NoOfSmallDogsCompanion { get; set; }
        public int? NoOfSmallDogsCompanions { get; set; }
        public double? NoOfSmallDogsCompanionPoints { get; set; }
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
        public double? MonthlyRent { get; set; }
        public double? Section8Rent { get; set; }

        public double? StandardDepositProperty { get; set; }
        
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
