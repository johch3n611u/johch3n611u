using Mxic.ITC.PAM.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Repository.Repository
{
    public class SelfAccountCheckRepository : RepositoryBase
    {
        /// <summary>
        /// 取資料
        /// </summary>
        /// <returns></returns>
        public PageQueryResult<PAM_SELF_ACCOUNT_CHECK> GetSelfAccountCheck()
        {
            var response = new PageQueryResult<PAM_SELF_ACCOUNT_CHECK>();

            response.Entries = Entities.PAM_SELF_ACCOUNT_CHECK.OrderByDescending(x => x.ID).ToList();

            return response;
        }

        /// <summary>
        /// 自檢寄信
        /// </summary>
        /// <param name="SelectedId"></param>
        /// <returns></returns>
        public PageQueryResult<string> PAM604SendMail(List<string> SelectedId)
        {
            var response = new PageQueryResult<string>();

            var ParseId = new List<decimal>();

            foreach (var Id in SelectedId)
            {
                ParseId.Add(decimal.Parse(Id));
            }

            var PamSelfAccountCheck = Entities.PAM_SELF_ACCOUNT_CHECK
                .Where(x => ParseId.Contains(x.ID))
                .ToList();

            var PamSelfAccountCheckId = PamSelfAccountCheck
                .Select(x => x.WAIT_NOTIFICATION_ID)
                .ToList();

            var WaitSendMail = Entities.NOTIFICATION_TASK
                .Where(x => PamSelfAccountCheckId.Contains(x.ID))
                .ToList();

            new BatchRepository().InsertTask(WaitSendMail);

            foreach (var item in PamSelfAccountCheck)
            {
                item.STATE = "已通知";
                // 追蹤不到新信件 Id 因為寫在 InsertTask 底層 ... 
            }

            Entities.SaveChanges();

            return response;
        }
    }
}
