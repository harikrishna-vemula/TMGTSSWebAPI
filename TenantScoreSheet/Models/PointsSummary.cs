namespace TenantScoreSheet.Models
{
    public class PointsSummary
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }

        public double? TotalPoints { get; set; }
        public bool? FinalApproval { get; set; }
        public double? TotalDeposit { get; set; }
        public double? DepositToHoldpaid { get; set; }
        public double? PetDeposit { get; set; }
        public double? AdditionalDeposit { get; set; }
        public double? BalanceDepositDue { get; set; }

        public double? BalanceDepositDuePoints { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
