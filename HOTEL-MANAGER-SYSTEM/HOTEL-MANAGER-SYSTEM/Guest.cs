using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace HOTEL_MANAGER_SYSTEM
{
    public class Guest : Person
    {

        string chuoiketnoi1 = "Data Source=LAPTOP-IGR3NG0G\\SQLEXPRESS;Initial Catalog=\"My Hotel\";Integrated Security=True;Encrypt=False";
        string sql1;
        SqlConnection ketnoi1;
        SqlCommand thuchien1;


        //-----------------------------------------------------
        //Constructor khong tham so

        public Guest()
        {

        }


        public Guest(int id, string name, DateTime birthday, string gender) : base(id, name, birthday, gender)
        {

        }



        public override string PrintDetails()
        {
            return base.PrintDetails();
        }

        //Phương thức đặt phòng
        public void BookRoom(int idkh, string namekh, string genderkh, string phonenumberkh, DateTime ngayden, DateTime ngaydi, string maphong)
        {
            ketnoi1 = new SqlConnection(chuoiketnoi1);
            ketnoi1.Open();
            sql1 = @"Insert into KHACHHANG (id, name, gender, phoneno, checkin,checkout, roomno)
    VALUES (@id, @name, @gender, @phoneno, @checkin, @checkout, @roomno)";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            thuchien1.Parameters.AddWithValue("@id", idkh);
            thuchien1.Parameters.AddWithValue("@name", namekh);
            thuchien1.Parameters.AddWithValue("@gender", genderkh);
            thuchien1.Parameters.AddWithValue("@phoneno", phonenumberkh);
            thuchien1.Parameters.AddWithValue("@checkin", ngayden);
            thuchien1.Parameters.AddWithValue("@checkout", ngaydi);
            thuchien1.Parameters.AddWithValue("@roomno", maphong);
            
            thuchien1.ExecuteNonQuery();
            ketnoi1.Close();
            //-------------------------------
        }
       /* public void BookRoom(int idkh, string namekh, string genderkh, string phonenumberkh, DateTime ngayden, DateTime ngaydi, string loaiphong)
        {
            ketnoi1 = new SqlConnection(chuoiketnoi1);
            ketnoi1.Open();
            sql1 = "SELECT number FROM Room WHERE type = @type AND status = 'available'";
            SqlCommand command = new SqlCommand(sql1, ketnoi1);
            command.Parameters.AddWithValue("@roomtype", loaiphong);

            SqlDataReader reader = command.ExecuteReader();
            int maphong = 0;
            if (reader.Read())
            {
                maphong = (int)reader["roomno"];
            }
            else
            {
                MessageBox.Show("No room available for this type");
                return;
            }
            ketnoi1.Close();

            
           
            ketnoi1.Open();
            sql1 = @"Insert into KHACHHANG (id, name, gender, phoneno, checkin,checkout, roomno)
    VALUES (@id, @name, @gender, @phoneno, @checkin, @checkout, @roomno)";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            thuchien1.Parameters.AddWithValue("@id", idkh);
            thuchien1.Parameters.AddWithValue("@name", namekh);
            thuchien1.Parameters.AddWithValue("@gender", genderkh);
            thuchien1.Parameters.AddWithValue("@phoneno", phonenumberkh);
            thuchien1.Parameters.AddWithValue("@checkin", ngayden);
            thuchien1.Parameters.AddWithValue("@checkout", ngaydi);
            thuchien1.Parameters.AddWithValue("@roomno", maphong );

            thuchien1.ExecuteNonQuery();
            ketnoi1.Close();
            //-------------------------------
        }
       */


        //Phương thức góp ý cho khách sạn
        public void GopY(int id, string name, DateTime ngaygopy, string gopy)
        {
            ketnoi1 = new SqlConnection(chuoiketnoi1);
            ketnoi1.Open();
            sql1 = @"Insert into FEEDBACK (id, name, ngaygopy, gopy)
            VALUES (@id, @name, @ngaygopy, @gopy)";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            thuchien1.Parameters.AddWithValue("@id", id);
            thuchien1.Parameters.AddWithValue("@name", name);
            thuchien1.Parameters.AddWithValue("@ngaygopy", ngaygopy);
            thuchien1.Parameters.AddWithValue("@gopy", gopy);
            thuchien1.ExecuteNonQuery();
            ketnoi1.Close();
        }

        //Yêu cầu thêm dịch vụ
        public void DatDichVu(int idkh, int sttdv, string tendv, float giadv)
        {
            int IdTest = idkh;
            using (ketnoi1 = new SqlConnection(chuoiketnoi1))
            {
                ketnoi1.Open();
                using (thuchien1 = new SqlCommand("SELECT COUNT(*) FROM KHACHHANG2 WHERE id = @id", ketnoi1))
                {
                    thuchien1.Parameters.AddWithValue("@id", IdTest);
                    int count = (int)thuchien1.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Khách hàng không tồn tại, vui lòng kiểm tra lại ID khách hàng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }


            ketnoi1 = new SqlConnection(chuoiketnoi1);
            ketnoi1.Open();
            sql1 = @"Insert into DATDICHVU (idkh, sttdv, tendv, giadv)
            VALUES (N'" + IdTest + @"','" + sttdv + @"','" + tendv + @"','" + giadv + @"')";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            thuchien1.ExecuteNonQuery();
            ketnoi1.Close();

            MessageBox.Show("Khách đặt dịch vụ thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}