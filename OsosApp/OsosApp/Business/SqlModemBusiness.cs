using OsosApp.Bo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.Business
{
    internal class SqlModemBusiness
    {
        public ResponseBo<List<ModemListBo>> GetList()
        {
            ResponseBo<List<ModemListBo>> responseBo = new ResponseBo<List<ModemListBo>>();

            try
            {
                List<Modem> modemList = null;

                List<ModemListBo> modemListBo = null;
                ModemListBo bo = null;
                using (ososDbEntities db = new ososDbEntities())
                {
                    modemList = db.Modem.Where(x => !x.IsDeleted).ToList();

                    if (modemList.Count() > 0)
                    {
                        modemListBo = new List<ModemListBo>();

                        foreach (Modem item in modemList)
                        {
                            bo = new ModemListBo();
                            bo.Id = item.Id;
                            bo.SerialNo = item.SerialNo;
                            bo.Name = item.Name;
                            bo.Ip = item.Ip;
                            bo.Port = item.Port;
                            bo.IsActive = item.IsActive;

                            bo.DeviceCount = db.Device.Where(x => x.ModemId == item.Id && !x.IsDeleted).Count();

                            modemListBo.Add(bo);
                        }
                    }

                    responseBo.Success(modemListBo);
                }
            }
            catch (Exception ex)
            {
                responseBo.FailedWithException(ex);
            }

            return responseBo;
        }

        public ResponseBo<Modem> Get(int id)
        {
            ResponseBo<Modem> responseBo = new ResponseBo<Modem>();

            try
            {
                Modem modem = null;

                using (ososDbEntities db = new ososDbEntities())
                {
                    modem = db.Modem.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

                    responseBo.Success(modem);
                }
            }
            catch (Exception ex)
            {
                responseBo.FailedWithException(ex);
            }

            return responseBo;
        }

        public ResponseBo Save(Modem modem)
        {
            ResponseBo responseBo = new ResponseBo();

            bool isNew = modem.Id <= 0;

            try
            {
                using (ososDbEntities db = new ososDbEntities())
                {
                    if (isNew)
                    {
                        modem.CreateDateTime = DateTime.UtcNow;

                        db.Modem.Add(modem);
                    }
                    else
                    {
                        modem.UpdateDateTime = DateTime.UtcNow;

                        db.Modem.Attach(modem);
                        db.Entry(modem).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                responseBo.FailedWithException(ex);
            }

            return responseBo;
        }
    }
}