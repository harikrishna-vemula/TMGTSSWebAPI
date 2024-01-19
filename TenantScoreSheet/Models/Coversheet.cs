namespace TenantScoreSheet.Models
{
    public class Coversheet
    {
        public int? Id { get; set; }
        public string? PrimaryTenant { get; set; }
        public string? Tenant2 { get; set; }
        public string? Tenant3 { get; set; }
        public string? Tenant4 { get; set; }
        public string? PropertyManager { get; set; }
        public string? PropertyAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? UnitCode { get; set; }
        public string? BestPOC { get; set; }
        public DateTime? RentReadyDate { get; set; }
        public DateTime? DepositPaidDate { get; set; }
        public DateTime? RentResponsibleDate { get; set; }
        public int? AgreementType { get; set; }
        public DateTime? QCDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public TimeOnly? SigningTime { get; set; }
        public string? WithWhom { get; set; }
        public string? OtherTerms { get; set; }
        public string? ListPaidUtilities { get; set; }
        public double? MonthlyRent { get; set; }
        public double? MoveinRentCharge { get; set; }
        public double? MoveinRentPaid { get; set; }
        public double? OtherMonthlyCharge11 { get; set; }
        public string OtherMonthlyCharge12 { get; set; }
        public double? OtherMonthlyCharge21 { get; set; }
        public string OtherMonthlyCharge22 { get; set; }
        public double? OtherMonthlyCharge31 { get; set; }
        public string OtherMonthlyCharge32 { get; set; }
        public double? OtherMonthlyCharge41 { get; set; }
        public string OtherMonthlyCharge42 { get; set; }
        public double? OtherMoveinCharge1 { get; set; }
        public double? OtherMoveinChargePaid1 { get; set; }
        public double? OtherMoveinCharge2 { get; set; }
        public double? OtherMoveinChargePaid2 { get; set; }
        public double? OtherMoveinCharge3 { get; set; }
        public double? OtherMoveinChargePaid3 { get; set; }
        public double? RubsMoveinCharge {  get; set;  }

        public double? RubsMoveinChargePaid { get; set; }
        public double? PrepaidCleaningCharge { get; set; }
        public double? PrepaidCleaningPaid { get; set; }
        public double? SecurityDepositCharge { get; set; }
        public double? SecurityDepositPaid { get; set; }
        public double? NonRefProcessingFeeCharge { get; set; }
        public double? NonRefProcessingFeePaid { get; set; }
        public double? PetDepositCharge { get; set; }
        public double? PetDepositPaid { get; set; }
        public double? PetNonRefFeeCharge { get; set; }
        public double? PetNonRefFeePaid { get; set; }

        public double? AdditionDepositCharge { get; set; }
        public double? AdditionDepositPaid { get; set; }
        public double? SubTotal { get; set; }
        public double? Paid { get; set; }
        public double? DueatMoveinKeyPickup { get; set; }
       
        public string? CreatedBy { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public int? ApplicantId { get; set; }
    }
}
