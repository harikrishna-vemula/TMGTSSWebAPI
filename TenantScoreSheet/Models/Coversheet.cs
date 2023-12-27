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
        public string? QCDate { get; set; }
        public string? SigningDate { get; set; }
        public string? SigningTime { get; set; }
        public string? WithWhom { get; set; }
        public string? OtherTerms { get; set; }
        public string? ListPaidUtilities { get; set; }
        public string? OtherMonthlyCharge11 { get; set; }
        public string? OtherMonthlyCharge12 { get; set; }
        public string? OtherMonthlyCharge21 { get; set; }
        public string? OtherMonthlyCharge22 { get; set; }
        public string? OtherMonthlyCharge31 { get; set; }
        public string? OtherMonthlyCharge32 { get; set; }
        public string? OtherMonthlyCharge41 { get; set; }
        public string? OtherMonthlyCharge42 { get; set; }
        public string? OtherMoveinCharge1 { get; set; }
        public string? OtherMoveinChargePaid1 { get; set; }
        public string? OtherMoveinCharge2 { get; set; }
        public string? OtherMoveinChargePaid2 { get; set; }
        public string? OtherMoveinCharge3 { get; set; }
        public string? OtherMoveinChargePaid3 { get; set; }
        public string? RubsMoveinCharge {  get; set;  }

        public string? RubsMoveinChargePaid { get; set; }
        public string? PrepaidCleaningCharge { get; set; }
        public string? PrepaidCleaningPaid { get; set; }
        public string? SecurityDepositCharge { get; set; }
        public string? SecurityDepositPaid { get; set; }
        public string? NonRefProcessingFeeCharge { get; set; }
        public string? NonRefProcessingFeePaid { get; set; }
        public string? PetDepositCharge { get; set; }
        public string? PetDepositPaid { get; set; }
        public string? PetNonRefFeeCharge { get; set; }
        public string? PetNonRefFeePaid { get; set; }

        public string? AdditionDepositCharge { get; set; }
        public string? AdditionDepositPaid { get; set; }
        public string? SubTotal { get; set; }
        public string? Paid { get; set; }
        public string? DueatMoveinKeyPickup { get; set; }
       
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
