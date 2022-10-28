```c#
using Autofac;
using Mxic.Framework.ServerComponent;
using Mxic.ITC.SAM.Enum;
using Mxic.ITC.SAM.Interface;
using Mxic.ITC.SAM.Model;
using Mxic.ITC.SAM.Model.Business;
using Mxic.ITC.SAM.Model.Entity;
using Mxic.ITC.SAM.Model.Extensions;
using Mxic.ITC.SAM.Repository.Repository;
using Mxic.ITC.SAM.Utility;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Mxic.ITC.SAM.Service
{
    [Authorization]
    public class DataMigrationService : BaseService
    {
        private string configPath;
        private string basePath;
        private List<string> authorizationType = new List<string>();
        private IHrMasterService hrMasterService { get; set; }

        public DataMigrationService()
        {
            configPath = WebConfigurationManager.AppSettings["VirtualUserManualFilePath"];
            basePath = System.Web.HttpContext.Current.Server.MapPath("~/" + configPath);
            hrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
        }

        /// <summary>
        /// 匯入格式1
        /// </summary>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        public PageQueryResult<string> ImportExcel1()
        {
            var response = new PageQueryResult<string>();
            string fileName = string.Empty;

            try
            {
                fileName = UploadFileHelper.UloadFileRetrunFileName(configPath).First();
                IWorkbook workBook;
                using (var fs = new FileStream(basePath + fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fileName.IndexOf(".xlsx") != -1)
                        workBook = new XSSFWorkbook(fs);
                    else
                        workBook = new HSSFWorkbook(fs);
                }

                var authItems = mappingExcel1ToSoftwareAuthorization(workBook.GetSheetAt(0));
                authItems = mappingExcel1ToSoftwareLicense(workBook.GetSheetAt(1), authItems);
                authItems = mappingExcel1ToSoftwareUsage(workBook.GetSheetAt(2), authItems);
                authItems = mappingExcel1ToMahistory(workBook.GetSheetAt(3), authItems);

                var request = new DataMigrationRequest
                {
                    UploadType = HttpContext.Current.Request["UploadType"],
                    SoftwareAuthorizations = authItems
                };

                request.SoftwareAuthorizations.ForEach(u => { u.UpdaterEmpNo = UserInfo.Account; u.ExcelPath = configPath + fileName; });

                var repository = new DataMigrationRepository();
                repository.ErrorMessage.AddRange(base.ErrorMessage);
                repository.ImportExcel1(request);

                response.StatusCode = (long)EnumStatusCode.Success;
            }
            catch (Exception ex)
            {
                File.Delete(basePath + fileName);
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message.RemoveLangTag();
            }
            return response;
        }

        /// <summary>
        /// 匯入格式2
        /// </summary>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        public PageQueryResult<string> ImportExcel2()
        {
            var response = new PageQueryResult<string>();
            string fileName = string.Empty;

            try
            {
                fileName = UploadFileHelper.UloadFileRetrunFileName(configPath).First();

                IWorkbook workBook;
                using (var fs = new FileStream(basePath + fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fileName.IndexOf(".xlsx") != -1)
                        workBook = new XSSFWorkbook(fs);
                    else
                        workBook = new HSSFWorkbook(fs);
                }


                var _softwareAvailables = new List<SoftwareAvailable>();
                _softwareAvailables.AddRange(mappingExcel2ToSoftwareAvailable(workBook.GetSheetAt(0), EnumPaymentType.Free));
                _softwareAvailables.AddRange(mappingExcel2ToSoftwareAvailable(workBook.GetSheetAt(2), EnumPaymentType.Pay));

                var request = new DataMigrationRequest
                {
                    UploadType = HttpContext.Current.Request["UploadType"],
                    SoftwareAvailables = _softwareAvailables,
                    SoftwareUsages = mappingExcel2ToSoftwareUsage(workBook.GetSheetAt(1))
                };

                var repository = new DataMigrationRepository();
                repository.ErrorMessage.AddRange(base.ErrorMessage);
                repository.SetUpdateEmpNo(UserInfo.Account);
                repository.ImportExcel2(request);

                response.StatusCode = (long)EnumStatusCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;
            }
            finally
            {
                File.Delete(basePath + fileName);
            }
            return response;
        }


        /// <summary>
        /// EXCEL1 - 1.軟體授權清單
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<SoftwareAuthorization> mappingExcel1ToSoftwareAuthorization(ISheet sheet)
        {
            var result = new List<SoftwareAuthorization>();

            string sheetName = "[軟體授權清單]";

            string _Code, _Name, _Version, _Lease, _AuthorizationTypeName,
                _PurchaseCount, _WarrantyStartDate, _WarrantyEndDate, _AttributionUnit,
                _SoftTypeName, _ApproveEmpNo, _PurchaseId, _PurchaseYear, _KeyMappingType,
                _Enable, _UsingCount, _StockCount, _CertificateFile, _Price, _SourceUrl, _DealerName,
                _CorpName, _Remark;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _Code = row.GetCell(0)?.ToString();
                        _Name = row.GetCell(1)?.ToString();
                        _Version = row.GetCell(2)?.ToString();
                        _Lease = row.GetCell(3)?.ToString();
                        _AuthorizationTypeName = row.GetCell(4)?.ToString();
                        _PurchaseCount = row.GetCell(5)?.ToString();
                        _WarrantyStartDate = row.GetCell(6)?.ToString();
                        _WarrantyEndDate = row.GetCell(7)?.ToString();
                        _AttributionUnit = row.GetCell(8)?.ToString();
                        _SoftTypeName = row.GetCell(9)?.ToString();
                        _ApproveEmpNo = row.GetCell(10)?.ToString();
                        _PurchaseId = row.GetCell(11)?.ToString();
                        _PurchaseYear = row.GetCell(12)?.ToString();
                        _KeyMappingType = row.GetCell(13)?.ToString();
                        _Enable = row.GetCell(14)?.ToString();
                        _UsingCount = row.GetCell(15)?.ToString();
                        _StockCount = row.GetCell(16)?.ToString();
                        _CertificateFile = row.GetCell(17)?.ToString();
                        _Price = row.GetCell(18)?.ToString();
                        _SourceUrl = row.GetCell(19)?.ToString();
                        _DealerName = row.GetCell(20)?.ToString();
                        _CorpName = row.GetCell(21)?.ToString();
                        _Remark = row.GetCell(22)?.ToString();

                        if (string.IsNullOrEmpty(_Code)
                            && string.IsNullOrEmpty(_Name)
                            && string.IsNullOrEmpty(_Version)
                            && string.IsNullOrEmpty(_Lease)
                            && string.IsNullOrEmpty(_AuthorizationTypeName)
                            && string.IsNullOrEmpty(_PurchaseCount)
                            && string.IsNullOrEmpty(_WarrantyStartDate)
                            && string.IsNullOrEmpty(_WarrantyEndDate)
                            && string.IsNullOrEmpty(_AttributionUnit)
                            && string.IsNullOrEmpty(_SoftTypeName)
                            && string.IsNullOrEmpty(_ApproveEmpNo)
                            && string.IsNullOrEmpty(_PurchaseId)
                            && string.IsNullOrEmpty(_PurchaseYear)
                            && string.IsNullOrEmpty(_KeyMappingType)
                            && string.IsNullOrEmpty(_Enable)
                            && string.IsNullOrEmpty(_UsingCount)
                            && string.IsNullOrEmpty(_StockCount)
                            && string.IsNullOrEmpty(_CertificateFile)
                            && string.IsNullOrEmpty(_Price)
                            && string.IsNullOrEmpty(_SourceUrl)
                            && string.IsNullOrEmpty(_DealerName)
                            && string.IsNullOrEmpty(_CorpName)
                            && string.IsNullOrEmpty(_Remark))
                            continue;


                        if (string.IsNullOrEmpty(_Code))
                            throwErrorMessage(sheetName, rowIndex, "軟體編號(必填)");

                        if (_Lease != "是" && _Lease != "否")
                            throwErrorMessage(sheetName, rowIndex, "是否為租賃與設定值不同");
                        if (_Enable != "是" && _Enable != "否")
                            throwErrorMessage(sheetName, rowIndex, "是否開放申請與設定值不同");
                        if (_KeyMappingType != "一對一" && _KeyMappingType != "一對多" && _KeyMappingType != "無授權碼/授權檔")
                            throwErrorMessage(sheetName, rowIndex, "Key給予方式與設定值不同");

                        string _keyMappingType = string.Empty;
                        if (_KeyMappingType == "一對一")
                            _keyMappingType = EnumKeyMappingType.OneToOne.ToString();
                        else if (_KeyMappingType == "一對多")
                            _keyMappingType = EnumKeyMappingType.OneToMany.ToString();
                        else if (_KeyMappingType == "無授權碼/授權檔")
                            _keyMappingType = EnumKeyMappingType.Not.ToString();
                        else
                            _keyMappingType = string.Empty;

                        if (!string.IsNullOrEmpty(_CertificateFile))
                        {
                            if (!File.Exists(basePath + _CertificateFile))
                                throwErrorMessage(sheetName, rowIndex, "附件檔案不存在Server特定目錄下");
                        }

                        if (string.IsNullOrEmpty(_Price))
                            _Price = "0";

                        result.Add(new SoftwareAuthorization
                        {
                            Code = _Code,
                            Name = _Name,
                            Version = _Version,
                            Lease = _Lease == "否"
                                ? (byte)EnumLease.NO
                                : (byte)EnumLease.YES,
                            AuthorizationTypeName = _AuthorizationTypeName,
                            PurchaseCount = Convert.ToInt32(_PurchaseCount),
                            WarrantyStartDate = _WarrantyStartDate,
                            WarrantyEndDate = _WarrantyEndDate,
                            AttributionUnit = _AttributionUnit,
                            SoftTypeName = _SoftTypeName,
                            ApproveEmpNo = _ApproveEmpNo,
                            PurchaseId = _PurchaseId,
                            PurchaseYear = Convert.ToInt16(_PurchaseYear),
                            KeyMappingType = _keyMappingType,
                            Enable = _Enable == "否"
                                ? (byte)EnumEnable.NO
                                : (byte)EnumEnable.YES,
                            UsingCount = string.IsNullOrEmpty(_UsingCount) ? (int?)null : Convert.ToInt32(_UsingCount),
                            CertificateFile = string.IsNullOrEmpty(_CertificateFile)
                                ? string.Empty
                                : configPath + _CertificateFile,
                            Price = Convert.ToDecimal(_Price),
                            SourceUrl = _SourceUrl,
                            DealerName = _DealerName,
                            CorpName = _CorpName,
                            Remark = _Remark,
                            UpdaterEmpNo = UserInfo.Account,
                            ExcelRow = rowIndex,
                            ExcelSheetName = sheetName,
                            IsModify = true
                        });
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// EXCEL1 - 2.授權碼或檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="authItems"></param>
        /// <returns></returns>
        private List<SoftwareAuthorization> mappingExcel1ToSoftwareLicense(ISheet sheet, List<SoftwareAuthorization> authItems)
        {
            string sheetName = "[授權碼或檔案]";

            string _Id, _Code, _Key, _File;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _Id = row.GetCell(0)?.ToString();
                        _Code = row.GetCell(1)?.ToString();
                        _Key = row.GetCell(2)?.ToString();
                        _File = row.GetCell(3)?.ToString();

                        if (string.IsNullOrEmpty(_Id)
                            && string.IsNullOrEmpty(_Code)
                            && string.IsNullOrEmpty(_Key)
                            && string.IsNullOrEmpty(_File))
                            continue;

                        if (string.IsNullOrEmpty(_Id))
                            throwErrorMessage(sheetName, rowIndex, "軟體編號(必填)");
                        if (string.IsNullOrEmpty(_Code))
                            throwErrorMessage(sheetName, rowIndex, "授權編號(必填)");
                        if (string.IsNullOrEmpty(_Key) && string.IsNullOrEmpty(_File))
                            throwErrorMessage(sheetName, rowIndex, "授權碼或授權檔擇一(必填)");

                        if (!string.IsNullOrEmpty(_File))
                        {
                            if (!File.Exists(basePath + _File))
                                throwErrorMessage(sheetName, rowIndex, "附件檔案不存在Server特定目錄下");
                        }

                        var authItem = authItems.Where(x => x.Code == _Id).FirstOrDefault();
                        if (authItem == null)
                            throwErrorMessage(sheetName, rowIndex, "查無授權主檔編號");

                        string _name = string.Empty;
                        byte _type = 0;
                        if (!string.IsNullOrEmpty(_Key))
                        {
                            _name = _Key;
                            _type = (byte)EnumLicenseType.Key;
                        }

                        if (!string.IsNullOrEmpty(_File))
                        {
                            _name = configPath + _File;
                            _type = (byte)EnumLicenseType.File;
                        }

                        var licenseCount = authItem.SoftwareLicenses.Count() + 1;
                        authItem.SoftwareLicenses.Add(new SoftwareLicense
                        {
                            Code = licenseCount.ToString().PadLeft(6, '0'),
                            Name = _name,
                            Type = _type,
                            UpdaterEmpNo = UserInfo.Account,
                            ExcelCode = _Code,
                            ExcelRow = rowIndex,
                            ExcelSheetName = sheetName
                        });
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }
                }
            }
            return authItems;
        }

        /// <summary>
        /// EXCEL1 - 3.軟體使用狀況
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="authItems"></param>
        /// <returns></returns>
        private List<SoftwareAuthorization> mappingExcel1ToSoftwareUsage(ISheet sheet, List<SoftwareAuthorization> authItems)
        {
            string sheetName = "[軟體使用狀況]";

            string _Code, _Type, _AssetsCode, _AssetsSubCode, _PcName,
                _SoftwareLicenseCode, _AuthEmpNo, _ActualVersion, _TrialStartDate,
                _TrialEndDate;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _Code = row.GetCell(0)?.ToString();
                        _Type = row.GetCell(1)?.ToString();
                        _AssetsCode = row.GetCell(2)?.ToString();
                        _AssetsSubCode = row.GetCell(3)?.ToString();
                        _PcName = row.GetCell(4)?.ToString();
                        _SoftwareLicenseCode = row.GetCell(5)?.ToString();
                        _AuthEmpNo = row.GetCell(6)?.ToString();
                        _ActualVersion = row.GetCell(7)?.ToString();
                        _TrialStartDate = row.GetCell(8)?.ToString();
                        _TrialEndDate = row.GetCell(9)?.ToString();

                        if (string.IsNullOrEmpty(_Code)
                            && string.IsNullOrEmpty(_Type)
                            && string.IsNullOrEmpty(_AssetsCode)
                            && string.IsNullOrEmpty(_AssetsSubCode)
                            && string.IsNullOrEmpty(_PcName)
                            && string.IsNullOrEmpty(_SoftwareLicenseCode)
                            && string.IsNullOrEmpty(_AuthEmpNo)
                            && string.IsNullOrEmpty(_ActualVersion)
                            && string.IsNullOrEmpty(_TrialStartDate)
                            && string.IsNullOrEmpty(_TrialEndDate))
                            continue;

                        if (string.IsNullOrEmpty(_Code))
                            throwErrorMessage(sheetName, rowIndex, "軟體編號(必填)");

                        if (_Type != "實體機" && _Type != "虛擬機")
                            throwErrorMessage(sheetName, rowIndex, "安裝機器類型與設定值不同");

                        var authItem = authItems.Where(x => x.Code == _Code).FirstOrDefault();
                        if (authItem == null)
                            throwErrorMessage(sheetName, rowIndex, "查無授權主檔編號");

                        string _SoftwareLicenseNewCode = string.Empty;
                        if (authItem.KeyMappingType == EnumKeyMappingType.OneToOne.ToString()
                            || authItem.KeyMappingType == EnumKeyMappingType.OneToMany.ToString())
                        {
                            if (!string.IsNullOrEmpty(_SoftwareLicenseCode))
                            {
                                var softwareLicense = authItems.SelectMany(x => x.SoftwareLicenses).Where(x => x.ExcelCode == _SoftwareLicenseCode).FirstOrDefault();
                                if (softwareLicense == null)
                                    throwErrorMessage(sheetName, rowIndex, "查無授權碼或檔案");

                                _SoftwareLicenseNewCode = softwareLicense.Code;
                            }
                        }

                        if (!string.IsNullOrEmpty(_AuthEmpNo))
                        {
                            var employee = hrMasterService.GetEmployeeIncludeQuit(_AuthEmpNo);
                            if (employee == null)
                                throwErrorMessage(sheetName, rowIndex, $"{_AuthEmpNo}不存在，請正確填寫員工工號。");
                            if (employee.quitFlag == "Y")
                                throwErrorMessage(sheetName, rowIndex, $"{_AuthEmpNo}已離職，請正確填寫員工工號。");
                        }

                        authItem.SoftwareUsages.Add(new SoftwareUsage
                        {
                            CompanyCode = "1100",
                            Type = _Type == "實體機"
                                ? (byte)EnumUsageType.Entity
                                : (byte)EnumUsageType.Virtual,
                            AssetsCode = _AssetsCode,
                            AssetsSubCode = _AssetsSubCode,
                            PcName = _PcName,
                            SoftwareLicenseCode = _SoftwareLicenseNewCode,
                            AuthEmpNo = _AuthEmpNo,
                            ActualVersion = _ActualVersion,
                            TrialStartDate = string.IsNullOrEmpty(_TrialStartDate)
                                ? (DateTime?)null
                                : DateTime.Parse(_TrialStartDate),
                            TrialEndDate = string.IsNullOrEmpty(_TrialEndDate)
                                ? (DateTime?)null
                                : DateTime.Parse(_TrialEndDate),
                            PaymentType = (byte)EnumPaymentType.Pay,
                            UpdaterEmpNo = UserInfo.Account,
                            ExcelRow = rowIndex,
                            ExcelSheetName = sheetName
                        });
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }
                }
            }
            return authItems;
        }

        /// <summary>
        /// EXCEL1 - 4.MA歷程
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="authItems"></param>
        /// <returns></returns>
        private List<SoftwareAuthorization> mappingExcel1ToMahistory(ISheet sheet, List<SoftwareAuthorization> authItems)
        {
            string sheetName = "[MA歷程]";

            string _Code, _Name, _ExtensionCount, _WarrantyStartDate, _WarrantyEndDate,
                _PurchaseYear, _PurchaseId, _MaCertification, _Description;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _Code = row.GetCell(0)?.ToString();
                        _Name = row.GetCell(1)?.ToString();
                        _ExtensionCount = row.GetCell(2)?.ToString();
                        _WarrantyStartDate = row.GetCell(3)?.ToString();
                        _WarrantyEndDate = row.GetCell(4)?.ToString();
                        _PurchaseYear = row.GetCell(5)?.ToString();
                        _PurchaseId = row.GetCell(6)?.ToString();
                        _MaCertification = row.GetCell(7)?.ToString();
                        _Description = row.GetCell(8)?.ToString();

                        if (string.IsNullOrEmpty(_Code)
                            && string.IsNullOrEmpty(_Name)
                            && string.IsNullOrEmpty(_ExtensionCount)
                            && string.IsNullOrEmpty(_WarrantyStartDate)
                            && string.IsNullOrEmpty(_WarrantyEndDate)
                            && string.IsNullOrEmpty(_PurchaseYear)
                            && string.IsNullOrEmpty(_PurchaseId)
                            && string.IsNullOrEmpty(_MaCertification)
                            && string.IsNullOrEmpty(_Description))
                            continue;

                        if (string.IsNullOrEmpty(_Code))
                            throwErrorMessage(sheetName, rowIndex, "軟體編號(必填)");

                        var authItem = authItems.Where(x => x.Code == _Code).FirstOrDefault();
                        if (authItem == null)
                            throwErrorMessage(sheetName, rowIndex, "查無授權主檔編號");

                        if (!string.IsNullOrEmpty(_MaCertification))
                        {
                            if (!File.Exists(basePath + _MaCertification))
                                throwErrorMessage(sheetName, rowIndex, "附件檔案不存在Server特定目錄下");
                        }

                        authItem.MaHsitorys.Add(new MaHsitory
                        {
                            Name = _Name,
                            ExtensionCount = Convert.ToInt32(_ExtensionCount),
                            WarrantyStartDate = _WarrantyStartDate,
                            WarrantyEndDate = _WarrantyEndDate,
                            PurchaseYear = _PurchaseYear,
                            PurchaseId = _PurchaseId,
                            MaCertification = string.IsNullOrEmpty(_MaCertification)
                                ? string.Empty
                                : configPath + _MaCertification,
                            Description = _Description,
                            ExcelRow = rowIndex,
                            ExcelSheetName = sheetName
                        });
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }
                }
            }
            return authItems;
        }

        /// <summary>
        /// EXCEL2 - 1.免費軟體清單 3.付費軟體申請清單
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<SoftwareAvailable> mappingExcel2ToSoftwareAvailable(ISheet sheet, EnumPaymentType paymentType)
        {
            var result = new List<SoftwareAvailable>();

            string sheetName = string.Empty;

            string _Id, _Name, _PaymentType, _AttributionUnit, _SoftwareTypeName,
                _Enable, _Price, _IsTrial, _CorpName, _Remark, _Remark2;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _Id = row.GetCell(0)?.ToString();
                        _Name = row.GetCell(1)?.ToString();
                        _PaymentType = row.GetCell(2)?.ToString();
                        _AttributionUnit = row.GetCell(3)?.ToString();
                        _SoftwareTypeName = row.GetCell(4)?.ToString();
                        _Enable = row.GetCell(5)?.ToString();

                        if (string.IsNullOrEmpty(_Id)
                            && string.IsNullOrEmpty(_Name)
                            && string.IsNullOrEmpty(_PaymentType)
                            && string.IsNullOrEmpty(_AttributionUnit)
                            && string.IsNullOrEmpty(_SoftwareTypeName)
                            && string.IsNullOrEmpty(_Enable))
                            continue;

                        sheetName = paymentType == EnumPaymentType.Free ? "[免費軟體清單]" : "[付費軟體申請清單]";

                        if (_PaymentType != "免費" && _PaymentType != "付費")
                            throwErrorMessage(sheetName, rowIndex, "免費/付費與設定值不同");
                        if (_Enable != "是" && _Enable != "否")
                            throwErrorMessage(sheetName, rowIndex, "是否開放申請與設定值不同");

                        if (paymentType == EnumPaymentType.Free)
                        {
                            _IsTrial = row.GetCell(6)?.ToString();
                            _CorpName = row.GetCell(7)?.ToString();
                            _Remark = row.GetCell(8)?.ToString();
                            _Remark2 = row.GetCell(9)?.ToString();

                            if (string.IsNullOrEmpty(_Id))
                                throwErrorMessage(sheetName, rowIndex, "免費軟體編號(必填)");
                            if (string.IsNullOrEmpty(_Name))
                                throwErrorMessage(sheetName, rowIndex, "軟體名稱(必填)");
                            if (string.IsNullOrEmpty(_PaymentType))
                                throwErrorMessage(sheetName, rowIndex, "免費/付費(必填)");
                            if (string.IsNullOrEmpty(_AttributionUnit))
                                throwErrorMessage(sheetName, rowIndex, "權責單位(必填)");
                            if (string.IsNullOrEmpty(_SoftwareTypeName))
                                throwErrorMessage(sheetName, rowIndex, "軟體分類(必填)");
                            if (string.IsNullOrEmpty(_Enable))
                                throwErrorMessage(sheetName, rowIndex, "是否開放申請?(必填)");
                            if (string.IsNullOrEmpty(_IsTrial))
                                throwErrorMessage(sheetName, rowIndex, "試用軟體?(必填)");
                            if (_IsTrial != "是" && _IsTrial != "否")
                                throwErrorMessage(sheetName, rowIndex, "試用軟體與設定值不同");

                            result.Add(new SoftwareAvailable
                            {
                                Id = _Id,
                                Code = _Id,
                                Name = _Name,
                                PaymentType = _PaymentType == "免費"
                                    ? (byte)EnumPaymentType.Free
                                    : (byte)EnumPaymentType.Pay,
                                AttributionUnit = _AttributionUnit,
                                SoftwareTypeName = _SoftwareTypeName,
                                Enable = _Enable == "否"
                                    ? (byte)EnumEnable.NO
                                    : (byte)EnumEnable.YES,
                                IsTrial = _IsTrial == "否"
                                    ? (byte)EnumTrial.NO
                                    : (byte)EnumTrial.YES,
                                CorpName = _CorpName,
                                Remark = _Remark,
                                Remark2 = _Remark2,
                                UpdaterEmpNo = UserInfo.Account,
                                ExcelRow = rowIndex,
                                ExcelSheetName = sheetName
                            });
                        }
                        else
                        {
                            _Price = row.GetCell(6)?.ToString();
                            _CorpName = row.GetCell(7)?.ToString();
                            _Remark = row.GetCell(8)?.ToString();
                            _Remark2 = row.GetCell(9)?.ToString();

                            if (string.IsNullOrEmpty(_Id))
                                throwErrorMessage(sheetName, rowIndex, "付費軟體申請編號(必填)");
                            if (string.IsNullOrEmpty(_Name))
                                throwErrorMessage(sheetName, rowIndex, "軟體名稱(必填)");
                            if (string.IsNullOrEmpty(_PaymentType))
                                throwErrorMessage(sheetName, rowIndex, "免費/付費(必填)");
                            if (string.IsNullOrEmpty(_AttributionUnit))
                                throwErrorMessage(sheetName, rowIndex, "權責單位(必填)");
                            if (string.IsNullOrEmpty(_SoftwareTypeName))
                                throwErrorMessage(sheetName, rowIndex, "軟體分類(必填)");
                            if (string.IsNullOrEmpty(_Enable))
                                throwErrorMessage(sheetName, rowIndex, "是否開放申請?(必填)");
                            if (!Regex.IsMatch(_Price ?? "", @"(^$)|(\d)"))
                                throwErrorMessage(sheetName, rowIndex, "單價(僅可輸入數字)");

                            result.Add(new SoftwareAvailable
                            {
                                Id = _Id,
                                Code = _Id,
                                Name = _Name,
                                PaymentType = _PaymentType == "免費"
                                    ? (byte)EnumPaymentType.Free
                                    : (byte)EnumPaymentType.Pay,
                                AttributionUnit = _AttributionUnit,
                                SoftwareTypeName = _SoftwareTypeName,
                                Enable = _Enable == "否"
                                    ? (byte)EnumEnable.NO
                                    : (byte)EnumEnable.YES,
                                Price = _Price,
                                IsTrial = (byte)EnumTrial.NO,
                                CorpName = _CorpName,
                                Remark = _Remark,
                                Remark2 = _Remark2,
                                UpdaterEmpNo = UserInfo.Account,
                                ExcelRow = rowIndex,
                                ExcelSheetName = sheetName
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// EXCEL2 - 2.免費軟體使用狀況
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<SoftwareUsage> mappingExcel2ToSoftwareUsage(ISheet sheet)
        {
            var result = new List<SoftwareUsage>();

            string sheetName = "[免費軟體使用狀況]";

            string _SoftwareAvailableId, _Type, _AssetsCode, _AssetsSubCode, _PcName,
                 _ActualVersion, _TrialStartDate, _TrialEndDate;

            IRow row = sheet.GetRow(0);
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    try
                    {
                        _SoftwareAvailableId = row.GetCell(0)?.ToString();
                        _Type = row.GetCell(1)?.ToString();
                        _AssetsCode = row.GetCell(2)?.ToString();
                        _AssetsSubCode = row.GetCell(3)?.ToString();
                        _PcName = row.GetCell(4)?.ToString();
                        _ActualVersion = row.GetCell(5)?.ToString();
                        _TrialStartDate = row.GetCell(6)?.ToString();
                        _TrialEndDate = row.GetCell(7)?.ToString();

                        if (string.IsNullOrEmpty(_SoftwareAvailableId)
                            && string.IsNullOrEmpty(_Type)
                            && string.IsNullOrEmpty(_AssetsCode)
                            && string.IsNullOrEmpty(_AssetsSubCode)
                            && string.IsNullOrEmpty(_PcName)
                            && string.IsNullOrEmpty(_ActualVersion)
                            && string.IsNullOrEmpty(_TrialStartDate)
                            && string.IsNullOrEmpty(_TrialEndDate))
                            continue;

                        if (string.IsNullOrEmpty(_SoftwareAvailableId))
                            throwErrorMessage(sheetName, rowIndex, "免費軟體編號(必填)");
                        if (string.IsNullOrEmpty(_Type))
                            throwErrorMessage(sheetName, rowIndex, "安裝機器類型(必填)");
                        if (_Type != "實體機" && _Type != "虛擬機")
                            throwErrorMessage(sheetName, rowIndex, "安裝機器類型與設定值不同");
                        if (_Type == "實體機" && string.IsNullOrEmpty(_AssetsCode))
                            throwErrorMessage(sheetName, rowIndex, "主資產編號(必填)");
                        if (!Regex.IsMatch(_AssetsCode ?? "", @"(^$)|^(\d){12}$"))
                            throwErrorMessage(sheetName, rowIndex, "主資產編號格式錯誤(12碼數字)");
                        if (_Type == "實體機" && string.IsNullOrEmpty(_AssetsSubCode))
                            throwErrorMessage(sheetName, rowIndex, "子資產編號(必填)");
                        if (!Regex.IsMatch(_AssetsSubCode ?? "", @"(^$)|^(\d){4}$"))
                            throwErrorMessage(sheetName, rowIndex, "子資產編號格式錯誤(4碼數字)");
                        if (_Type == "虛擬機" && string.IsNullOrEmpty(_PcName))
                            throwErrorMessage(sheetName, rowIndex, "電腦名稱(必填)");
                        if (!Regex.IsMatch(_TrialStartDate ?? "", @"(^$)|^\d\d\d\d\/(0?[1-9]|1[0-2])\/(0?[1-9]|[12][0-9]|3[01])$"))
                            throwErrorMessage(sheetName, rowIndex, "試用起日格式錯誤(YYYY/MM/DD)");
                        if (!Regex.IsMatch(_TrialEndDate ?? "", @"(^$)|^\d\d\d\d\/(0?[1-9]|1[0-2])\/(0?[1-9]|[12][0-9]|3[01])$"))
                            throwErrorMessage(sheetName, rowIndex, "試用迄日格式錯誤(YYYY/MM/DD)");

                        result.Add(new SoftwareUsage
                        {
                            SoftwareAvailableId = _SoftwareAvailableId,
                            Type = _Type == "實體機"
                                ? (byte)EnumUsageType.Entity
                                : (byte)EnumUsageType.Virtual,
                            AssetsCode = _AssetsCode ?? "",
                            AssetsSubCode = _AssetsSubCode ?? "",
                            PcName = _PcName ?? "",
                            ActualVersion = _ActualVersion,
                            TrialStartDate = string.IsNullOrEmpty(_TrialStartDate)
                                ? (DateTime?)null
                                : DateTime.Parse(_TrialStartDate),
                            TrialEndDate = string.IsNullOrEmpty(_TrialEndDate)
                                ? (DateTime?)null
                                : DateTime.Parse(_TrialEndDate),
                            UpdaterEmpNo = UserInfo.Account,
                            ExcelRow = rowIndex,
                            ExcelSheetName = sheetName,
                            PaymentType = (byte)EnumPaymentType.Free
                        });
                    }
                    catch (Exception ex)
                    {
                        throwErrorMessage(sheetName, rowIndex, ex.Message);
                    }
                }
            }
            return result;
        }

        private void throwErrorMessage(string sheetName, int excelRowIndex, string error)
        {
            if (base.ErrorMessage.Count >= base.ShowErrorCount)
            {
                throw new Exception(string.Join("\n", base.ErrorMessage));
            }
            else
            {
                base.ErrorMessage.Add(string.Format("{0} Row->{1} , {2}", sheetName, excelRowIndex + 1, error));
            }
        }
    }
}

```