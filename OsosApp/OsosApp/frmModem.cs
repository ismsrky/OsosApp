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
    public partial class frmModem : Form
    {
        #region Var
        SqlModemBusiness sqlModemBusiness = new SqlModemBusiness();
        List<ModemListBo> modemList = null;

        Modem selectedModem = null;
        bool IsNew = false;
        #endregion

        #region Form
        public frmModem()
        {
            InitializeComponent();

            Init();
        }

        private void frmModem_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        #endregion

        #region Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            selectedModem.SerialNo = txtSerialNo.Text;
            selectedModem.Name = txtName.Text;
            selectedModem.Ip = txtIp.Text;
            selectedModem.Port = txtPort.Text.ToInt32();
            selectedModem.IsActive = chkIsActive.Checked;
            sqlModemBusiness.Save(selectedModem);

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
            ResponseBo<List<ModemListBo>> responseBo = sqlModemBusiness.GetList();
            modemList = responseBo.Bo;

            dataGridView1.DataSource = modemList;

            if (modemList == null || modemList.Count() == 0)
            {
                New();
            }
        }
        void Clear()
        {
            txtId.Text = "";
            txtSerialNo.Text = "";
            txtName.Text = "";
            txtIp.Text = "";
            txtPort.Text = "";
            chkIsActive.Checked = false;

            usDevice1.ShowForm(0);
        }
        void New()
        {
            IsNew = true;

            Clear();

            chkIsActive.Checked = true;

            selectedModem = new Modem();
            selectedModem.IsActive = true;
            selectedModem.IsDeleted = false;
            selectedModem.Id = -1;

            txtSerialNo.Focus();
        }
        void Delete()
        {
            if (selectedModem == null || selectedModem.Id <= 0)
            {
                MessageBox.Show("Kayıt bulunamadı.", "Geçersiz İşlem",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MessageBox.Show($"Modemi(Seri No:{selectedModem.SerialNo}) Silmek İstediğinize Emin Misiniz?", "Sil Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            selectedModem.IsDeleted = true;
            sqlModemBusiness.Save(selectedModem);
            LoadList();
        }
        void Init()
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

            ModemListBo modemListBo = dataGridView1.CurrentRow.DataBoundItem as ModemListBo;
            if (modemListBo == null)
            {
                return;
            }

            ResponseBo<Modem> responseBo = sqlModemBusiness.Get(modemListBo.Id);

            if (!responseBo.IsSuccess)
            {

                MessageBox.Show(responseBo.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (responseBo.Bo == null)
            {
                MessageBox.Show("Modem bulunamadı", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            selectedModem = responseBo.Bo;
            IsNew = false;

            txtId.Text = selectedModem.Id.ToString();
            txtSerialNo.Text = selectedModem.SerialNo;
            txtName.Text = selectedModem.Name;
            txtIp.Text = selectedModem.Ip;
            txtPort.Text = selectedModem.Port.ToString();
            chkIsActive.Checked = selectedModem.IsActive;

            usDevice1.ShowForm(selectedModem.Id);
        }
        #endregion
    }
}