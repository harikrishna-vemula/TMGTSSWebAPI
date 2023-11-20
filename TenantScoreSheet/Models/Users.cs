namespace TenantScoreSheet.Models
{
    public class Users
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty!;
        public string? Email { get; set; } = string.Empty!;
        public string? Phone { get; set; } = string.Empty!;
        public string? Address { get; set; } = string.Empty!;
        public string? Status { get; set; } = string.Empty!;
        public int RoleId { get; set; }

        public int Id { get; set; }

        public string? RoleName { get; set; }
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
