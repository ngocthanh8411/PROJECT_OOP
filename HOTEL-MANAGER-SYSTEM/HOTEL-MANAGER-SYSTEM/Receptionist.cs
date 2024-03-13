using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HOTEL_MANAGER_SYSTEM
{
    public class Receptionist : Person
    {
        private float salary;
        private string experience;
        public Receptionist()
        {

        }

        public Receptionist(int id, string name, DateTime birthday, string gender, float salary, string experience) : base(id, name, birthday, gender)
        {
           Experience = experience;
            Salary = salary;
        }
        public string Experience {  get { return experience; } set { experience = value; } }
        public float Salary { get { return salary; } set { salary = value; } }
        public override string PrintDetails()
        {
            return base.PrintDetails() + "\nExperience" + experience + "\nSalary" + salary;
        }
        

        //----------------------------------------------------------
        string chuoiketnoi = "Data Source=LAPTOP-IGR3NG0G\\SQLEXPRESS;Initial Catalog=\"My Hotel\";Integrated Security=True;Encrypt=False";
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        //----------------------------------------------------------


        //Phương thức thanh toán
        public void ThanhToan(string roomno, int id)
        {
            string phongkhacho, songaykhacho, tongtien;

            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @" Select ADDROOM.roomno, KHACHHANG2.name, Datediff(day, KHACHHANG2.checkin, KHACHHANG2.checkout), Datediff(day, KHACHHANG2.checkin, KHACHHANG2.checkout) * ADDROOM.price
            FROM ADDROOM Inner Join KHACHHANG2
            ON ADDROOM.roomno = KHACHHANG2.roomno
            Where (KHACHHANG2.roomno = N'" + roomno + @"') and (KHACHHANG2.id = N'" + id + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            int i = 0;
            while (docdulieu.Read())
            {
                phongkhacho = docdulieu[0].ToString();
                songaykhacho = docdulieu[2].ToString();
                tongtien = docdulieu[3].ToString();
                i++;
            }
            ketnoi.Close();
        }

        //Duyệt khách hàng
        public void DuyetKH(int idkh, string namekh, string genderkh, string phonenumberkh, DateTime ngayden, DateTime ngaydi, string maphong)
        {
            //Duyệt và thêm khách hàng vừa duyệt vào KHACHHANG2
            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Insert into KHACHHANG2 (id, name, gender, phoneno, checkin, checkout, roomno)
    VALUES (@id, @name, @gender, @phoneno, @checkin, @checkout, @roomno)";
            thuchien = new SqlCommand(sql, ketnoi);
            // Tạo và gán giá trị cho các tham số
            thuchien.Parameters.AddWithValue("@id", idkh);
            thuchien.Parameters.AddWithValue("@name", namekh);
            thuchien.Parameters.AddWithValue("@gender", genderkh);
            thuchien.Parameters.AddWithValue("@phoneno", phonenumberkh);
            thuchien.Parameters.AddWithValue("@checkin", ngayden);
            thuchien.Parameters.AddWithValue("@checkout", ngaydi);
            thuchien.Parameters.AddWithValue("@roomno", maphong);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();

            //Xóa khách hàng vừa được duyệt trong KHACHHANG

            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi.Open();
            sql = @"Delete From KHACHHANG WHERE (id = N'" + idkh + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();

            //Doi trang thai booked
            ketnoi.Open();
            sql = @"UPDATE ADDROOM SET
            roomcondition = N'" + "Booked" + @"'
            WHERE (roomno = N'" + maphong + @"')";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();


        }
    }
}