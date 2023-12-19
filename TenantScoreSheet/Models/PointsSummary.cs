namespace TenantScoreSheet.Models
{
    public class PointsSummary
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }

        public int? TotalPoints { get; set; }
        public bool? FinalApproval { get; set; }
        public int? TotalDeposit { get; set; }
        public int? DepositToHoldpaid { get; set; }
        public int? PetDeposit { get; set; }
        public int? AdditionalDeposit { get; set; }
        public int? BalanceDepositDue { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
