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
        public string? IncomeAdequate { get; set; }
        public string? CreditLines { get; set; }
        public string? CreditScore { get; set; }
        public string? CreditScorePoints { get; set; }
        public string? CreditScoreAvailable { get; set; }
        public string? CreditScoreAvailablePoints { get; set; }
        public string? AccountPastDue60Days { get; set; }
        public string? CollectionAccounts { get; set; }
        public string? CollectionAccountsPoints { get; set; }
        public string? MedicalCollections { get; set; }
        public string? PropertyRelatedHousingRecord { get; set; }
        public string? PropertyRelatedHousingRecordPoints { get; set; }
        public string? BankRuptyActive { get; set; }
        public string? BankRuptyActivePoints { get; set; }
        public string? LiensRepossessions { get; set; }
        public string? LiensRepossessionsPoints { get; set; }
        public string? EvectionHistory { get; set; }

        public string? EvectionHistoryPoints { get; set; }
        public string? Class1Felonies { get; set; }
        public string? Class1FeloniesPoints { get; set; }
        public string? Class2Felonies { get; set; }
        public string? Class2FeloniesPoints { get; set; }
        public string? Class1Misdemeaners { get; set; }
        public string? Class1MisdemeanersPoints { get; set; }
        public string? Class2Misdemeaners { get; set; }
        public string? Class2MisdemeanersPoints { get; set; }
        public string? DepositApproved { get; set; }
        public string? DepositToHold { get; set; }
        public string? RentalReferance { get; set; }
        public string? LandlordType { get; set; }

        public string? ProperNotice { get; set; }
        public string? ProperNoticePoints { get; set; }
        public string? NSF { get; set; }
        public string? NSFPoints { get; set; }
        public string? LatePayments { get; set; }
        public string? LatePaymentsPoints { get; set; }
        public string? PaymentOrVacantNotices { get; set; }
        public string? PaymentOrVacantNoticesPoints { get; set; }
        public string? TendayComplyNotice { get; set; }
        public string? TendayComplyNoticePoints { get; set; }
        public string? HOAViolations { get; set; }
        public string? HOAViolationsPoints { get; set; }
        public string? PropertyCleanliness { get; set; }

        public string? PropertyCleanlinessPoints { get; set; }
        public string? Pets { get; set; }
        public string? PetsPoints { get; set; }
        public string? AdversePetReferance { get; set; }
        public string? AdversePetReferancePoints { get; set; }
        public string? Rerent { get; set; }
        public string? RerentPoints { get; set; }
        public string? RentalHistoryLength { get; set; }
        public string? PetApprovedLandlordReferance1 { get; set; }

        public string? PetApprovedLandlordReferance2 { get; set; }
        public string? NoOfCatsCompanion { get; set; }
        public string? NoOfCatsCompanions { get; set; }
        public string? NoOfCatsCompanionPoints { get; set; }
        public string? NoOfLargeDogsCompanion { get; set; }

        public string? NoOfLargeDogsCompanions { get; set; }
        public string? NoOfLargeDogsCompanionPoints { get; set; }
        public string? NoOfSmallDogsCompanion { get; set; }
        public string? NoOfSmallDogsCompanions { get; set; }
        public string? NoOfSmallDogsCompanionPoints { get; set; }
        public string? TotalPoints { get; set; }
        public string? FinalApproval { get; set; }
        public string? TotalDeposit { get; set; }
        public string? DepositToHoldpaid { get; set; }

        public string? PetDeposit { get; set; }
        public string? AdditionalDeposit { get; set; }
        public string? BalanceDepositDue { get; set; }

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

      
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }


    }
}
