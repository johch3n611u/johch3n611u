using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Model.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Model
{
    public class PAMAccountChange
    {
        public decimal Id { get; set; }
        public string SignFormNo { get; set; }
        public string SignatoryName { get; set; }
        public decimal AccountChangeLogId { get; set; }
        public decimal SignFormId { get; set; }
        public Nullable<int> FunctionType { get; set; }
        public Nullable<int> AccountType { get; set; }
        public string FunctionApplyType { get; set; }
        public string AccountCode { get; set; }
        public Nullable<int> ManageType { get; set; }
        public string DeptNo { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public Nullable<int> UsingType { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdaterEmpNo { get; set; }
        public Nullable<int> RefType { get; set; }
        public Nullable<decimal> LastRefSignFormId { get; set; }
        public Nullable<System.DateTime> UsingEndDate { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string RequireDescription { get; set; }
        public string DomainName { get; set; }
        public string Attachment { get; set; }
        public string EnableCitrixNight { get; set; }
        public string EnableAd { get; set; }
        public string EnableNovell { get; set; }
        public string EnableNotes { get; set; }
        public string EnableExternalMail { get; set; }
        public string EnableInternet { get; set; }
        public string EnablePrint { get; set; }
        public string EnableFax { get; set; }
        public string MobileType { get; set; }
        public string MobileId { get; set; }
        public string Others { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string EnableView { get; set; }
        public string EnableDowload { get; set; }
        public string EnableCopy { get; set; }
        public string EnableUpload { get; set; }
        public string SiteClass { get; set; }
        public string MainAssetNo { get; set; }
        public string SubAssetNo { get; set; }
        public string ComputerName { get; set; }
        public string CompanyCode { get; set; }
        public string PrinterName { get; set; }
        public Nullable<int> PrinterFunction { get; set; }
        public string MailCompany { get; set; }
        public string MailName { get; set; }
        public string MailAddress { get; set; }
        public string NotesMailAccount { get; set; }
        public string NbIdentity { get; set; }
        public string Policy { get; set; }
        public string MacAddress { get; set; }
        public string SiteType { get; set; }
        public Nullable<int> ApplyCount { get; set; }
        public string CompanyName { get; set; }
        public string Customer { get; set; }
        public string Zone { get; set; }
        public string Group { get; set; }
        public string SystemName { get; set; }
        public string CompanyNameEn { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public string ContactIp { get; set; }
        public byte[] FtpPermission { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> PasswordType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameTW { get; set; }
        public string LastNameTW { get; set; }
        public string ManageMethod { get; set; }
        public int? ApplyType { get; set; } = (byte)EnumAccountApplyType.AllDomain;
        public string FlowName { get; set; }
        public string FormStatus { get; set; }
        public DateTime? ApplicanterDate { get; set; }
        public string ApplicanterEmpNo { get; set; }
        public string ApplicanterName { get; set; }
        public string ApplicanterDeptNo { get; set; }
        public DateTime? FinalSignDate { get; set; }
        public string ServiceProject { get; set; }
        public string GroupDesc { get; set; }
        public string Safe { get; set; }

    }

    public class PAMAccountChangeRequest
    {
        public int SignFormId { get; set; }
        public bool IsOneYear { get; set; } = false;
    }

}
