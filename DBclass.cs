using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace savePic
{
    
    class DBclass
    {
        string sql_cmmd;
        string conn_string = "Data Source =.; Initial Catalog = university; User ID = sa; Password=123";
        public string error_msg = "";
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;
        byte[] param;
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter();
        public byte[] Param { get => param; set => param = value; }

        public DBclass(string sql_cmmd)
        {
            this.sql_cmmd = sql_cmmd;
        }
        public string execute_sql()
        {
            try
            {
                conn.ConnectionString = conn_string;
                conn.Open();
                cmd = new SqlCommand(sql_cmmd, conn);
                cmd.Parameters.AddWithValue("@pic", Param);
                cmd.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public DataTable search_info()
        {
            try
            {
                conn.ConnectionString = conn_string;
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql_cmmd, conn);
                adp.SelectCommand = sqlCommand;
                adp.Fill(dt);    
            }
            catch (Exception ex)
            {
                error_msg = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();   
            }
            return dt;
        }
    }
}
