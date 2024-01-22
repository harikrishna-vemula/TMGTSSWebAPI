namespace TenantScoreSheet.Models
{
    public class Formulae
    {
        public int? Id { get; set; }

        public int? PropertyTypeId { get; set; }

        public int? ApplicantTypeId { get; set; }


        public string? Description { get; set; } = string.Empty!;
        public string? StartValue { get; set; } = string.Empty!;
        public string? EndValue { get; set; } = string.Empty!;
        public string? Calculation { get; set; } = string.Empty!;


        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }

    }
}
