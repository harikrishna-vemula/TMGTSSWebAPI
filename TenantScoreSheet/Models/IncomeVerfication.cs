namespace TenantScoreSheet.Models
{
    public class IncomeVerfication
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
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
