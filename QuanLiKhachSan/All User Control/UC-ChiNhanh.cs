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
    public partial class UC_ChiNhanh : UserControl
    {
        DBconnection db = new DBconnection();

        public UC_ChiNhanh()
        {
            InitializeComponent();
        }

        private void UC_ChiNhanh_Load(object sender, EventArgs e)
        {
            LoadBranches();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string ten = txtTen.Text;
            string diaChi = txtDiaChi.Text;
            string sdt = txtSDT.Text;
            string email = txtEmail.Text;

            db.AddBranch(ten, diaChi, sdt, email);
            LoadBranches();
        }

        private void LoadBranches()
        {
            DataTable dtBranches = db.GetBranches();
            dataGridViewChiNhanh.DataSource = dtBranches;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
