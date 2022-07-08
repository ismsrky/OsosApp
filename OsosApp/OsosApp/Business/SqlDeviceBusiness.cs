using OsosApp.Bo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.Business
{
    internal class SqlDeviceBusiness
    {
        public ResponseBo<List<DeviceListBo>> GetList(int modemId)
        {
            ResponseBo<List<DeviceListBo>> responseBo = new ResponseBo<List<DeviceListBo>>();

            try
            {
                List<Device> deviceList = null;

                List<DeviceListBo> deviceListBo = null;
                DeviceListBo bo = null;
                using (ososDbEntities db = new ososDbEntities())
                {
                    deviceList = db.Device.Where(x => x.ModemId == modemId && !x.IsDeleted).ToList();

                    if (deviceList.Count() > 0)
                    {
                        deviceListBo = new List<DeviceListBo>();

                        foreach (Device item in deviceList)
                        {
                            bo = new DeviceListBo()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                SerialNo = item.SerialNo,

                                BrandId = item.BrandId,
                                BrandDesc = item.EnumDeviceBrand.Description,

                                Model = item.Model,

                                IsProduction = item.IsProduction,
                                IsActive = item.IsActive
                            };

                            deviceListBo.Add(bo);
                        }
                    }

                    responseBo.Success(deviceListBo);
                }
            }
            catch (Exception ex)
            {
                responseBo.FailedWithException(ex);
            }

            return responseBo;
        }

        public ResponseBo<Device> Get(int id)
        {
            ResponseBo<Device> responseBo = new ResponseBo<Device>();

            try
            {
                Device device = null;

                using (ososDbEntities db = new ososDbEntities())
                {
                    device = db.Device.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

                    responseBo.Success(device);
                }
            }
            catch (Exception ex)
            {
                responseBo.FailedWithException(ex);
            }

            return responseBo;
        }

        public ResponseBo Save(Device device)
        {
            ResponseBo responseBo = new ResponseBo();

            bool isNew = device.Id <= 0;

            try
            {
                using (ososDbEntities db = new ososDbEntities())
                {
                    if (isNew)
                    {
                        device.CreateDateTime = DateTime.UtcNow;

                        db.Device.Add(device);
                    }
                    else
                    {
                        device.UpdateDateTime = DateTime.UtcNow;

                        db.Device.Attach(device);
                        db.Entry(device).State = EntityState.Modified;
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