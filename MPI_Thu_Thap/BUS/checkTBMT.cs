using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI_Thu_Thap.DAO;

namespace MPI_Thu_Thap.BUS
{
    public class checkTBMT
    {
        public bool checkTB(string id)
        {
            string count = "";
            connectionString conn = new connectionString();
            SqlConnection con = new SqlConnection(conn.conStr);
            string sql = "select count (*) from TBMT where MaThongBao = '" + id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                 count = reader[0].ToString();
            }

            if (count != "0")
            {
                return true;
            }
            return false;
        }
    }
}
