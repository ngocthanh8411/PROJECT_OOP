using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HOTEL_MANAGER_SYSTEM
{
    public class Manager : Person
    {
        string chuoiketnoi = "Data Source=LAPTOP-IGR3NG0G\\SQLEXPRESS;Initial Catalog=\"My Hotel\";Integrated Security=True;Encrypt=False";
        string sql;

        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;


        //-----------------------------------------------------------------------------

        public Manager()
        {

        }
        public Manager(int id, string name, DateTime birthday, string gender) : base(id, name, birthday, gender)
        {

        }
        // Phương thức PrintDetails
        public override string PrintDetails()
        {
            return base.PrintDetails();
        }

        //-----------------------------------------------------------------------------

        //Thêm phòng cho khách sạn
        public void AddRoom(int roomnumber, string roomtype, int bedcount, float price, string roomcondition)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Insert into ADDROOM(roomno, roomtype, bedcount, price, roomcondition)
            VALUES (N'" + roomnumber + @"','" + roomtype + @"','" + bedcount + @"','" + price + @"','" + roomcondition + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }

        //Sửa thông tin phòng
        public void EditRoomInformation(int roomnumber, string roomtype, int bedcount, float price)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"UPDATE ADDROOM SET
            roomno = N'" + roomnumber + @"', roomtype = N'" + roomtype + @"', bedcount = N'" + bedcount + @"', price = N'" + price + @"'
            WHERE (roomno = N'" + roomnumber + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }

        //Xóa phòng
        public void DeleteRoom(int roomnumber)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Delete From ADDROOM WHERE (roomno = N'" + roomnumber + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }

        //Quản lí nhân viên
       //Them nhan vien
        public void ThemNhanVien(int id, string name, string exp, float salary)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Insert into NHANVIEN (id, name, exp, salary)
            VALUES (N'" + id + @"','" + name + @"','" + exp + @"','" + salary + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }
            //Xoa nhan vien
        public void XoaNhanVien(int id)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Delete From NHANVIEN WHERE (id = N'" + id + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }

        //Quản lí cơ sở vật chất của khách sạn
        public void FacilitiesManager()
        {

        }

        

        //Xem doanh thu
        public void XemDoanhThu(DateTime begin, DateTime end)
        {
            DateTime ngayBatDau = begin;
            DateTime ngayKetThuc = end;

            // Thực hiện truy vấn SQL để lấy tổng tổngtien trong khoảng thời gian đã chọn
            sql = "SELECT SUM(tongtien) AS TongTien FROM THANHTOAN WHERE ngayden >= @NgayBatDau AND ngaydi <= @NgayKetThuc";

            using (ketnoi = new SqlConnection(chuoiketnoi))
            {
                using (SqlCommand command = new SqlCommand(sql, ketnoi))
                {
                    // Thêm tham số cho ngày bắt đầu và kết thúc
                    command.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                    try
                    {
                        ketnoi.Open();

                        // Thực hiện truy vấn và lấy kết quả
                        object result = command.ExecuteScalar();

                        // Hiển thị kết quả trên giao diện người dùng
                        if (result != DBNull.Value && result != null)
                        {
                            MessageBox.Show("Tổng tiền trong khoảng thời gian này là: " + result.ToString(), "Thông báo doanh thu!");
                        }
                        else
                        {
                            MessageBox.Show("Không có doanh thu trong khoảng thời gian này!", "Thông báo doanh thu!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }
        public void XemDoanhThu(DateTime begin, DateTime end, System.Windows.Forms.ListView listView) { 
            listView.Items.Clear();
            int j = 0;
            DateTime ngaybatdau = begin;
            DateTime ngayketthuc = end;

            sql = "SELECT * FROM THANHTOAN WHERE ngayden >= @ngaybatdau AND ngaydi <= @ngayketthuc";

            using (ketnoi = new SqlConnection(chuoiketnoi))
            {
                thuchien = new SqlCommand(sql, ketnoi);
                
                    // Thêm tham số cho ngày bắt đầu và kết thúc
                    thuchien.Parameters.AddWithValue("@ngayBatDau", ngaybatdau);
                    thuchien.Parameters.AddWithValue("@ngayKetThuc", ngayketthuc);
                    ketnoi.Open();
                    docdulieu = thuchien.ExecuteReader();

                    while (docdulieu.Read())
                    {
                        listView.Items.Add(docdulieu[0].ToString());
                        listView.Items[j].SubItems.Add(docdulieu[1].ToString());
                        listView.Items[j].SubItems.Add(docdulieu[2].ToString());
                        listView.Items[j].SubItems.Add(docdulieu[3].ToString());
                        j++;
                    }
                    docdulieu.Close();
                               
            }

        }
        public void XemPhanHoi()
        {
            string result = "";
            // Thực hiện truy vấn SQL để lấy tất cả thông tin từ bảng
            sql = "SELECT * FROM FEEDBACK"; // Thay thế YourTableName bằng tên thực tế của bảng

            using (ketnoi = new SqlConnection(chuoiketnoi))
            {
                using (thuchien = new SqlCommand(sql, ketnoi))
                {
                    try
                    {
                        ketnoi.Open();

                        // Sử dụng SqlDataReader để đọc dữ liệu từ bảng
                        using (docdulieu = thuchien.ExecuteReader())
                        {
                            // Duyệt qua tất cả các dòng dữ liệu
                            while (docdulieu.Read())
                            {
                                // Lấy giá trị từ các cột trong dòng hiện tại
                                object id = docdulieu[0]; // Thay "id" bằng tên cột thực tế
                                object name = docdulieu[1]; // Thay "name" bằng tên cột thực tế
                                object gopy = docdulieu[2]; // Thay "gopy" bằng tên cột thực tế

                                // Hiển thị thông tin trong MessageBox hoặc làm gì đó khác với thông tin này
                                result += $"{id} {name} {gopy} \n";
                            }
                        }
                        MessageBox.Show(result,"Tất cả phản hồi của khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //phuong thuc overload
        public void XemPhanHoi(DateTime begin, DateTime end, System.Windows.Forms.ListView listView)
        {
            listView.Items.Clear();
            int j = 0;
            DateTime ngaybatdau = begin;
            DateTime ngayketthuc = end;

            sql = "SELECT * FROM FEEDBACK WHERE ngaygopy >= @ngaybatdau AND ngaygopy <= @ngayketthuc";

            using (ketnoi = new SqlConnection(chuoiketnoi))
            {
                thuchien = new SqlCommand(sql, ketnoi);

                // Thêm tham số cho ngày bắt đầu và kết thúc
                thuchien.Parameters.AddWithValue("@ngayBatDau", ngaybatdau);
                thuchien.Parameters.AddWithValue("@ngayKetThuc", ngayketthuc);
                ketnoi.Open();
                docdulieu = thuchien.ExecuteReader();

                while (docdulieu.Read())
                {
                    listView.Items.Add(docdulieu[0].ToString());
                    listView.Items[j].SubItems.Add(docdulieu[1].ToString());
                    listView.Items[j].SubItems.Add(docdulieu[2].ToString());
                    listView.Items[j].SubItems.Add(docdulieu[3].ToString());
                    j++;
                }
                docdulieu.Close();

            }

        }

        public void XemCongSuatPhong(DateTime begin, DateTime end, System.Windows.Forms.ListView listView)
        {
            listView.Items.Clear();
            int j = 0;
            DateTime ngaybatdau = begin;
            DateTime ngayketthuc = end;

            sql = "SELECT roomcondition, COUNT (*) AS COUNT FROM ADDROOM GROUP BY roomcondition";
            using (ketnoi = new SqlConnection(chuoiketnoi))
            {
                thuchien = new SqlCommand(sql, ketnoi);

                // Thêm tham số cho ngày bắt đầu và kết thúc
                thuchien.Parameters.AddWithValue("@ngayBatDau", ngaybatdau);
                thuchien.Parameters.AddWithValue("@ngayKetThuc", ngayketthuc);
                ketnoi.Open();
                docdulieu = thuchien.ExecuteReader();

                while (docdulieu.Read())
                {
                    listView.Items.Add(docdulieu[0].ToString());
                    listView.Items[j].SubItems.Add(docdulieu[1].ToString());
                    //listView.Items[j].SubItems.Add(docdulieu[2].ToString());
                    //listView.Items[j].SubItems.Add(docdulieu[3].ToString());
                    j++;
                }
                docdulieu.Close();

            }
        }

        public void AddService(int stt, string namedv, float giadv)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Insert into DICHVU (stt, tendv, giadv)
            VALUES (N'" + stt + @"','" + namedv + @"','" + giadv + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
        }
    }
}