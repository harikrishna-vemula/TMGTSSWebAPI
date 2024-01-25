namespace TenantScoreSheet.Models
{
    public class LandLordReferences
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public bool? RentalReferance { get; set; }
        public double? RentalReferancePoints { get; set; }
        public int? LL1LandlordType { get; set; }
        public bool? LL1ProperNotice { get; set; }
        public double? LL1ProperNoticePoints { get; set; }
        public int? LL1NSF { get; set; }
        public double? LL1NSFPoints { get; set; }
        public int? LL1LatePayments { get; set; }
        public double? LL1LatePaymentsPoints { get; set; }
        public int? LL1PaymentOrVacantNotices { get; set; }
        public double? LL1PaymentOrVacantNoticesPoints { get; set; }
        public int? lL1TendayComplyNotice { get; set; }
        public double? lL1TendayComplyNoticePoints { get; set; }
        public int? LL1HOAViolations { get; set; }
        public double? LL1HOAViolationsPoints { get; set; }
        public int? LL1PropertyCleanliness { get; set; }
        public double? LL1PropertyCleanlinessPoints { get; set; }
        public bool? LL1Pets { get; set; }
        public double? LL1PetsPoints { get; set; }
        public bool? LL1AdversePetReferance { get; set; }
        public double? LL1AdversePetReferancePoints { get; set; }
        public bool? LL1Rerent { get; set; }
        public double? LL1RerentPoints { get; set; }

       
        public int? LL2LandlordType { get; set; }
        public bool? LL2ProperNotice { get; set; }
        public double? LL2ProperNoticePoints { get; set; }
        public int? LL2NSF { get; set; }
        public double? LL2NSFPoints { get; set; }
        public int? LL2LatePayments { get; set; }
        public double? LL2LatePaymentsPoints { get; set; }
        public int? LL2PaymentOrVacantNotices { get; set; }
        public double? LL2PaymentOrVacantNoticesPoints { get; set; }
        public int? lL2TendayComplyNotice { get; set; }
        public double? lL2TendayComplyNoticePoints { get; set; }
        public int? LL2HOAViolations { get; set; }
        public double? LL2HOAViolationsPoints { get; set; }
        public int? LL2PropertyCleanliness { get; set; }
        public double? LL2PropertyCleanlinessPoints { get; set; }
        public bool? LL2Pets { get; set; }
        public int? LL2PetsPoints { get; set; }
        public bool? LL2AdversePetReferance { get; set; }
        public double? LL2AdversePetReferancePoints { get; set; }
        public bool? LL2Rerent { get; set; }
        public double? LL2RerentPoints { get; set; }
        public bool? RentalHistoryLength { get; set; }      
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
