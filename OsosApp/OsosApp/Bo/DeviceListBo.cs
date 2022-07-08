namespace OsosApp.Bo
{
    internal class DeviceListBo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }

        public int BrandId { get; set; }
        public string BrandDesc { get; set; }

        public string Model { get; set; }


        public bool IsProduction { get; set; }
        public bool IsActive { get; set; }
    }
}