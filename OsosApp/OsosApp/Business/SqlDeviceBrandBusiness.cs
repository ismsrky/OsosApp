using OsosApp.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.Business
{
    internal class SqlDeviceBrandBusiness
    {
        public ResponseBo<List<EnumDeviceBrand>> GetList()
        {
            ResponseBo<List<EnumDeviceBrand>> responseBo = new ResponseBo<List<EnumDeviceBrand>>();

            try
            {
                List<EnumDeviceBrand> list = null;

                using (ososDbEntities db = new ososDbEntities())
                {
                    list = db.EnumDeviceBrand.Where(x => x.Id > 0).ToList();

                    responseBo.Success(list);
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