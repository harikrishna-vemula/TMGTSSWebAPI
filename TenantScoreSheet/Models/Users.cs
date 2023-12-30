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
        public string? FirstName { get; set; } = string.Empty!;
        public string? LastName { get; set; } = string.Empty!;
        public string? MiddleName { get; set; } = string.Empty!;
      
        public string? TwoFactorEnabled { get; set; } = string.Empty!;
        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
        public int RoleId { get; set; } = 0;

        public int Id { get; set; }

        public string? RoleName { get; set; }

        public string? Image { get; set; } = string.Empty;

        public string ProfileImage { get; set; } = string.Empty;

        public string? Token { get; set; } = string.Empty;

    }
}
