using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsosApp.Bo
{
    internal class ModemListBo
    {
        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public bool IsActive { get; set; }

        public int DeviceCount { get; set; } = 0;
    }
}