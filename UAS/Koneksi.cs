using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//1
using System.Data;
using System.Data.SqlClient;

namespace UAS
{
    internal class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data Source=DESKTOP-7R46M35\\MYSERVER; initial catalog=Penjualan_Motor; integrated security=true";
            return Conn;
        }
    }
}
