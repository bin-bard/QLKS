using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan
{
    internal class DBconnection
    {

        SqlConnection conAdmin = new SqlConnection(@"Data Source=ACER\SQLEXPRESS;Initial Catalog=KhachSan;Persist Security Info=True;User ID=sa;Password=123456;");
        public SqlConnection getConnectionAdmin
        {
            get
            {
                return conAdmin;
            }
        }

        public void openConnectionAdmin()
        {
            if (conAdmin.State == ConnectionState.Closed)
            {
                conAdmin.Open();
            }
        }

        public void closeConnectionAdmin()
        {
            if (conAdmin.State == ConnectionState.Open)
            {
                conAdmin.Close();
            }
        }

        public void AddBranch(string ten, string diaChi, string sdt, string email)
        {
            using (SqlCommand cmd = new SqlCommand("AddBranch", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetBranches()
        {
            DataTable dtBranches = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewBranches", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtBranches);
                closeConnectionAdmin();
            }
            return dtBranches;
        }

        public void AddRoom(string soPhong, string tenLoaiPhong, string loaiGiuong, decimal gia, int MaKhachSan)
        {
            using (SqlCommand cmd = new SqlCommand("AddRoom", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                cmd.Parameters.AddWithValue("@TenLoaiPhong", tenLoaiPhong);
                cmd.Parameters.AddWithValue("@LoaiGiuong", loaiGiuong);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@MaKhachSan", MaKhachSan);
                cmd.Parameters.AddWithValue("@TinhTrang", "trong");

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetRooms()
        {
            DataTable dtRooms = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewRooms", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtRooms);
                closeConnectionAdmin();
            }
            return dtRooms;
        }

        public void AddDichVu(string tenDichVu, string moTa, decimal gia)
        {
            using (SqlCommand cmd = new SqlCommand("AddDichVu", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenDichVu", tenDichVu);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.Parameters.AddWithValue("@Gia", gia);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetDichVu()
        {
            DataTable dtDichVu = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewDichVu", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtDichVu);
                closeConnectionAdmin();
            }
            return dtDichVu;
        }

        public void AddKhuyenMai(int? maNV, string tenKhuyenMai, decimal phanTramGiam, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            using (SqlCommand cmd = new SqlCommand("AddKhuyenMai", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", (object)maNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TenKhuyenMai", tenKhuyenMai);
                cmd.Parameters.AddWithValue("@PhanTramGiam", phanTramGiam);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetKhuyenMai()
        {
            DataTable dtKhuyenMai = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewKhuyenMai", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtKhuyenMai);
                closeConnectionAdmin();
            }
            return dtKhuyenMai;
        }

        public DataTable GetAvailableRoomsByCriteria(string tenLoaiPhong, string loaiGiuong)
        {

            DataTable dtRooms = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GetAvailableRoomsByCriteria(@TenLoaiPhong, @LoaiGiuong)", getConnectionAdmin))
            {
                cmd.Parameters.AddWithValue("@TenLoaiPhong", tenLoaiPhong);
                cmd.Parameters.AddWithValue("@LoaiGiuong", loaiGiuong);

                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtRooms);
                closeConnectionAdmin();
            }
            return dtRooms;

        }


        public DataTable GetAvailableDichVu(string tenDichVu)
        {

            DataTable dtDichVu = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GetAvailableDichVu(@TenDichVu)", getConnectionAdmin))
            {
                cmd.Parameters.AddWithValue("@TenDichVu", tenDichVu);

                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtDichVu);
                closeConnectionAdmin();
            }
            return dtDichVu;

        }

        public void AddCustomer(string ten, DateTime ngaySinh, string diaChi, string sdt, string email)
        {
            using (SqlCommand cmd = new SqlCommand("AddCustomer", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetCustomers()
        {
            DataTable dtCustomers = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewCustomer", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtCustomers);
                closeConnectionAdmin();
            }
            return dtCustomers;
        }

        public DataTable GetCustomersInHotel()
        {
            DataTable dtCustomers = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM KhachDangSuDungPhong", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtCustomers);
                closeConnectionAdmin();
            }
            return dtCustomers;
        }

        public string AddCustomerToRoom(string sdt, int soPhong)
        {
            using (SqlCommand cmd = new SqlCommand("AddCustomerToRoom", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@SoPhong", soPhong);

                SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(resultParam);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();

                return resultParam.Value.ToString();
            }
        }

        public void AddServiceToCustomer(string sdt, string tenDichVu, string moTa, int soLuong)
        {
            using (SqlCommand cmd = new SqlCommand("AddServiceToCustomer", getConnectionAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@TenDichVu", tenDichVu);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                openConnectionAdmin();
                cmd.ExecuteNonQuery();
                closeConnectionAdmin();
            }
        }

        public DataTable GetInvoiceDetails()
        {
            DataTable dtInvoiceDetails = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ViewInvoiceDetails", getConnectionAdmin))
            {
                openConnectionAdmin();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtInvoiceDetails);
                closeConnectionAdmin();
            }
            return dtInvoiceDetails;
        }




    }
}
