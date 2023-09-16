﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Contact2 { get; set; }
        public string Email { get; set; }
        public bool IsFixedEmployee { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string WorkPermitID { get; set; }
        public DateTime? WorkPEDate { get; set; }
        public DateTime? ResidentPDExpire { get; set; }
        public string Address { get; set; }
        public int JobTitleId { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Accomodation { get; set; }
        public DateTime? MedicalExpiryDate { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? OtherAllowance { get; set; }
        public decimal? NetSalary { get; set; }
        public string EmiratesId { get; set; }
        public DateTime EmiratesIdExpire { get; set; }
        public string DamanNo { get; set; }
        public DateTime DamanNoExpire { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? UserRoleId { get; set; }


        [ForeignKey("UserRoleId")]
        public UserRole UserRole { get; set; }
        [ForeignKey("JobTitleId")]
        public MasterJobTitle MasterJobTitle { get; set; }

        //public List<MonthlyAttendence> MonthlyAttendences { get; set; }
        public User User { get; set; }
        //[NotMapped]
        //public List<Order> Orders { get; set; }
        //[NotMapped]
        //public List<OrderDetail> OrderDetails { get; set; }
        //public List<EmployeeAdvancePayment> EmployeeAdvancePayments { get; set; }
    }
}
