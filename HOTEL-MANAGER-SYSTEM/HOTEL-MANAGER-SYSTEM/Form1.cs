using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace HOTEL_MANAGER_SYSTEM
{
    public partial class Form1 : Form
    {
        string filename = "Report";
        public Form1()
        {
            InitializeComponent();
        }
        string chuoiketnoi = "Data Source=LAPTOP-IGR3NG0G\\SQLEXPRESS;Initial Catalog=\"My Hotel\";Integrated Security=True;Encrypt=False";
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        //------------------------------------------------------------------
        string chuoiketnoi1 = "Data Source=LAPTOP-IGR3NG0G\\SQLEXPRESS;Initial Catalog=\"My Hotel\";Integrated Security=True;Encrypt=False";
        string sql1;
        SqlConnection ketnoi1;
        SqlCommand thuchien1;
        SqlDataReader docdulieu1;
        int j = 0; 
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'my_HotelDataSet.ADDROOM' table. You can move, or remove it, as needed.
            this.aDDROOMTableAdapter1.Fill(this.my_HotelDataSet.ADDROOM);

            ketnoi = new SqlConnection(chuoiketnoi);
            ketnoi1 = new SqlConnection(chuoiketnoi1);
            hienthi();
            hienthi1();
            hienthi4();
            hienthi5();
            //hienthi6();
            //hienthi7();
           
        }

        // hien thi cua listview1
        public void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"Select roomno, roomtype, bedcount, price, roomcondition FROM ADDROOM";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                listView1.Items.Add(docdulieu[0].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[1].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[2].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[3].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[4].ToString());
                i++;
            }
            ketnoi.Close();
        }

        // hien thi cua listview2
        public void hienthi1()
        {
            listView2.Items.Clear();
            ketnoi1.Open();
            sql1 = @"Select id, name, gender, phoneno, checkin, checkout, roomno FROM KHACHHANG";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listView2.Items.Add(docdulieu1[0].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[2].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[3].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[4].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[5].ToString());
                listView2.Items[j].SubItems.Add(docdulieu1[6].ToString());
                j++;
            }
            ketnoi1.Close();
        }

        //Hiển thị của listview 3
        public void hienthi3()
        {
            listView3.Items.Clear();
            ketnoi1.Open();
            sql1 = @"Select id, name, exp, salary FROM NHANVIEN";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listView3.Items.Add(docdulieu1[0].ToString());
                listView3.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listView3.Items[j].SubItems.Add(docdulieu1[2].ToString());
                
                j++;
            }
            ketnoi1.Close();
        }

        //Hiển thị của listview nhân viên
        public void hienthi4()
        {
            listView4.Items.Clear();
            ketnoi1.Open();
            sql1 = @"Select id, name, exp, salary FROM NHANVIEN";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listView4.Items.Add(docdulieu1[0].ToString());
                listView4.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listView4.Items[j].SubItems.Add(docdulieu1[2].ToString());
                listView4.Items[j].SubItems.Add(docdulieu1[3].ToString());
                j++;
            }
            ketnoi1.Close();
        }

        //Hiển thị của listview thêm dịch vụ
        public void hienthi5()
        {
            listView5.Items.Clear();
            ketnoi1.Open();
            sql1 = @"Select stt, tendv, giadv FROM DICHVU";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listView5.Items.Add(docdulieu1[0].ToString());
                listView5.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listView5.Items[j].SubItems.Add(docdulieu1[2].ToString());
                j++;
            }
            ketnoi1.Close();
        }

        //Thêm phòng
        private void bt_addroom1_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.AddRoom(int.Parse(tb_roomnumber1.Text), cb_roomtype1.Text, int.Parse(tb_bedcount1.Text), float.Parse(tb_price1.Text), cb_roomcondition1.Text);
                MessageBox.Show("Đã thêm phòng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_roomnumber1.Clear();
                tb_bedcount1.Clear();
                tb_price1.Clear();
                cb_roomtype1.SelectedIndex = -1;
                cb_roomcondition1.SelectedIndex = -1;
                hienthi();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!","Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        //Sửa thông tin phòng
        private void bt_editroominformation_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.EditRoomInformation(int.Parse(tb_roomnumber1.Text), cb_roomtype1.Text, int.Parse(tb_bedcount1.Text), float.Parse(tb_price1.Text));

                MessageBox.Show("Sửa thông tin phòng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                hienthi();
            }
            catch 
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        //Click vào để hiển thị dữ liệu lên TextBox
        private void listView1_Click(object sender, EventArgs e)
        {
            tb_roomnumber1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            cb_roomtype1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            tb_bedcount1.Text = listView1.SelectedItems[0].SubItems[2].Text;
            tb_price1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            cb_roomcondition1.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void bt_deleteroom_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.DeleteRoom(int.Parse(tb_roomnumber1.Text));

                MessageBox.Show("Xóa phòng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                hienthi();
            }
            catch 
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void bt_bookroom2_Click(object sender, EventArgs e)
        {
            try
            {
                //Dat phong
                Guest guest = new Guest();
                guest.BookRoom(int.Parse(tb_id2.Text), tb_name2.Text, cb_gender2.Text, tb_phonenumber2.Text, DateTime.Parse(dtp_checkin2.Text), DateTime.Parse(dtp_checkout2.Text), cb_roomnumber2.Text);
                MessageBox.Show("Đã đặt phòng, vui lòng chờ nhân viên kiểm duyệt", "Thông báo!");
                
                hienthi1();



                //----------------------------------------------------------------------------------------------
                //listView2.Items.Clear();
                //ketnoi1 = new SqlConnection(chuoiketnoi1);
                //ketnoi1.Open();
                //sql1 = @"Insert into KHACHHANG (id, name, gender, phoneno, checkin, checkout, roomno)
                //VALUES (N'" + tb_id2.Text + @"','" + tb_name2.Text + @"','" + cb_gender2.Text + @"','" + tb_phonenumber2.Text + @"','" + dtp_checkin2.Text + @"','" + dtp_checkout2.Text + @"','" + cb_roomnumber2.Text + @"')";
                //thuchien1 = new SqlCommand(sql1, ketnoi1);
                //thuchien1.ExecuteNonQuery();
                //ketnoi1.Close();
                //hienthi1();
                //----------------------------------------------------------------------------------------------
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        //Xóa khách hàng
        private void bt_deleteguest_Click(object sender, EventArgs e)
        {
            try
            {
                ketnoi1 = new SqlConnection(chuoiketnoi1);
                ketnoi1.Open();
                sql1 = @"Delete From KHACHHANG WHERE (id = N'" + tb_id2.Text + @"')";
                thuchien1 = new SqlCommand(sql1, ketnoi1);
                thuchien1.ExecuteNonQuery();
                ketnoi1.Close();
                hienthi1();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void listView2_Click(object sender, EventArgs e)
        {
            tb_id2.Text = listView2.SelectedItems[0].SubItems[0].Text;
            tb_name2.Text = listView2.SelectedItems[0].SubItems[1].Text;
            cb_gender2.Text = listView2.SelectedItems[0].SubItems[2].Text;
            tb_phonenumber2.Text = listView2.SelectedItems[0].SubItems[3].Text;
            dtp_checkin2.Text = listView2.SelectedItems[0].SubItems[4].Text;
            dtp_checkout2.Text = listView2.SelectedItems[0].SubItems[5].Text;
            cb_roomnumber2.Text = listView2.SelectedItems[0].SubItems[6].Text;
        }

        //private void bt_timkiemtheoroomnumber3_Click(object sender, EventArgs e)
        //{
        //    listView3.Items.Clear();
        //    ketnoi.Open();
        //    sql = @"Select id, name, gender, phoneno, checkin, checkout, roomno From KHACHHANG2 Where (roomno like '%" + cb_roomnumber3.Text + "%')";

        //    thuchien = new SqlCommand(sql, ketnoi);
        //    docdulieu = thuchien.ExecuteReader();
        //    i = 0;
        //    while (docdulieu.Read())
        //    {
        //        listView3.Items.Add(docdulieu[0].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[1].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[2].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[3].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[4].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[5].ToString());
        //        listView3.Items[i].SubItems.Add(docdulieu[6].ToString());
        //        i++;
        //    }
        //    ketnoi.Close();
        //}

        private void bt_timkiemtheoid3_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            ketnoi.Open();
            sql = @"Select id, name, gender, phoneno, checkin, checkout, roomno From KHACHHANG2 Where (id like '%" + tb_id3.Text + "%')";

            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                listView3.Items.Add(docdulieu[0].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[1].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[2].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[3].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[4].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[5].ToString());
                listView3.Items[i].SubItems.Add(docdulieu[6].ToString());
                i++;
            }
            ketnoi.Close();
        }

        //Thanh toan
        private void bt_thanhtoan3_Click(object sender, EventArgs e)
        {
            try
            {
                Receptionist receptionist = new Receptionist();
                receptionist.ThanhToan(cb_roomnumber3.Text, int.Parse(tb_id3.Text));

                //Thanh toan tien phong
                ketnoi.Open();
                sql = @" Select ADDROOM.roomno, KHACHHANG2.checkin, KHACHHANG2.checkout, Datediff(day, KHACHHANG2.checkin, KHACHHANG2.checkout), Datediff(day, KHACHHANG2.checkin, KHACHHANG2.checkout) * ADDROOM.price
            FROM KHACHHANG2
            Inner Join ADDROOM ON KHACHHANG2.roomno = ADDROOM.roomno
            
            Where (KHACHHANG2.roomno = N'" + cb_roomnumber3.Text + @"') and (KHACHHANG2.id = N'" + tb_id3.Text + @"')";
                thuchien = new SqlCommand(sql, ketnoi);
                docdulieu = thuchien.ExecuteReader();
                while (docdulieu.Read())
                {
                    lb_phongkhacho3.Text = docdulieu[0].ToString();
                    lb_ngaydentt.Text = docdulieu[1].ToString();
                    lb_ngayditt.Text = docdulieu[2].ToString();
                    lb_songaykhacho3.Text = docdulieu[3].ToString();
                    lb_tongtienphong.Text = docdulieu[4].ToString();
                    i++;
                }
                ketnoi.Close();

                //Thanh toan tien dich vu
                int idkh;
                if (int.TryParse(tb_id3.Text, out idkh))
                {
                    // Kết nối đến cơ sở dữ liệu
                    using (ketnoi = new SqlConnection(chuoiketnoi))
                    {
                        ketnoi.Open();

                        // Thực hiện truy vấn SQL để tính tổng giadv dựa trên idkh
                        sql = "SELECT idkh, SUM(giadv) AS TongGiaDV FROM DATDICHVU WHERE idkh = @idkh GROUP BY idkh";
                        using (thuchien = new SqlCommand(sql, ketnoi))
                        {
                            thuchien.Parameters.AddWithValue("@idkh", idkh);

                            // Thực hiện đọc dữ liệu
                            using (docdulieu = thuchien.ExecuteReader())
                            {
                                if (docdulieu.Read())
                                {
                                    // Hiển thị kết quả
                                    lb_tiendichvutt.Text = $"{docdulieu["TongGiaDV"]}";

                                }
                                //else
                                //{
                                //    lb_tiendichvutt.Text = "Không có dữ liệu cho IDKH này.";
                                //}
                            }
                        }
                        ketnoi.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá trị IDKH hợp lệ.");
                }

                //Cộng tiền dịch vụ và tiền phòng
                float total = 0;
                if (lb_tiendichvutt.Text == "")
                {
                    total = float.Parse(lb_tongtienphong.Text);
                    lb_tiendichvutt.Text = 0.ToString();
                    lb_tongtientt.Text = total.ToString();

                }
                else
                {
                    total = float.Parse(lb_tongtienphong.Text) + float.Parse(lb_tiendichvutt.Text);
                    lb_tongtientt.Text = total.ToString();
                }

                //Thêm idkh và tiền phòng vào THANHTOAN
                ketnoi = new SqlConnection(chuoiketnoi);
                ketnoi.Open();
                sql = @"Insert into THANHTOAN (id, ngayden, ngaydi, tongtien)
            VALUES (@id, @ngayden, @ngaydi, @tongtien)";
                thuchien = new SqlCommand(sql, ketnoi);

                // Tạo và gán giá trị cho các tham số
                thuchien.Parameters.AddWithValue("@id", tb_id3.Text);
                thuchien.Parameters.AddWithValue("@ngayden", DateTime.Parse(lb_ngaydentt.Text));
                thuchien.Parameters.AddWithValue("@ngaydi", DateTime.Parse(lb_ngayditt.Text));
                thuchien.Parameters.AddWithValue("@tongtien", lb_tongtientt.Text);

                thuchien.ExecuteNonQuery();
                ketnoi.Close();

                //Sau khi thanh toan xong thi delete KH trong KHACHHANG2

                ketnoi1.Open();
                sql1 = @"Delete From KHACHHANG2 WHERE (id = N'" + tb_id3.Text + @"')";
                thuchien1 = new SqlCommand(sql1, ketnoi1);
                thuchien1.ExecuteNonQuery();
                ketnoi1.Close();
                hienthi1();

                //Xóa tất cả dịch vụ mà khách đã đặt sau khi thanh toán
                ketnoi1.Open();
                sql1 = @"Delete From DATDICHVU WHERE (idkh = N'" + tb_id3.Text + @"')";
                thuchien1 = new SqlCommand(sql1, ketnoi1);
                thuchien1.ExecuteNonQuery();
                ketnoi1.Close();
                hienthi1();

                //Tra lai trang thai unbooked cho room
                ketnoi.Open();
                sql = @"UPDATE ADDROOM SET
            roomcondition = N'" + "UnBooked" + @"'
            WHERE (roomno = N'" + cb_roomnumber3.Text + @"')";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                hienthi();

                MessageBox.Show("Đã thanh toán thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Xóa dữ liệu trên tb
                tb_id3.Clear();
                cb_roomnumber3.SelectedIndex = -1;

                //hienthi3();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void listView3_Click(object sender, EventArgs e)
        {
            tb_id3.Text = listView3.SelectedItems[0].SubItems[0].Text;
            cb_roomnumber3.Text = listView3.SelectedItems[0].SubItems[6].Text;
        }

        private void tb_duyetkhachhang2_Click(object sender, EventArgs e)
        {
            try
            {
                Receptionist receptionist = new Receptionist();
                receptionist.DuyetKH(int.Parse(tb_id2.Text), tb_name2.Text, cb_gender2.Text, tb_phonenumber2.Text, DateTime.Parse(dtp_checkin2.Text), DateTime.Parse(dtp_checkout2.Text), cb_roomnumber2.Text);
                MessageBox.Show("Khách hàng đã được duyệt", "Thông báo!", MessageBoxButtons.OK);
                hienthi();
                hienthi1();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK);
            }
           
        }

        private void bt_xemdoanhthu_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.XemDoanhThu(DateTime.Parse(dtp_begin.Text), DateTime.Parse(dtp_end.Text),listviewThanhToan);
           //hienthi6();
        }

        private void listView4_Click(object sender, EventArgs e)
        {
            tb_idnv.Text = listView4.SelectedItems[0].SubItems[0].Text;
            tb_namenv.Text = listView4.SelectedItems[0].SubItems[1].Text;
            tb_expnv.Text = listView4.SelectedItems[0].SubItems[2].Text;
            tb_salarynv.Text = listView4.SelectedItems[0].SubItems[3].Text;
        }
        private void listView5_Click(object sender, EventArgs e)
        {
            tb_sttdvclick.Text = listView5.SelectedItems[0].SubItems[0].Text;
            tb_tendvclick.Text = listView5.SelectedItems[0].SubItems[1].Text;
            tb_giadvclick.Text = listView5.SelectedItems[0].SubItems[2].Text;
        }

        private void bt_themnhanvien_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.ThemNhanVien(int.Parse(tb_idnv.Text), tb_namenv.Text, tb_expnv.Text, float.Parse(tb_salarynv.Text));

                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_idnv.Clear();
                tb_namenv.Clear();
                tb_expnv.Clear();
                tb_salarynv.Clear();
                hienthi4();
            } catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void bt_xoanhanvien_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.XoaNhanVien(int.Parse(tb_idnv.Text));

                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                hienthi4();
            }catch
            {
                MessageBox.Show("Vui long điền đầy đủ thông tin!" , "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void bt_goigopy_Click(object sender, EventArgs e)
        {
            try
            {
                Guest guest = new Guest();
                guest.GopY(int.Parse(tb_idgopy.Text), tb_namegopy.Text, DateTime.Parse(dt_ngaygopy.Text), tb_gopycuakhach.Text);
                MessageBox.Show("Góp ý thành công, cảm ơn bạn đã góp ý cho khách sạn!");
                tb_idgopy.Clear();
                tb_namegopy.Clear();
                tb_gopycuakhach.Clear();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void bt_xemphanhoi_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.XemPhanHoi(DateTime.Parse(dtp_begin.Text), DateTime.Parse(dtp_end.Text), listviewPhanHoi);
            //hienthi7();
        }

        private void bt_themdichvu_Click(object sender, EventArgs e)
        {
            try
            {
                Manager manager = new Manager();
                manager.AddService(int.Parse(tb_sttthemdv.Text), tb_tendvthemdv.Text, float.Parse(tb_giathemdv.Text));

                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                hienthi5();

                //xóa dứ liệu trên textbox
                tb_sttthemdv.Clear();
                tb_tendvthemdv.Clear();
                tb_giathemdv.Clear();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void bt_xacnhandichvu_Click(object sender, EventArgs e)
        {
            try
            {
                Guest guest = new Guest();
                guest.DatDichVu(int.Parse(tb_idkhdatdichvu.Text), int.Parse(tb_sttdvclick.Text), tb_tendvclick.Text, float.Parse(tb_giadvclick.Text));
                tb_idkhdatdichvu.Clear();
                tb_sttdvclick.Clear();
                tb_tendvclick.Clear();
                tb_giadvclick.Clear();
            }
            catch {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------
        private void listviewBaoCao_Click(object sender, EventArgs e)
        {
            tb_sttdvclick.Text = listView5.SelectedItems[0].SubItems[0].Text;
            tb_tendvclick.Text = listView5.SelectedItems[0].SubItems[1].Text;
            tb_giadvclick.Text = listView5.SelectedItems[0].SubItems[2].Text;
        }
        public void hienthi7()
        {
            listviewPhanHoi.Items.Clear();
            ketnoi1.Open();
            sql1 = @"Select id, name, gopy  FROM FEEDBACK";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listviewPhanHoi.Items.Add(docdulieu1[0].ToString());
                listviewPhanHoi.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listviewPhanHoi.Items[j].SubItems.Add(docdulieu1[2].ToString());
                j++;
            }
            ketnoi1.Close();
        }
       /* public void hienthi6()
        {
            listviewThanhToan.Items.Clear();
            ketnoi1.Open();
            DateTime fromdate = dtp_begin.Value;
            DateTime todate = dtp_end.Value;

            sql1 = @"Select id, ngayden, ngaydi, tongtien FROM THANHTOAN  ";
            thuchien1 = new SqlCommand(sql1, ketnoi1);
            
            docdulieu1 = thuchien1.ExecuteReader();
            j = 0;
            while (docdulieu1.Read())
            {
                listviewThanhToan.Items.Add(docdulieu1[0].ToString());
                listviewThanhToan.Items[j].SubItems.Add(docdulieu1[1].ToString());
                listviewThanhToan.Items[j].SubItems.Add(docdulieu1[2].ToString());
                listviewThanhToan.Items[j].SubItems.Add(docdulieu1[3].ToString());
             
                j++;
            }
            ketnoi1.Close();
        }*/

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            
            
            using (StreamWriter sw = new StreamWriter(@"D:\Project_managerHotel\Revenue.txt"))
            {
                // Write the column headers to the file

                string header = "";
                foreach (ColumnHeader ch in listviewThanhToan.Columns)
                {
                    header += ch.Text + "\t" + "\t";
                }
                sw.WriteLine(header);
                // Write the items and subitems to the file
                foreach (ListViewItem item in listviewThanhToan.Items)
                {
                   
                        // Write the item and subitem to the file
                        string line = "";
                        foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                        {
                            line += subitem.Text + "\t";
                        }

                        sw.WriteLine(line);
                    
                }
            }

            using (StreamWriter sw = new StreamWriter(@"D:\Project_managerHotel\FeedBack.txt"))
            {
                // Write the column headers to the file

                string header = "";
                foreach (ColumnHeader ch in listviewPhanHoi.Columns)
                {
                    header += ch.Text + "\t" + "\t";
                }
                sw.WriteLine(header);
                // Write the items and subitems to the file
                foreach (ListViewItem item in listviewPhanHoi.Items)
                {

                    // Write the item and subitem to the file
                    string line = "";
                    foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                    {
                        line += subitem.Text + "\t";
                    }

                    sw.WriteLine(line);

                }
            }


            using (StreamWriter sw = new StreamWriter(@"D:\Project_managerHotel\Occupany.txt"))
            {
                
                // Write the column headers to the file

                string header = "";
                foreach (ColumnHeader ch in listviewCongSuat.Columns)
                {
                    header += ch.Text + "\t";
                }
                sw.WriteLine(header);
                // Write the items and subitems to the file
                foreach (ListViewItem item in listviewCongSuat.Items)
                {

                    // Write the item and subitem to the file
                    string line = "";
                    foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                    {
                        line += subitem.Text + "\t";
                       
                        
                    }


                    sw.WriteLine(line);
                    
                }
            }
            // Show a message box to confirm the export
            MessageBox.Show("The data has been exported to " + filename, "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXemCongSuatPhong_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.XemCongSuatPhong(DateTime.Parse(dtp_begin.Text), DateTime.Parse(dtp_end.Text), listviewCongSuat);
        }

        private void bt_timkiemtheoroomnumber3_Click(object sender, EventArgs e)
        {

        }
    }

}
