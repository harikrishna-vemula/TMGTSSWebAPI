namespace TenantScoreSheet.Models
{
    public class IncomeVerfication
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public int? PaystubRecent { get; set; }
        public int? YTD_Earnings { get; set; }
        public int? PaystubRecentMonthly { get; set; }
        public int? BankStatement { get; set; }
        public int? secondPayStub { get; set; }
        public int? BankStatementMonthly { get; set; }
        public int? xRent { get; set; }
        public bool? IncomeAdequate { get; set; }       
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
