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
        public string? AgreementType { get; set; }
        public DateTime? QCDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public TimeOnly? SigningTime { get; set; }
        public string? WithWhom { get; set; }
        public string? OtherTerms { get; set; }
        public string? ListPaidUtilities { get; set; }
        public int? OtherMonthlyCharge11 { get; set; }
        public int? OtherMonthlyCharge12 { get; set; }
        public int? OtherMonthlyCharge21 { get; set; }
        public int? OtherMonthlyCharge22 { get; set; }
        public int? OtherMonthlyCharge31 { get; set; }
        public int? OtherMonthlyCharge32 { get; set; }
        public int? OtherMonthlyCharge41 { get; set; }
        public int? OtherMonthlyCharge42 { get; set; }
        public int? OtherMoveinCharge1 { get; set; }
        public int? OtherMoveinChargePaid1 { get; set; }
        public int? OtherMoveinCharge2 { get; set; }
        public int? OtherMoveinChargePaid2 { get; set; }
        public int? OtherMoveinCharge3 { get; set; }
        public int? OtherMoveinChargePaid3 { get; set; }
        public int? RubsMoveinCharge {  get; set;  }

        public int? RubsMoveinChargePaid { get; set; }
        public int? PrepaidCleaningCharge { get; set; }
        public int? PrepaidCleaningPaid { get; set; }
        public int? SecurityDepositCharge { get; set; }
        public int? SecurityDepositPaid { get; set; }
        public int? NonRefProcessingFeeCharge { get; set; }
        public int? NonRefProcessingFeePaid { get; set; }
        public int? PetDepositCharge { get; set; }
        public int? PetDepositPaid { get; set; }
        public int? PetNonRefFeeCharge { get; set; }
        public int? PetNonRefFeePaid { get; set; }

        public int? AdditionDepositCharge { get; set; }
        public int? AdditionDepositPaid { get; set; }
        public int? SubTotal { get; set; }
        public int? Paid { get; set; }
        public int? DueatMoveinKeyPickup { get; set; }
       
        public string? CreatedBy { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public int? ApplicantId { get; internal set; }
    }
}
