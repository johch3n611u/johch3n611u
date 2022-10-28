using AutoMapper;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Interface;
using Mxic.ITC.PAM.Model;
using Mxic.ITC.PAM.Model.Business;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Helper;
using Mxic.ITC.PAM.Model.Sign;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Repository.Repository
{
    public class PAMapplyRepository : RepositoryBase, ISignRepository<AccountApplyForm>
    {
        private readonly IMapper _mapper;
        //public Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public PAMapplyRepository()
        {

            var config = new MapperConfiguration(
                cfg =>
                {

                    cfg.CreateMap<SignFormMain, SIGN_FORM_MAIN>()
                       .ForMember(s => s.APPLICANTER_DEPT_NO, opt => opt.MapFrom(s => s.ApplicanterDeptNO))
                       .ForMember(s => s.REQUIRED_DATE, opt => opt.MapFrom(s => s.RequiredDate))
                       .ForMember(s => s.REQUIRED_DESCRIPTION, opt => opt.MapFrom(s => s.RequiredDesc))
                       .ForMember(s => s.APPLICANTER_EMP_NO, opt => opt.MapFrom(s => s.ApplicanterEmpNO))
                       .ForMember(s => s.APPLICANTER_NAME, opt => opt.MapFrom(s => s.ApplicanterName))
                       .ForMember(s => s.CREATE_DATE, opt => opt.MapFrom(s => s.CreateDate))
                       .ForMember(s => s.FILLER_DEPT_NO, opt => opt.MapFrom(s => s.FillerDeptNO))
                       .ForMember(s => s.FILLER_EMP_NO, opt => opt.MapFrom(s => s.FillerEmpNO))
                       .ForMember(s => s.FILLER_NAME, opt => opt.MapFrom(s => s.FillerName))
                       .ForMember(s => s.FINAL_SIGN_DATE, opt => opt.MapFrom(s => s.FinalSignDate))
                       .ForMember(s => s.FLOW_ID, opt => opt.MapFrom(s => s.FlowID))
                       .ForMember(s => s.FORM_STATUS, opt => opt.MapFrom(s => s.FormStatus))
                       .ForMember(s => s.FORM_TYPE, opt => opt.MapFrom(s => s.FormType))
                       .ForMember(s => s.RELATED_MAIN, opt => opt.MapFrom(s => s.RelatedMain))
                       .ForMember(s => s.SERVICE_CODE, opt => opt.MapFrom(s => s.ServiceCode))
                       .ForMember(s => s.SIGN_FORM_NO, opt => opt.MapFrom(s => s.SignFromNo))
                       .ForMember(s => s.SIGN_FORM_ID, opt => opt.MapFrom(s => s.SignFromID));

                    cfg.CreateMap<SIGN_FORM_MAIN, SignFormMain>()
                        .ForMember(s => s.ApplicanterDeptNO, opt => opt.MapFrom(s => s.APPLICANTER_DEPT_NO))
                        .ForMember(s => s.RequiredDesc, opt => opt.MapFrom(s => s.REQUIRED_DESCRIPTION))
                        .ForMember(s => s.RequiredDate, opt => opt.MapFrom(s => s.REQUIRED_DATE))
                        .ForMember(s => s.ApplicanterEmpNO, opt => opt.MapFrom(s => s.APPLICANTER_EMP_NO))
                        .ForMember(s => s.ApplicanterName, opt => opt.MapFrom(s => s.APPLICANTER_NAME))
                        .ForMember(s => s.CreateDate, opt => opt.MapFrom(s => s.CREATE_DATE))
                        .ForMember(s => s.FillerDeptNO, opt => opt.MapFrom(s => s.FILLER_DEPT_NO))
                        .ForMember(s => s.FillerEmpNO, opt => opt.MapFrom(s => s.FILLER_EMP_NO))
                        .ForMember(s => s.FillerName, opt => opt.MapFrom(s => s.FILLER_NAME))
                        .ForMember(s => s.FinalSignDate, opt => opt.MapFrom(s => s.FINAL_SIGN_DATE))
                        .ForMember(s => s.FlowID, opt => opt.MapFrom(s => s.FLOW_ID))
                        .ForMember(s => s.FormStatus, opt => opt.MapFrom(s => s.FORM_STATUS))
                        .ForMember(s => s.FormType, opt => opt.MapFrom(s => s.FORM_TYPE))
                        .ForMember(s => s.RelatedMain, opt => opt.MapFrom(s => s.RELATED_MAIN))
                        .ForMember(s => s.ServiceCode, opt => opt.MapFrom(s => s.SERVICE_CODE))
                        .ForMember(s => s.SignFromNo, opt => opt.MapFrom(s => s.SIGN_FORM_NO))
                        .ForMember(s => s.SignFromID, opt => opt.MapFrom(s => s.SIGN_FORM_ID));

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, AccountApplyBase>()
                        .ForMember(s => s.Id, opt => opt.MapFrom(s => s.ID))
                        .ForMember(s => s.FunctionType, opt => opt.MapFrom(s => s.FUNCTION_TYPE))
                        .ForMember(s => s.AccountType, opt => opt.MapFrom(s => s.ACCOUNT_TYPE))
                        .ForMember(s => s.AccountCode, opt => opt.MapFrom(s => s.ACCOUNT_CODE))
                        .ForMember(s => s.ManageType, opt => opt.MapFrom(s => s.MANAGE_TYPE))
                        .ForMember(s => s.DeptNo, opt => opt.MapFrom(s => s.DEPT_NO))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.CreateDate, opt => opt.MapFrom(s => s.CREATE_DATE))
                        .ForMember(s => s.UpdateDate, opt => opt.MapFrom(s => s.UPDATE_DATE))
                        .ForMember(s => s.UpdaterEmpNo, opt => opt.MapFrom(s => s.UPDATER_EMP_NO))
                        .ForMember(s => s.RefType, opt => opt.MapFrom(s => s.REF_TYPE))
                        .ForMember(s => s.UsingEndDate, opt => opt.MapFrom(s => s.USING_END_DATE))
                        .ForMember(s => s.Status, opt => opt.MapFrom(s => s.STATUS))
                        .ForMember(s => s.DisableDate, opt => opt.MapFrom(s => s.DISABLE_DATE))
                        ;

                    cfg.CreateMap<AccountApplyBase, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.ID, opt => opt.MapFrom(s => s.Id))
                        .ForMember(s => s.FUNCTION_TYPE, opt => opt.MapFrom(s => s.FunctionType))
                        .ForMember(s => s.ACCOUNT_TYPE, opt => opt.MapFrom(s => s.AccountType))
                        .ForMember(s => s.ACCOUNT_CODE, opt => opt.MapFrom(s => s.AccountCode))
                        .ForMember(s => s.MANAGE_TYPE, opt => opt.MapFrom(s => s.ManageType))
                        .ForMember(s => s.DEPT_NO, opt => opt.MapFrom(s => s.DeptNo))
                        .ForMember(s => s.EMP_NO, opt => opt.MapFrom(s => s.EmpNo))
                        .ForMember(s => s.EMP_NAME, opt => opt.MapFrom(s => s.EmpName))
                        .ForMember(s => s.USING_TYPE, opt => opt.MapFrom(s => s.UsingType))
                        .ForMember(s => s.CREATE_DATE, opt => opt.MapFrom(s => s.CreateDate))
                        .ForMember(s => s.UPDATE_DATE, opt => opt.MapFrom(s => s.UpdateDate))
                        .ForMember(s => s.UPDATER_EMP_NO, opt => opt.MapFrom(s => s.UpdaterEmpNo))
                        .ForMember(s => s.REF_TYPE, opt => opt.MapFrom(s => s.RefType))
                        .ForMember(s => s.USING_END_DATE, opt => opt.MapFrom(s => s.UsingEndDate))
                        .ForMember(s => s.STATUS, opt => opt.MapFrom(s => s.Status))
                        .ForMember(s => s.DISABLE_DATE, opt => opt.MapFrom(s => s.DisableDate))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, ComputerAccount>()
                        .ForMember(s => s.RequireDescription, opt => opt.MapFrom(s => s.REQUIRE_DESCRIPTION))
                        .ForMember(s => s.EnableAd, opt => opt.MapFrom(s => s.ENABLE_AD))
                        .ForMember(s => s.EnableNovell, opt => opt.MapFrom(s => s.ENABLE_NOVELL))
                        .ForMember(s => s.EnableNotes, opt => opt.MapFrom(s => s.ENABLE_NOTES))
                        .ForMember(s => s.EnableExternalMail, opt => opt.MapFrom(s => s.ENABLE_EXTERNAL_MAIL))
                        .ForMember(s => s.EnableInternet, opt => opt.MapFrom(s => s.ENABLE_INTERNET == "Y" ? true : false))
                        .ForMember(s => s.EnablePrint, opt => opt.MapFrom(s => s.ENABLE_PRINT == "Y" ? true : false))
                        .ForMember(s => s.EnableFax, opt => opt.MapFrom(s => s.ENABLE_FAX == "Y" ? true : false))
                        .ForMember(s => s.SystemName, opt => opt.MapFrom(s => s.SYSTEM_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.PasswordType, opt => opt.MapFrom(s => s.PASSWORD_TYPE))
                        .ForMember(s => s.FirstName, opt => opt.MapFrom(s => s.FIRST_NAME))
                        .ForMember(s => s.LastName, opt => opt.MapFrom(s => s.LAST_NAME))
                        .ForMember(s => s.FirstNameTW, opt => opt.MapFrom(s => s.FIRST_NAME_TW))
                        .ForMember(s => s.ManageMethod, opt => opt.MapFrom(s => s.MANAGE_METHOD))
                        .ForMember(s => s.AccountType, opt => opt.MapFrom(s => s.ACCOUNT_TYPE))
                        ;
                    cfg.CreateMap<ComputerAccount, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.REQUIRE_DESCRIPTION, opt => opt.MapFrom(s => s.RequireDescription))
                        .ForMember(s => s.ENABLE_AD, opt => opt.MapFrom(s => s.EnableAd))
                        .ForMember(s => s.ENABLE_NOVELL, opt => opt.MapFrom(s => s.EnableNovell))
                        .ForMember(s => s.ENABLE_NOTES, opt => opt.MapFrom(s => s.EnableNotes))
                        .ForMember(s => s.ENABLE_EXTERNAL_MAIL, opt => opt.MapFrom(s => s.EnableExternalMail))
                        .ForMember(s => s.ENABLE_INTERNET, opt => opt.MapFrom(s => s.EnableInternet))
                        .ForMember(s => s.ENABLE_PRINT, opt => opt.MapFrom(s => s.EnablePrint))
                        .ForMember(s => s.ENABLE_FAX, opt => opt.MapFrom(s => s.EnableFax))
                        .ForMember(s => s.SYSTEM_NAME, opt => opt.MapFrom(s => s.SystemName))
                        .ForMember(s => s.PASSWORD_TYPE, opt => opt.MapFrom(s => s.PasswordType))
                        .ForMember(s => s.FIRST_NAME, opt => opt.MapFrom(s => s.FirstName))
                        .ForMember(s => s.LAST_NAME, opt => opt.MapFrom(s => s.LastName))
                        .ForMember(s => s.FIRST_NAME_TW, opt => opt.MapFrom(s => s.FirstNameTW))
                        .ForMember(s => s.MANAGE_METHOD, opt => opt.MapFrom(s => s.ManageMethod))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, PushMail>()
                        .ForMember(s => s.MobileType, opt => opt.MapFrom(s => s.MOBILE_TYPE))
                        .ForMember(s => s.MobileId, opt => opt.MapFrom(s => s.MOBILE_ID))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        ;
                    cfg.CreateMap<PushMail, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MOBILE_TYPE, opt => opt.MapFrom(s => s.MobileType))
                        .ForMember(s => s.MOBILE_ID, opt => opt.MapFrom(s => s.MobileId))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, Citrix>()
                        .ForMember(s => s.EnableCitrixNight, opt => opt.MapFrom(s => s.ENABLE_CITRIX_NIGHT))
                        .ForMember(s => s.Others, opt => opt.MapFrom(s => s.OTHERS))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        ;
                    cfg.CreateMap<Citrix, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.ENABLE_CITRIX_NIGHT, opt => opt.MapFrom(s => s.EnableCitrixNight))
                        .ForMember(s => s.OTHERS, opt => opt.MapFrom(s => s.Others))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, Websense>()
                        .ForMember(s => s.SiteName, opt => opt.MapFrom(s => s.SITE_NAME))
                        .ForMember(s => s.SiteUrl, opt => opt.MapFrom(s => s.SITE_URL))
                        .ForMember(s => s.EnableView, opt => opt.MapFrom(s => s.ENABLE_VIEW == "Y" ? "true" : "false"))
                        .ForMember(s => s.EnableDowload, opt => opt.MapFrom(s => s.ENABLE_DOWNLOAD == "Y" ? "true" : "false"))
                        .ForMember(s => s.EnableCopy, opt => opt.MapFrom(s => s.ENABLE_COPY == "Y" ? "true" : "false"))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.EnableUpload, opt => opt.MapFrom(s => s.ENABLE_UPLOAD == "Y" ? "true" : "false"))
                        .ForMember(s => s.SiteClass, opt => opt.MapFrom(s => s.SITE_CLASS))
                        .ForMember(s => s.EnableInternet, opt => opt.MapFrom(s => s.ENABLE_INTERNET))
                        ;
                    cfg.CreateMap<Websense, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.SITE_NAME, opt => opt.MapFrom(s => s.SiteName))
                        .ForMember(s => s.SITE_URL, opt => opt.MapFrom(s => s.SiteUrl))
                        .ForMember(s => s.ENABLE_VIEW, opt => opt.MapFrom(s => s.EnableView))
                        .ForMember(s => s.ENABLE_DOWNLOAD, opt => opt.MapFrom(s => s.EnableDowload))
                        .ForMember(s => s.ENABLE_COPY, opt => opt.MapFrom(s => s.EnableCopy))
                        .ForMember(s => s.ENABLE_UPLOAD, opt => opt.MapFrom(s => s.EnableUpload))
                        .ForMember(s => s.SITE_CLASS, opt => opt.MapFrom(s => s.SiteClass))
                        .ForMember(s => s.ENABLE_INTERNET, opt => opt.MapFrom(s => s.EnableInternet))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, LocalDomain>()
                        .ForMember(s => s.MainAssetNo, opt => opt.MapFrom(s => s.MAIN_ASSET_NO))
                        .ForMember(s => s.SubAssetNo, opt => opt.MapFrom(s => s.SUB_ASSET_NO))
                        .ForMember(s => s.ComputerName, opt => opt.MapFrom(s => s.COMPUTER_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.CompanyCode, opt => opt.MapFrom(s => s.COMPANY_CODE))
                        ;
                    cfg.CreateMap<LocalDomain, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MAIN_ASSET_NO, opt => opt.MapFrom(s => s.MainAssetNo))
                        .ForMember(s => s.SUB_ASSET_NO, opt => opt.MapFrom(s => s.SubAssetNo))
                        .ForMember(s => s.COMPUTER_NAME, opt => opt.MapFrom(s => s.ComputerName))
                        .ForMember(s => s.COMPANY_CODE, opt => opt.MapFrom(s => s.CompanyCode))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, NetworkPrinting>()
                        .ForMember(s => s.PrinterName, opt => opt.MapFrom(s => s.PRINTER_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.PrinterFunction, opt => opt.MapFrom(s => s.PRINTER_FUNCTION))
                        ;
                    cfg.CreateMap<NetworkPrinting, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.PRINTER_NAME, opt => opt.MapFrom(s => s.PrinterName))
                        .ForMember(s => s.PRINTER_FUNCTION, opt => opt.MapFrom(s => s.PrinterFunction))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, MailOut>()
                        .ForMember(s => s.MailCompany, opt => opt.MapFrom(s => s.MAIL_COMPANY))
                        .ForMember(s => s.MailName, opt => opt.MapFrom(s => s.MAIL_NAME))
                        .ForMember(s => s.MailAddress, opt => opt.MapFrom(s => s.MAIL_ADDRESS))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.NotesMailAccount, opt => opt.MapFrom(s => s.NOTES_MAIL_ACCOUNT))
                        ;
                    cfg.CreateMap<MailOut, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MAIL_COMPANY, opt => opt.MapFrom(s => s.MailCompany))
                        .ForMember(s => s.MAIL_NAME, opt => opt.MapFrom(s => s.MailName))
                        .ForMember(s => s.MAIL_ADDRESS, opt => opt.MapFrom(s => s.MailAddress))
                        .ForMember(s => s.NOTES_MAIL_ACCOUNT, opt => opt.MapFrom(s => s.NotesMailAccount))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, NB>()
                        .ForMember(s => s.MainAssetNo, opt => opt.MapFrom(s => s.MAIN_ASSET_NO))
                        .ForMember(s => s.SubAssetNo, opt => opt.MapFrom(s => s.SUB_ASSET_NO))
                        .ForMember(s => s.ComputerName, opt => opt.MapFrom(s => s.COMPUTER_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.CompanyCode, opt => opt.MapFrom(s => s.COMPANY_CODE))
                        .ForMember(s => s.NbIdentity, opt => opt.MapFrom(s => s.NB_IDENTITY))
                        ;
                    cfg.CreateMap<NB, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MAIN_ASSET_NO, opt => opt.MapFrom(s => s.MainAssetNo))
                        .ForMember(s => s.SUB_ASSET_NO, opt => opt.MapFrom(s => s.SubAssetNo))
                        .ForMember(s => s.COMPUTER_NAME, opt => opt.MapFrom(s => s.ComputerName))
                        .ForMember(s => s.COMPANY_CODE, opt => opt.MapFrom(s => s.CompanyCode))
                        .ForMember(s => s.NB_IDENTITY, opt => opt.MapFrom(s => s.NbIdentity))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, FTP>()
                        .ForMember(s => s.SystemName, opt => opt.MapFrom(s => s.SYSTEM_NAME))
                        .ForMember(s => s.CompanyNameEn, opt => opt.MapFrom(s => s.COMPANY_NAME_EN))
                        .ForMember(s => s.ContactName, opt => opt.MapFrom(s => s.CONTACT_NAME))
                        .ForMember(s => s.ContactMail, opt => opt.MapFrom(s => s.CONTACT_MAIL))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.ContactIp, opt => opt.MapFrom(s => s.CONTACT_IP))
                        .ForMember(s => s.FtpPermission, opt => opt.MapFrom(s => s.FTP_PERMISSION))
                        ;
                    cfg.CreateMap<FTP, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.SYSTEM_NAME, opt => opt.MapFrom(s => s.SystemName))
                        .ForMember(s => s.COMPANY_NAME_EN, opt => opt.MapFrom(s => s.CompanyNameEn))
                        .ForMember(s => s.CONTACT_NAME, opt => opt.MapFrom(s => s.ContactName))
                        .ForMember(s => s.CONTACT_MAIL, opt => opt.MapFrom(s => s.ContactMail))
                        .ForMember(s => s.CONTACT_IP, opt => opt.MapFrom(s => s.ContactIp))
                        .ForMember(s => s.FTP_PERMISSION, opt => opt.MapFrom(s => s.FtpPermission))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, ComputerOthers>()
                        .ForMember(s => s.MainAssetNo, opt => opt.MapFrom(s => s.MAIN_ASSET_NO))
                        .ForMember(s => s.SubAssetNo, opt => opt.MapFrom(s => s.SUB_ASSET_NO))
                        .ForMember(s => s.ComputerName, opt => opt.MapFrom(s => s.COMPUTER_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.CompanyCode, opt => opt.MapFrom(s => s.COMPANY_CODE))
                        .ForMember(s => s.Policy, opt => opt.MapFrom(s => s.POLICY))
                        ;
                    cfg.CreateMap<ComputerOthers, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MAIN_ASSET_NO, opt => opt.MapFrom(s => s.MainAssetNo))
                        .ForMember(s => s.SUB_ASSET_NO, opt => opt.MapFrom(s => s.SubAssetNo))
                        .ForMember(s => s.COMPUTER_NAME, opt => opt.MapFrom(s => s.ComputerName))
                        .ForMember(s => s.COMPANY_CODE, opt => opt.MapFrom(s => s.CompanyCode))
                        .ForMember(s => s.POLICY, opt => opt.MapFrom(s => s.Policy))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, MobileWifi>()
                        .ForMember(s => s.MacAddress, opt => opt.MapFrom(s => s.MAC_ADDRESS))
                        .ForMember(s => s.SiteType, opt => opt.MapFrom(s => s.SITE_TYPE))
                        .ForMember(s => s.MobileId, opt => opt.MapFrom(s => s.MOBILE_ID))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.MobileType, opt => opt.MapFrom(s => s.MOBILE_TYPE))
                        ;
                    cfg.CreateMap<MobileWifi, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.MAC_ADDRESS, opt => opt.MapFrom(s => s.MacAddress))
                        .ForMember(s => s.SITE_TYPE, opt => opt.MapFrom(s => s.SiteType))
                        .ForMember(s => s.MOBILE_ID, opt => opt.MapFrom(s => s.MobileId))
                        .ForMember(s => s.MOBILE_TYPE, opt => opt.MapFrom(s => s.MobileType))
                        ;

                    cfg.CreateMap<PAM_ACCOUNT_APPLY, CustomerWifi>()
                        .ForMember(s => s.SiteType, opt => opt.MapFrom(s => s.SITE_TYPE))
                        .ForMember(s => s.Zone, opt => opt.MapFrom(s => s.ZONE))
                        .ForMember(s => s.ApplyCount, opt => opt.MapFrom(s => s.APPLY_COUNT))
                        .ForMember(s => s.CompanyName, opt => opt.MapFrom(s => s.COMPANY_NAME))
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        .ForMember(s => s.Customer, opt => opt.MapFrom(s => s.CUSTOMER))
                        ;
                    cfg.CreateMap<CustomerWifi, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.SITE_TYPE, opt => opt.MapFrom(s => s.SiteType))
                        .ForMember(s => s.ZONE, opt => opt.MapFrom(s => s.Zone))
                        .ForMember(s => s.APPLY_COUNT, opt => opt.MapFrom(s => s.ApplyCount))
                        .ForMember(s => s.COMPANY_NAME, opt => opt.MapFrom(s => s.CompanyName))
                        .ForMember(s => s.CUSTOMER, opt => opt.MapFrom(s => s.Customer))
                        ;
                    cfg.CreateMap<PAM_ACCOUNT_APPLY, SSLVpn>()
                        .ForMember(s => s.EmpNo, opt => opt.MapFrom(s => s.EMP_NO))
                        .ForMember(s => s.EmpName, opt => opt.MapFrom(s => s.EMP_NAME))
                        .ForMember(s => s.UsingType, opt => opt.MapFrom(s => s.USING_TYPE))
                        ;
                    cfg.CreateMap<SSLVpn, PAM_ACCOUNT_APPLY>()
                        .ForMember(s => s.EMP_NO, opt => opt.MapFrom(s => s.EmpNo))
                         .ForMember(s => s.EMP_NAME, opt => opt.MapFrom(s => s.EmpName))
                        .ForMember(s => s.USING_TYPE, opt => opt.MapFrom(s => s.UsingType))
                        ;
                });

            _mapper = config.CreateMapper();
        }
        public bool Approve(AccountApplyForm Data)
        {
            return true;
        }

        public string Approved(AccountApplyForm Data, SignFormMain Sign)
        {

            //// 核準新增 Account
            //var result = true;
            //if (nexStage.ROLE_ID == (int)EnumFlowRoles.CaseOfficerMgrCosign)
            //// || nexStage.ROLE_ID == (int)EnumFlowRoles.VIPCaseOfficerCosign
            //{//承辦關卡 產配置子單

            // 共用
            Account Account = new Account
            {
                //共用
                FunctionType = Data.FunctionType,
                AccountType = Data.AccountType,
            };

            #region 其餘

            if (Data.ComputerAccount != null)
            {
                foreach (var Item in Data.ComputerAccount)
                {
                    //共用
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // 電腦帳號欄位
                    Account.EnableAd = Item.EnableAd;
                    Account.EnableNovell = Item.EnableNovell;
                    Account.EnableNotes = Item.EnableNotes;
                    Account.EnableExternalMail = Item.EnableExternalMail;
                    Account.EnableInternet = Item.EnableInternet.ToString();
                    Account.EnablePrint = Item.EnablePrint.ToString();
                    Account.EnableFax = Item.EnableFax.ToString();
                    Account.PasswordType = Item.PasswordType;
                    Account.RequireDescription = Item.RequireDescription;
                    Account.FirstName = Item.FirstName;
                    Account.LastName = Item.LastName;
                    Account.FirstNameTW = Item.FirstNameTW;
                    Account.LastNameTW = Item.LastNameTW;
                    Account.SystemName = Item.SystemName;
                    Account.ManageMethod = Item.ManageMethod;

                    using (var repository = new ComputerAccountRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.ComputerAccount;
                        var autoincrementId = repository.Add(Account);
                    }

                };

            }
            if (Data.PushMail != null)
            {
                foreach (var Item in Data.PushMail)
                {
                    //共用
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // PushMail欄位
                    Account.MobileType = Item.MobileType;
                    Account.MobileId = Item.MobileId;

                    using (var repository = new PushMailRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.PushMail;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.SSLVpn != null)
            {
                foreach (var Item in Data.SSLVpn)
                {
                    //共用
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    using (var repository = new SslVpnRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.SslVpn;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.Citrix != null)
            {
                foreach (var Item in Data.Citrix)
                {
                    //共用
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // Citrix 欄位
                    Account.EnableCitrixNight = Item.EnableCitrixNight;
                    Account.Others = Item.Others;

                    using (var repository = new CitrixRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.Citrix;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.Websense != null)
            {
                foreach (var Item in Data.Websense)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // Websense 欄位
                    Account.SiteName = Item.SiteName;
                    Account.SiteUrl = Item.SiteUrl;
                    Account.EnableView = Item.EnableView;
                    Account.EnableDowload = Item.EnableDowload;
                    Account.EnableCopy = Item.EnableCopy;
                    Account.EnableUpload = Item.EnableUpload;
                    Account.SiteClass = Item.SiteClass;
                    Account.EnableInternet = Item.EnableInternet;

                    using (var repository = new WebsenseRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.Websense;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.LocalDomain != null)
            {
                foreach (var Item in Data.LocalDomain)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // LocalDomain 欄位
                    Account.MainAssetNo = Item.MainAssetNo;
                    Account.SubAssetNo = Item.SubAssetNo;
                    Account.ComputerName = Item.ComputerName;
                    Account.CompanyCode = Item.CompanyCode;

                    using (var repository = new LocalAdminRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.LocalDomain;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.NetworkPrinting != null)
            {
                foreach (var Item in Data.NetworkPrinting)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // NetworkPrinting 欄位
                    Account.PrinterName = Item.PrinterName;
                    Account.PrinterFunction = Item.PrinterFunction;

                    using (var repository = new NetworkPrintingRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.NetworkPrinting;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.MailOut != null)
            {
                foreach (var Item in Data.MailOut)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // MailOut 欄位
                    Account.MailCompany = Item.MailCompany;
                    Account.MailName = Item.MailName;
                    Account.MailAddress = Item.MailAddress;
                    Account.NotesMailAccount = Item.NotesMailAccount;

                    using (var repository = new MailOutRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.MailOut;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.NB != null)
            {
                foreach (var Item in Data.NB)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // NB 欄位
                    Account.MainAssetNo = Item.MainAssetNo;
                    Account.SubAssetNo = Item.SubAssetNo;
                    Account.ComputerName = Item.ComputerName;
                    Account.CompanyCode = Item.CompanyCode;
                    Account.NbIdentity = Item.NbIdentity;

                    using (var repository = new NBRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.NB;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }


            if (Data.ComputerOthers != null)
            {
                foreach (var Item in Data.ComputerOthers)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // ComputerOthers 欄位
                    Account.MainAssetNo = Item.MainAssetNo;
                    Account.SubAssetNo = Item.SubAssetNo;
                    Account.ComputerName = Item.ComputerName;
                    Account.CompanyCode = Item.CompanyCode;
                    Account.Policy = Item.Policy;

                    using (var repository = new ComputerOthersRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.ComputerOthers;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.MobileWifi != null)
            {
                foreach (var Item in Data.MobileWifi)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // MobileWifi 欄位
                    Account.MacAddress = Item.MacAddress;
                    Account.SiteType = Item.SiteType;
                    Account.MobileId = Item.MobileId;
                    Account.MobileType = Item.MobileType;

                    using (var repository = new MobileWifiRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.MobileWifi;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.CustomerWifi != null)
            {
                foreach (var Item in Data.CustomerWifi)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    // CustomerWifi 欄位
                    Account.SiteType = Item.SiteType;
                    Account.Zone = Item.Zone;
                    Account.ApplyCount = Item.ApplyCount;
                    Account.CompanyName = Item.CompanyName;
                    Account.Customer = Item.Customer;

                    using (var repository = new CustomerWifiRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.CustomerWifi;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }
            if (Data.EFax != null)
            {
                foreach (var Item in Data.EFax)
                {
                    // 共用欄位
                    Account.FunctionApplyType = Item.FunctionApplyType;
                    Account.ManageType = Item.ManageType;
                    Account.DeptNo = Item.DeptNo;
                    Account.EmpNo = Item.EmpNo;
                    Account.EmpName = Item.EmpName;
                    Account.UsingType = Item.UsingType;
                    Account.CreateDate = DateTime.Now;
                    Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    Account.RefType = (int)Item.RefType;
                    Account.Status = (byte)EnumAccountStatus.Enable;
                    Account.DisableDate = Item.DisableDate;

                    using (var repository = new EFaxRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.EFax;
                        var autoincrementId = repository.Add(Account);
                    }
                };
            }

            //}
            #endregion

            if (Data.FTP != null)
            {
                // TODO：外層 共用欄位沒使用可能會缺少資料，需從 Portal 那處理
                Account.FunctionApplyType = Data.FTP.FunctionApplyType;
                Account.ManageType = Data.FTP.ManageType;
                Account.DeptNo = Data.FTP.DeptNo;
                Account.CreateDate = DateTime.Now;
                Account.UpdaterEmpNo = Data.FTP.UpdaterEmpNo;
                Account.RefType = (int)Data.FTP.RefType;
                Account.Status = (byte)EnumAccountStatus.Enable;
                Account.DisableDate = Data.FTP.DisableDate;

                foreach (var FTP in Data.FTP.AddAccount)
                {
                    // 小於0則從 Portal 倒入 ACCOUNT，小於 0 則新增 ACCOUNT FTP
                    if (FTP.AccountId < 0)
                    {
                        //共用欄位
                        Account.EmpNo = FTP.UserEmpNo; // 帳號持有者 ENPNO
                        Account.UsingType = FTP.UseType;
                        Account.EmpName = Data.FTP.EmpName;


                        // FTP 欄位
                        Account.CompanyNameEn = Data.FTP.CompanyNameEn; // TODO 廠商 EN
                        Account.ContactName = FTP.User; // TODO 聯絡人
                        Account.ContactMail = FTP.ContactMail; // 聯絡人 MAIL
                        Account.ContactIp = FTP.ContactIp; // 連線 IP
                        Account.SystemName = FTP.SystemName; // 系統名稱
                        // 組完後回塞 FTP 目錄授權 Permission

                        using (var repository = new FTPRepository())
                        {
                            Account.FunctionType = (byte)EnumAccountFunctionType.FTP;
                            var autoincrementId = repository.Add(Account); // ACCOUNT_ID

                            List<Dictionary<string, string>> FTP_PERMISSION = new List<Dictionary<string, string>>();

                            // 對應 Folder Id 組完後回塞 FTP 目錄授權 Permission

                            if (Data.FTP.ListFromKeeper.FirstOrDefault(x => x.AccountId == FTP.AccountId) is null)
                            {
                                Data.FTP.ListFromKeeper.FirstOrDefault(x => x.AccountId == FTP.AccountId).AccountId = Convert.ToInt32(autoincrementId);

                                foreach (var item in Data.FTP.ListFromKeeper)
                                {
                                    Dictionary<string, string> FTP_PERMISSION_Item = new Dictionary<string, string>();
                                    FTP_PERMISSION_Item.Add("FolderId", item.AccountId.ToString());
                                    string Permission="";
                                    switch (item.FunctionType)
                                    {
                                        case 0:
                                            Permission = "Download";
                                            break;
                                        case 1:
                                            Permission = "Upload";
                                            break;
                                        case 2:
                                            Permission = "ControlAll";
                                            break;

                                    }
                                    FTP_PERMISSION_Item.Add("Permission", Permission);
                                    FTP_PERMISSION.Add(FTP_PERMISSION_Item);
                                }

                            }

                            if (Data.FTP.UserCatalog.FirstOrDefault(x => x.AccountId == FTP.AccountId) is null)
                            {
                                Data.FTP.UserCatalog.FirstOrDefault(x => x.AccountId == FTP.AccountId).AccountId = Convert.ToInt32(autoincrementId);

                                foreach (var item in Data.FTP.UserCatalog)
                                {
                                    Dictionary<string, string> FTP_PERMISSION_Item = new Dictionary<string, string>();
                                    FTP_PERMISSION_Item.Add("FolderId", item.AccountId.ToString());
                                    string Permission = "";
                                    switch (item.FunctionType)
                                    {
                                        case 0:
                                            Permission = "Download";
                                            break;
                                        case 1:
                                            Permission = "Upload";
                                            break;
                                        case 2:
                                            Permission = "ControlAll";
                                            break;

                                    }
                                    FTP_PERMISSION_Item.Add("Permission", Permission);
                                    FTP_PERMISSION.Add(FTP_PERMISSION_Item);
                                }
                            }

                             var FtpPermission = JsonConvert.SerializeObject(FTP_PERMISSION);

                            decimal ACCOUNT_ID = decimal.Parse(autoincrementId);
                            Entities.ACCOUNT.FirstOrDefault(x => x.ID == ACCOUNT_ID).FTP_PERMISSION = FtpPermission;
                        }
                    }
                }

                foreach (var FTPFolder in Data.FTP.Catalog)
                {
                    //共用欄位
                    Account.EmpNo = Data.FTP.EmpNo; // 目錄保管人

                    // FTP Folder 欄位
                    Account.FolderName = FTPFolder.FTPFolder;

                    using (var repository = new FTPFolderRepository())
                    {
                        Account.FunctionType = (byte)EnumAccountFunctionType.FTPFolder;
                        var autoincrementId = repository.Add(Account); // ACCOUNT
                    }
                }

            }

            return null;
        }

        public bool Close(AccountApplyForm Data, SignFormMain Sign)
        {
            return true;
        }

        public bool Create(AccountApplyForm Data, decimal dSignID, bool bIsNew)
        {
            var result = true;
            Entities.PAM_ACCOUNT_APPLY.RemoveRange(Entities.PAM_ACCOUNT_APPLY.Where(x => x.PAM_ACCOUNT_APPLYFORM.SIGN_FORM_ID == dSignID));
            Entities.PAM_ACCOUNT_APPLYFORM.RemoveRange(Entities.PAM_ACCOUNT_APPLYFORM.Where(x => x.SIGN_FORM_ID == dSignID));

            var autoincrementId = Entities.PAM_ACCOUNT_APPLYFORM.Select(x => x.APPLY_NO).DefaultIfEmpty(0).Max() + 1;
            var accessForm = new PAM_ACCOUNT_APPLYFORM
            {
                APPLY_NO = autoincrementId,
                SIGN_FORM_ID = dSignID,
                ACCOUNT_TYPE = Data.AccountType,
                APPLY_ITEM = Data.ApplyItem,
                UPDATE_DATE = DateTime.Now,
                FUNCTION_TYPE = Data.FunctionType,
            };
            Entities.PAM_ACCOUNT_APPLYFORM.Add(accessForm);

            if (Data.ComputerAccount != null)
            {
                addComputerAccount(Data.ComputerAccount, autoincrementId);
            }
            if (Data.PushMail != null)
            {
                addPushMail(Data.PushMail, autoincrementId);
            }
            if (Data.SSLVpn != null)
            {
                addSSLVpn(Data.SSLVpn, autoincrementId);
            }
            if (Data.Citrix != null)
            {
                addCitrix(Data.Citrix, autoincrementId);
            }
            if (Data.Websense != null)
            {
                addWebsense(Data.Websense, autoincrementId);
            }
            if (Data.LocalDomain != null)
            {
                addLocalDomain(Data.LocalDomain, autoincrementId);
            }
            if (Data.NetworkPrinting != null)
            {
                addNetworkPrinting(Data.NetworkPrinting, autoincrementId);
            }
            if (Data.MailOut != null)
            {
                addMailOut(Data.MailOut, autoincrementId);
            }
            if (Data.NB != null)
            {
                addNB(Data.NB, autoincrementId);
            }
            if (Data.FTP != null)
            {
                addFTP(Data.FTP, autoincrementId); // 改
            }
            if (Data.ComputerOthers != null)
            {
                addComputerOthers(Data.ComputerOthers, autoincrementId);
            }
            if (Data.MobileWifi != null)
            {
                addMobileWifi(Data.MobileWifi, autoincrementId);
            }
            if (Data.CustomerWifi != null)
            {
                addCustomerWifi(Data.CustomerWifi, autoincrementId);
            }
            if (Data.EFax != null)
            {
                addEFax(Data.EFax, autoincrementId);
            }
            return result;
        }

        public bool Invalid(AccountApplyForm Data)
        {
            return true;
        }

        public bool Rejected(AccountApplyForm Data)
        {
            return true;
        }

        public void SetEntities(Entities entities)
        {
            Entities = entities;
        }

        public void SetUpdaterEmpNo(string empNo)
        {
            this.UpdaterEmpNo = empNo;
        }

        private void addComputerAccount(ComputerAccount[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<ComputerAccount, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.ENABLE_FAX = data.EnableFax ? "Y" : null;
                applyData.ENABLE_INTERNET = data.EnableInternet ? "Y" : null;
                applyData.ENABLE_PRINT = data.EnablePrint ? "Y" : null;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                applyData.ACCOUNT_TYPE = data.AccountType;

                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addPushMail(PushMail[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<PushMail, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addSSLVpn(SSLVpn[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addCitrix(Citrix[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<Citrix, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addWebsense(Websense[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<Websense, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                applyData.ENABLE_UPLOAD = data.EnableUpload == "true" ? "Y" : null;
                applyData.ENABLE_VIEW = data.EnableView == "true" ? "Y" : null;
                applyData.ENABLE_COPY = data.EnableCopy == "true" ? "Y" : null;
                applyData.ENABLE_DOWNLOAD = data.EnableDowload == "true" ? "Y" : null;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addLocalDomain(LocalDomain[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<LocalDomain, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addNetworkPrinting(NetworkPrinting[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<NetworkPrinting, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addMailOut(MailOut[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<MailOut, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addNB(NB[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<NB, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addFTP(FTP data, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();

            var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
            applyData = _mapper.Map<FTP, PAM_ACCOUNT_APPLY>(data, applyData);
            applyData.APPLY_NO = applyNo;
            applyData.ID = ++autoincrementApplyId;
            applyData.EMP_NAME = data.EmpName;
            applyData.EMP_NO = data.EmpNo;
            applyData.USING_TYPE = data.UsingType;
            string json = JsonConvert.SerializeObject(data);
            applyData.FTP_PERMISSION = Encoding.UTF8.GetBytes(json);
            Entities.PAM_ACCOUNT_APPLY.Add(applyData);
            Entities.SaveChanges();

        }
        private void addComputerOthers(ComputerOthers[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<ComputerOthers, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addMobileWifi(MobileWifi[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<MobileWifi, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addCustomerWifi(CustomerWifi[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData = _mapper.Map<CustomerWifi, PAM_ACCOUNT_APPLY>(data, applyData);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }
        private void addEFax(EFax[] datas, decimal applyNo)
        {
            var autoincrementApplyId = Entities.PAM_ACCOUNT_APPLY.Select(x => x.ID).DefaultIfEmpty(0).Max();
            foreach (var data in datas)
            {
                var applyData = _mapper.Map<AccountApplyBase, PAM_ACCOUNT_APPLY>(data);
                applyData.APPLY_NO = applyNo;
                applyData.ID = ++autoincrementApplyId;
                applyData.EMP_NAME = data.EmpName;
                applyData.EMP_NO = data.EmpNo;
                applyData.USING_TYPE = data.UsingType;
                Entities.PAM_ACCOUNT_APPLY.Add(applyData);
                Entities.SaveChanges();
            }
        }

        public PageQueryResult<ServiceQuery> GetApplyFormList(int pIntType)
        {
            var result = new PageQueryResult<ServiceQuery>();
            List<ServiceQuery> DataList = new List<ServiceQuery>();
            var data = Entities.SIGN_FORM_MAIN.Where(x => x.FORM_TYPE == "PAMCitrixPermission" || x.FORM_TYPE == "AccountApplyForm").OrderByDescending(x1 => x1.SIGN_FORM_NO).ToList();
            var date = new DateTime(DateTime.Now.Year, 1, 1);
            if (pIntType == 0) { data = data.Where(x => x.CREATE_DATE > date).ToList(); }
            if (pIntType == 1) { data = data.Where(x => x.CREATE_DATE < date).ToList(); }
            foreach (var ele in data)
            {
                ServiceQuery Data = new ServiceQuery();
                Data.SignFormId = (int)ele.SIGN_FORM_ID;
                Data.ApplicanterDept = ele.APPLICANTER_DEPT_NO;
                Data.ApplicanterEmpNo = ele.APPLICANTER_EMP_NO;
                Data.ApplicanterName = ele.APPLICANTER_NAME;
                Data.CreateDate = (DateTime)ele.CREATE_DATE;
                Data.FillerEmpNo = ele.FILLER_EMP_NO;
                Data.ServiceProject = ele.PAM_SYSTEM_SERVICES.SERVICE_PROJECT;
                Data.FormNo = ele.SIGN_FORM_NO;
                Data.FormType = ele.FORM_TYPE;
                Data.FormStatus = ele.FORM_STATUS;
                Data.ServiceClass = ele.PAM_SYSTEM_SERVICES.SERVICE_PROJECT;
                Data.ServiceSubClass = ele.PAM_SYSTEM_SERVICES.SERVICE_SUB_PROJECT;
                DataList.Add(Data);

            }

            result.Entries = DataList;

            return result;
        }



        public List<ApplyItem> GetApplyItems(ApplyItemRequest request)
        {
            var apiUri = base.ITCPORTALAPIUri + "GetApplyItems";
            var response = new List<ApplyItem>();
            try
            {
                var jsonResult = RestSharpHelper.PostJson(apiUri, null, JsonConvert.SerializeObject(request));
                response = JsonConvert.DeserializeObject<List<ApplyItem>>(jsonResult);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                throw ex;
            }
            return response;
        }
        public SignData<AccountApplyForm> GetSignByID(int pIntID)
        {
            Entities = new Entities(ConnectionString.DefaultConnectionName);
            var objSign = new SignData<AccountApplyForm>();

            var objSFM = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == pIntID);
            objSign.Sign = _mapper.Map<SIGN_FORM_MAIN, SignFormMain>(objSFM);

            var objForm = objSFM.PAM_ACCOUNT_APPLYFORM.FirstOrDefault();
            var objApplys = objForm.PAM_ACCOUNT_APPLY.ToList();

            var applyForm = new AccountApplyForm
            {
                ApplyNo = (int)objForm.APPLY_NO,
                AccountType = (int)objForm.ACCOUNT_TYPE,
                ApplyItem = objForm.APPLY_ITEM,
                SignFormId = (int)objForm.SIGN_FORM_ID,
                FunctionType = (int)objForm.FUNCTION_TYPE
            };

            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.ComputerAccount)
                applyForm.ComputerAccount = _mapper.Map<List<ComputerAccount>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.PushMail)
                applyForm.PushMail = _mapper.Map<List<PushMail>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.SslVpn)
                applyForm.SSLVpn = _mapper.Map<List<SSLVpn>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.Citrix)
                applyForm.Citrix = _mapper.Map<List<Citrix>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.Websense)
                applyForm.Websense = _mapper.Map<List<Websense>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.LocalDomain)
                applyForm.LocalDomain = _mapper.Map<List<LocalDomain>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.NetworkPrinting)
                applyForm.NetworkPrinting = _mapper.Map<List<NetworkPrinting>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.MailOut)
                applyForm.MailOut = _mapper.Map<List<MailOut>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.NB)
                applyForm.NB = _mapper.Map<List<NB>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.FTP)
            {
                applyForm.FTP = JsonConvert.DeserializeObject<FTP>(System.Text.Encoding.UTF8.GetString(objApplys[0].FTP_PERMISSION));
                //applyForm.FTP = _mapper.Map<FTP>(objApplys);
            }

            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.ComputerOthers)
                applyForm.ComputerOthers = _mapper.Map<List<ComputerOthers>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.MobileWifi)
                applyForm.MobileWifi = _mapper.Map<List<MobileWifi>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.CustomerWifi)
                applyForm.CustomerWifi = _mapper.Map<List<CustomerWifi>>(objApplys).ToArray();
            if (applyForm.FunctionType == (byte)EnumAccountFunctionType.EFax)
                applyForm.EFax = _mapper.Map<List<EFax>>(objApplys).ToArray();

            objSign.FormData = applyForm;

            return objSign;
        }
    }
}
