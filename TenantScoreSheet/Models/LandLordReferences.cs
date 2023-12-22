namespace TenantScoreSheet.Models
{
    public class LandLordReferences
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public bool? RentalReferance { get; set; }
        public int? LandlordType { get; set; }
        public bool? ProperNotice { get; set; }
        public int? ProperNoticePoints { get; set; }
        public int? NSF { get; set; }
        public int? NSFPoints { get; set; }
        public int? LatePayments { get; set; }
        public int? LatePaymentsPoints { get; set; }
        public int? PaymentOrVacantNotices { get; set; }
        public int? PaymentOrVacantNoticesPoints { get; set; }
        public int? TendayComplyNotice { get; set; }
        public int? TendayComplyNoticePoints { get; set; }
        public int? HOAViolations { get; set; }
        public int? HOAViolationsPoints { get; set; }
        public int? PropertyCleanliness { get; set; }
        public int? PropertyCleanlinessPoints { get; set; }
        public bool? Pets { get; set; }
        public int? PetsPoints { get; set; }
        public bool? AdversePetReferance { get; set; }
        public int? AdversePetReferancePoints { get; set; }
        public bool? Rerent { get; set; }
        public int? RerentPoints { get; set; }
        public bool? RentalHistoryLength { get; set; }
      
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
