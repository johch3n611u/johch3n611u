using Mxic.Framework.ServerComponent;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Model.Business;
using Mxic.ITC.PAM.Repository;
using Mxic.ITC.PAM.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Service
{
    [Authorization]
    public class SelfAccountCheckService : BaseService
    {
        [ExposeWebAPI(true)]
        public void CallPAM604() 
        {
            Task.Run(() => {
                new BatchService().PAM604();
            });
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<PAM_SELF_ACCOUNT_CHECK> GetSelfAccountCheck()
        {
            var response = new PageQueryResult<PAM_SELF_ACCOUNT_CHECK>();
            try
            {
                response = new SelfAccountCheckRepository().GetSelfAccountCheck();
                response.StatusCode = (long)EnumStatusCode.Success;
            }
            catch (DbEntityValidationException ex)
            {
                _logger.Error("⭐⭐⭐PAM604 GetSelfAccountCheck⭐⭐⭐");
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                _logger.Error(exceptionMessage);
                _logger.Error(ex.EntityValidationErrors);

                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.StackTrace + ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Error("⭐⭐⭐PAM604 GetSelfAccountCheck⭐⭐⭐");
                _logger.Error(ex.StackTrace);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.StackTrace + ex.Message;
            }

            return response;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> PAM604SendMail(List<string> SelectedId)
        {
            var response = new PageQueryResult<string>();
            try
            {
                response = new SelfAccountCheckRepository().PAM604SendMail(SelectedId);
                response.StatusCode = (long)EnumStatusCode.Success;
            }
            catch (DbEntityValidationException ex)
            {
                _logger.Error("⭐⭐⭐PAM604 SendMail⭐⭐⭐");
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                _logger.Error(exceptionMessage);
                _logger.Error(ex.EntityValidationErrors);

                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.StackTrace + ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Error("⭐⭐⭐PAM604 SendMail⭐⭐⭐");
                _logger.Error(ex.StackTrace);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.StackTrace + ex.Message;
            }

            return response;
        }
    }
}
