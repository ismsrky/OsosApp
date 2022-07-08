using OsosApp.Bo;
using OsosApp.Business;
using OsosApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsosApp
{
    public partial class usDevice : UserControl
    {
        #region Var
        int modemId = 0;

        SqlDeviceBusiness sqlDeviceBusiness = new SqlDeviceBusiness();
        SqlDeviceBrandBusiness sqlDeviceBrandBusiness = new SqlDeviceBrandBusiness();

        List<DeviceListBo> deviceList = null;
        List<EnumDeviceBrand> brandList = null;

        Device selectedDevice = null;
        bool IsNew = false;
        #endregion

        #region Form
        public usDevice()
        {
            InitializeComponent();

            Init();
        }
        #endregion

        #region Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            selectedDevice.ModemId = modemId;

            selectedDevice.SerialNo = txtSerialNo.Text;
            selectedDevice.Name = txtName.Text;
            selectedDevice.IsActive = chkIsActive.Checked;
            selectedDevice.BrandId = cmbBrandId.SelectedValue.ToInt32();
            selectedDevice.Model = txtModel.Text;
            selectedDevice.IsProduction = chkIsProduction.Checked;
            sqlDeviceBusiness.Save(selectedDevice);

            LoadList();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelectedRowChange();
        }
        #endregion

        #region Method
        void LoadList()
        {
            ResponseBo<List<DeviceListBo>> responseBo = sqlDeviceBusiness.GetList(modemId);
            deviceList = responseBo.Bo;

            dataGridView1.DataSource = deviceList;

            if (deviceList == null || deviceList.Count() == 0)
            {
                New();
            }
        }
        void Clear()
        {
            txtId.Text = "";
            txtSerialNo.Text = "";
            txtName.Text = "";
            txtModel.Text = "";
            chkIsProduction.Checked = false;
            chkIsActive.Checked = false;
        }
        void New()
        {
            IsNew = true;

            Clear();

            chkIsActive.Checked = true;

            selectedDevice = new Device();
            selectedDevice.IsProduction = false;
            selectedDevice.IsActive = true;
            selectedDevice.IsDeleted = false;
            selectedDevice.Id = -1;
        }
        void Delete()
        {
            if (selectedDevice == null || selectedDevice.Id <= 0)
            {
                MessageBox.Show("Kayıt bulunamadı.", "Geçersiz İşlem",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MessageBox.Show($"Sayacı (Seri No:{selectedDevice.SerialNo}) Silmek İstediğinize Emin Misiniz?", "Sil Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            selectedDevice.IsDeleted = true;
            sqlDeviceBusiness.Save(selectedDevice);

            LoadList();
        }
        void Init()
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ResponseBo<List<EnumDeviceBrand>> responseBo = sqlDeviceBrandBusiness.GetList();
            brandList = responseBo.Bo;
            cmbBrandId.DataSource = brandList;
            cmbBrandId.DisplayMember = "Description";
            cmbBrandId.ValueMember = "Id";

            //dataGridView1.Columns.Add(new DataGridViewColumn()
            //{
            //    Name = "Id",
            //    DataPropertyName = "Id",
            //    ReadOnly = true,
            //    HeaderText="Id"
            //});

            //dataGridView1.Columns.Add("Id", "Id");

            // DeviceCount
        }

        void SelectedRowChange()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            DeviceListBo deviceListBo = dataGridView1.CurrentRow.DataBoundItem as DeviceListBo;
            if (deviceListBo == null)
            {
                return;
            }

            ResponseBo<Device> responseBo = sqlDeviceBusiness.Get(deviceListBo.Id);

            if (!responseBo.IsSuccess)
            {

                MessageBox.Show(responseBo.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (responseBo.Bo == null)
            {
                MessageBox.Show("Sayaç bulunamadı", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            selectedDevice = responseBo.Bo;
            IsNew = false;

            txtId.Text = selectedDevice.Id.ToString();
            txtSerialNo.Text = selectedDevice.SerialNo;
            txtName.Text = selectedDevice.Name;
            chkIsActive.Checked = selectedDevice.IsActive;
            cmbBrandId.SelectedValue = selectedDevice.BrandId;
            txtModel.Text = selectedDevice.Model;
        }

        public void ShowForm(int modemId)
        {
            this.modemId = modemId;

            LoadList();
        }
        #endregion
    }
}