using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Expert { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Contact2 { get; set; }
        public string EmiratesId { get; set; }
        public DateTime EmiratesIdExpire { get; set; }
        public string DamanNo { get; set; }
        public DateTime DamanNoExpire { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? OtherAllowance { get; set; }
        public decimal? NetSalary { get; set; }
        public bool IsFixedEmployee { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string WorkPermitID { get; set; }
        public DateTime? WorkPEDate { get; set; }
        public DateTime? ResidentPDExpire { get; set; }
        public string Address { get; set; }
        public int JobTitleId { get; set; }
        public int UserRoleId { get; set; }
        public string Role { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Accomodation { get; set; }
        public bool IsActive { get; set; }
        public DateTime? MedicalExpiryDate { get; set; }
        public List<EmployeeAdvancePaymentResponse> EmployeeAdvancePayments { get; set; }
    }
}
