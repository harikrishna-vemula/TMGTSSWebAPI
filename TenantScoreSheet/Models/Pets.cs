namespace TenantScoreSheet.Models
{
    public class Pets
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? ApplicantId { get; set; }
        public int? TenantSNo { get; set; }
        public int? PetApprovedLandlordReferance1 { get; set; }
        public int? PetApprovedLandlordReferance2 { get; set; }
        public bool? NoOfCatsCompanion { get; set; }
        public int? NoOfCatsCompanions { get; set; }
        public int? NoOfCatsCompanionPoints { get; set; }
        public bool? NoOfLargeDogsCompanion { get; set; }
        public int? NoOfLargeDogsCompanions { get; set; }
        public int? NoOfLargeDogsCompanionPoints { get; set; }
        public bool? NoOfSmallDogsCompanion { get; set; }
        public int? NoOfSmallDogsCompanions { get; set; }
        public int? NoOfSmallDogsCompanionPoints { get; set; }       
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }

    }
}
