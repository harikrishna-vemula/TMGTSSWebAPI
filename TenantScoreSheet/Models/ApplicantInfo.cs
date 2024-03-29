﻿namespace TenantScoreSheet.Models
{
    public class ApplicantInfo
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }
        public string? ApplicantName { get; set; } = string.Empty!;

        public int ApplicationStatusId { get; set; }
        public string? ApplicationStatus { get; set; } = string.Empty!;

        public int? TenantId { get; set; }

        public string? TenantSNo { get; set; } 
        public string? accountname { get; set; } = string.Empty!;
        public string? Property { get; set; } = string.Empty!;

        public int? ApplicantTypeId { get; set; }

        public string? ApplicantType { get; set; } = string.Empty!;


        public string? City { get; set; } = string.Empty!;

        public string? State { get; set; } = string.Empty!;

        public string? Zip { get; set; } = string.Empty!;

        public double? MonthlyRent { get; set; } 

        public double? Section8Rent { get; set; } 

        public double? StandardDepositProperty { get; set; } 

        public double? PetDeposit { get; set; } 

        public int? PropertyTypeId { get; set; }

        public string? PropertyType { get; set; } = string.Empty!;

        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
        //public basicinfo? basicinfo  { get; set; } = new basicinfo();

    }
    public class basicinfo
    {
        public int Id { get; set; }
        public string? ApplicantName { get; set; } = string.Empty!;
        public string? Property { get; set; } = string.Empty!;

        public int? ApplicantTypeId { get; set; }

        public string? ApplicantType { get; set; } = string.Empty!;


        public string? City { get; set; } = string.Empty!;

        public string? State { get; set; } = string.Empty!;

        public string? Zip { get; set; } = string.Empty!;

        public string? MonthlyRent { get; set; } = string.Empty!;

        public string? Section8Rent { get; set; } = string.Empty!;

        public string? StandardDepositProperty { get; set; } = string.Empty!;

        public string? PetDeposit { get; set; } = string.Empty!;

        public int? PropertyTypeId { get; set; }

        public string? PropertyType { get; set; } = string.Empty!;

        public string? CreatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty!;
        public DateTime? ModifiedDate { get; set; }
    }
}
