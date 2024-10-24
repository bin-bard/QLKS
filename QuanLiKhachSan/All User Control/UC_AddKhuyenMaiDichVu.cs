using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiKhachSan.All_User_Control
{
    public partial class UC_AddKhuyenMaiDichVu : UserControl
    {
        DBconnection db = new DBconnection();

        public UC_AddKhuyenMaiDichVu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UC_AddKhuyenMaiDichVu_Load(object sender, EventArgs e)
        {
            LoadDichVu();
            LoadKhuyenMai();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            string tenDichVu = txtTenDichVu.SelectedItem.ToString();
            string moTa = txtMoTa.Text;
            decimal gia = decimal.Parse(txtGia.Text);

            db.AddDichVu(tenDichVu, moTa, gia);
            LoadDichVu();
        }

        private void LoadDichVu()
        {
            DataTable dtDichVu = db.GetDichVu();
            dgvDichVu.DataSource = dtDichVu;
        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            int? maNV = string.IsNullOrEmpty(txtMaNV.Text) ? (int?)null : int.Parse(txtMaNV.Text);
            string tenKhuyenMai = txtTenKhuyenMai.Text;
            decimal phanTramGiam = decimal.Parse(txtPhanTramGiam.Text);
            DateTime ngayBatDau = dtpNgayBatDau.Value;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value;

            db.AddKhuyenMai(maNV, tenKhuyenMai, phanTramGiam, ngayBatDau, ngayKetThuc);
            LoadKhuyenMai();
        }

        private void LoadKhuyenMai()
        {
            DataTable dtKhuyenMai = db.GetKhuyenMai();
            dgvKhuyenMai.DataSource = dtKhuyenMai;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
