using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//3
using System.Data;
using System.Data.SqlClient;

namespace UAS
{
    public partial class Form1 : Form
    {
        //4
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        Koneksi Konn = new Koneksi();

        void NoTransaksiOtomatis()
        {
            long hitung;
            string urutan;
            SqlDataReader rd;
            SqlConnection conn = Konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select No_Transaksi from TBL_MOTOR where No_Transaksi in(select max(No_Transaksi) from TBL_MOTOR) order by No_Transaksi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["No_Transaksi"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "NR" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else
            {
                urutan = "NR001";
            }
            rd.Close();
            textBox1.Text = urutan;
            conn.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }


        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            TampilBarang();
            NoTransaksiOtomatis();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilBarang();
            Bersihkan();
            NoTransaksiOtomatis();
        }

        void TampilBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_MOTOR", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_MOTOR");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_MOTOR";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*****************************************************
             * Memeriksa apakah kolom text kosong ? *
             ****************************************************/
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox7.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Mohon isikan terlebih dahulu kolom-kolom yang tersedia");

            }
            else
            {
                /*****************************************************
                 * Simpan Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("INSERT INTO TBL_MOTOR VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "' ,'" + textBox5.Text + "' ,'" + textBox6.Text + "' ,'" + textBox7.Text + "' ,'" + comboBox1.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Data Berhasil!!");
                    TampilBarang();
                    Bersihkan();
                }
                catch (Exception X)
                {
                    MessageBox.Show("Tidak dapat menyimpan Data");
                }
            }
        }

        void CariBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_MOTOR where [No_Transaksi] like '%" + textBox8.Text + "%' or [Nama_Motor] like '%" + textBox8.Text + "%'", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_MOTOR");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_MOTOR";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["No_Transaksi"].Value.ToString();
                textBox2.Text = row.Cells["Nama_Motor"].Value.ToString();
                textBox3.Text = row.Cells["Harga_Motor"].Value.ToString();
                textBox4.Text = row.Cells["Jumlah_Motor"].Value.ToString();
                textBox5.Text = row.Cells["No_Plat"].Value.ToString();
                textBox6.Text = row.Cells["Massa_Plat"].Value.ToString();
                textBox7.Text = row.Cells["Tahun"].Value.ToString();
                comboBox1.Text = row.Cells["Keresidenan"].Value.ToString();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*****************************************************
             * Memeriksa apakah kolom text kosong ? *
             ****************************************************/
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox7.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Mohon isikan terlebih dahulu kolom-kolom yang tersedia");

            }
            else
            {
                /*****************************************************
                 * Simpan Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("UPDATE TBL_MOTOR SET No_Transaksi='" + textBox1.Text + "', Nama_Motor='" + textBox2.Text + "', Harga_Motor='" + textBox3.Text + "',Jumlah_Motor='" + textBox4.Text + "' ,No_Plat='" + textBox5.Text + "' ,Massa_Plat='" + textBox6.Text + "' ,Tahun='" + textBox7.Text + "' ,Keresidenan='" + comboBox1.Text + "' WHERE No_Transaksi='" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Data Berhasil!!");
                    TampilBarang();
                    Bersihkan();
                }
                catch (Exception X)
                {
                    MessageBox.Show("Tidak dapat menyimpan Data");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(textBox2.Text + ",Yakin ingin dihapus?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                /*****************************************************
                 * Hapus Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                cmd = new SqlCommand("DELETE TBL_MOTOR WHERE No_Transaksi='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hapus Data Berhasil!!");
                TampilBarang();
                Bersihkan();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bersihkan();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            CariBarang();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}