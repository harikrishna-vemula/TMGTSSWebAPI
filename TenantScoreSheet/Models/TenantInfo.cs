namespace TenantScoreSheet.Models
{
    public class TenantInfo
    {
        public int? Id { get; set; }
        public int? ApplicantId { get; set; }

        public string? ApplicantName { get; set; } = string.Empty!;
        public int? TenantSNo { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
