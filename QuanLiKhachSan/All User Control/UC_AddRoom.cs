using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiKhachSan.All_User_Control
{
    public partial class UC_AddRoom : UserControl
    {   
        DBconnection db = new DBconnection();
        public UC_AddRoom()
        {
            InitializeComponent();
            this.Enter += new EventHandler(UC_AddRoom_Enter);
            LoadBranches();
            
        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            LoadBranches();
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            LoadRooms();
            LoadBranches();
        }

        private void LoadBranches()
        {
            DataTable dtBranches = db.GetBranches();
            comboBoxBranches.DataSource = dtBranches;
            comboBoxBranches.DisplayMember = "Ten";
            comboBoxBranches.ValueMember = "MaKhachSan";
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string soPhong = txtSoPhong.Text;
            string tenLoaiPhong = txtTenLoaiPhong.SelectedItem.ToString();
            string loaiGiuong = txtLoaiGiuong.SelectedItem.ToString();
            decimal gia = decimal.Parse(txtGia.Text);
            int MaKhachSan = (int)comboBoxBranches.SelectedValue;

            db.AddRoom(soPhong, tenLoaiPhong, loaiGiuong, gia, MaKhachSan);
            MessageBox.Show("Room added successfully.");
            LoadRooms();

        }

        private void LoadRooms()
        {
            DataTable dtRooms = db.GetRooms();
            dataGridViewRooms.DataSource = dtRooms;
        }

    }
}
