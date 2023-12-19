namespace TenantScoreSheet.Models
{
    public class RentalHistory
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public string? RentalHistoryLength { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
