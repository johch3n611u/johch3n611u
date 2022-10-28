using AutoMapper;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Login;
using Mxic.ITC.PAM.Repository.Repository.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Repository.Repository
{
    public class XfortRepository : RepositoryBase
    {
        public XfortRepository()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<USERS, Users>().ForMember(s => s.Roles, o => o.MapFrom(s => JsonConvert.DeserializeObject<string[]>(s.ROLENAMES))));
            //_mapper = config.CreateMapper();
        }
        public bool Create(XfortUpload model, string empno)
        {
            try
            {
                if (Entities.XFORT.Select(x => x.ID).Count() > 0)
                {
                    model.Id = (int)Entities.XFORT.Select(x => x.ID).Max() + 1;
                }
                else
                {
                    model.Id = 1;
                }
                model.Update_EmpNo = empno;
                model.LastUpdateDate = DateTime.Now;
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<XfortProfile>();
                });
                var mapper = config.CreateMapper();
                var result = mapper.Map<XFORT>(model);

                Entities.XFORT.Add(result);
                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        public List<XfortUpload> GetXfort()
        {
            try
            {

                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<XfortProfile>();
                });
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<XfortUpload>>(Entities.XFORT).OrderBy(x => x.Id).ToList();



                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        public bool Update(XfortUpload model, string empno)
        {
            try
            {
                model.Update_EmpNo = empno;
                model.LastUpdateDate = DateTime.Now;
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<XfortProfile>();
                });
                var mapper = config.CreateMapper();
                var result = mapper.Map<XFORT>(model);

                var data = Entities.XFORT.FirstOrDefault(x => x.ID == model.Id);
                if (!string.IsNullOrEmpty(model.Name))
                {
                    data.NAME = result.NAME;
                }
                if (!string.IsNullOrEmpty(model.Code))
                {
                    data.CODE = result.CODE;
                }
                if (!string.IsNullOrEmpty(model.Content))
                {
                    data.CONTENT = result.CONTENT;
                }
                if (!string.IsNullOrEmpty(model.Text))
                {
                    data.TEXT = result.TEXT;
                }
                data.LAST_UPDATE_DATE = DateTime.Now;
                data.UPDATE_EMPNO = empno;
                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {

                Entities.XFORT.Remove(Entities.XFORT.FirstOrDefault(x => x.ID == id));
                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
    }
}
