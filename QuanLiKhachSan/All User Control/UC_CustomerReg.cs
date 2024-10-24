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
    public partial class UC_CustomerReg : UserControl
    {
        DBconnection db = new DBconnection();

        public UC_CustomerReg()
        {
            InitializeComponent();
            // Đăng ký các event handlers
            cbxRoom.SelectedIndexChanged += cbxRoom_SelectedIndexChanged;
            txtTenLoaiPhong.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            txtLoaiGiuong.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            // Đăng ký event handler cho dịch vụ
            cbxChonDichVu.SelectedIndexChanged += cbxChonDichVu_SelectedIndexChanged;
        }

        private void UC_CustomerReg_Load(object sender, EventArgs e)
        {
            // Chỉ load rooms khi cả hai combobox đã được chọn
            if (txtTenLoaiPhong.SelectedItem != null && txtLoaiGiuong.SelectedItem != null)
            {
                LoadAvailableRooms();
            }

            // Load dịch vụ nếu đã chọn dịch vụ
            if (cbxChonDichVu.SelectedItem != null)
            {
                LoadAvailableDichVu();
            }
        }

        // Event handler chung cho cả hai combobox loại phòng và loại giường
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        private void LoadAvailableRooms()
        {
            // Kiểm tra xem đã chọn cả loại phòng và loại giường chưa
            if (txtTenLoaiPhong.SelectedItem == null || txtLoaiGiuong.SelectedItem == null)
            {
                cbxRoom.DataSource = null;
                txtPrice.Clear();
                return;
            }

            try
            {
                string tenLoaiPhong = txtTenLoaiPhong.SelectedItem.ToString();
                string loaiGiuong = txtLoaiGiuong.SelectedItem.ToString();

                DataTable dtRooms = db.GetAvailableRoomsByCriteria(tenLoaiPhong, loaiGiuong);

                if (dtRooms != null && dtRooms.Rows.Count > 0)
                {
                    cbxRoom.DataSource = dtRooms;
                    cbxRoom.DisplayMember = "SoPhong";
                    cbxRoom.ValueMember = "SoPhong";

                    // Tự động chọn phòng đầu tiên
                    if (cbxRoom.Items.Count > 0)
                    {
                        cbxRoom.SelectedIndex = 0;
                        // Giá sẽ được cập nhật thông qua event SelectedIndexChanged của cbxRoom
                    }
                }
                else
                {
                    cbxRoom.DataSource = null;
                    txtPrice.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải danh sách phòng: " + ex.Message,
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void cbxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoom.SelectedItem is DataRowView selectedRow)
                {
                    decimal gia;
                    if (decimal.TryParse(selectedRow["Gia"].ToString(), out gia))
                    {
                        txtPrice.Text = gia.ToString("N0"); // Định dạng số với dấu phân cách hàng nghìn
                    }
                    else
                    {
                        txtPrice.Text = "0";
                    }
                }
                else
                {
                    txtPrice.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật giá phòng: " + ex.Message,
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void cbxChonDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableDichVu();
        }

        private void LoadAvailableDichVu()
        {
            if (cbxChonDichVu.SelectedItem == null)
            {
                cbxMota.DataSource = null;
                return;
            }

            try
            {
                string tenDichVu = cbxChonDichVu.SelectedItem.ToString();
                DataTable dtDichVu = db.GetAvailableDichVu(tenDichVu);

                if (dtDichVu != null && dtDichVu.Rows.Count > 0)
                {
                    cbxMota.DataSource = dtDichVu;
                    cbxMota.DisplayMember = "MoTa";
                    cbxMota.ValueMember = "TenDichVu";

                    // Tự động chọn mô tả đầu tiên
                    if (cbxMota.Items.Count > 0)
                    {
                        cbxMota.SelectedIndex = 0;
                    }
                }
                else
                {
                    cbxMota.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải thông tin dịch vụ: " + ex.Message,
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void cbxMota_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxMota.SelectedItem is DataRowView selectedRow)
                {
                    string moTa = selectedRow["MoTa"].ToString();
                    // Nếu bạn có TextBox để hiển thị mô tả chi tiết, có thể thêm dòng sau:
                    // txtMoTaChiTiet.Text = moTa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin dịch vụ: " + ex.Message,
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnKhach_Click(object sender, EventArgs e)
        {
            // Collect customer details from the form
            string ten = txtTenKhach.Text;
            DateTime ngaySinh = dateTimePickerNgaySinhKhach.Value;
            string diaChi = txtDiaChiKhach.Text;
            string sdt = txtSDTKhach.Text;
            string email = txtEmailKhach.Text;

            // Add the customer to the database
            db.AddCustomer(ten, ngaySinh, diaChi, sdt, email);

            // Provide feedback to the user
            MessageBox.Show("Customer added successfully.");
        }

        private void btnDangKiPhong_Click(object sender, EventArgs e)
        {
            string sdt = txtSDTPhong.Text;
            int soPhong = int.Parse(cbxRoom.SelectedValue.ToString());

            string result = db.AddCustomerToRoom(sdt, soPhong);

            MessageBox.Show(result);
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                string sdt = txtSDTDichVu.Text;
                string tenDichVu = cbxChonDichVu.SelectedItem.ToString();
                string moTa = cbxMota.SelectedItem.ToString();
                int soLuong = int.Parse(txtSoLuong.Text);

                db.AddServiceToCustomer(sdt, tenDichVu, moTa, soLuong);

                MessageBox.Show("Service added to customer successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the service to the customer: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cbxRoom_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}