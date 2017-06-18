using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GooLuck_LineNotify.classData
{
    public class clsData
    {
        private SqlCommand com = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlConnection conn = new SqlConnection();

        public clsData()
        {
        }

        public string Connect(string CS)
        {
            string functionReturnValue = null;
            try
            {
                if (!(conn.State == ConnectionState.Closed))
                {
                    functionReturnValue = "Error : Connection is still open. Unable to connect";
                    return functionReturnValue;
                }
                conn = new SqlConnection(CS);
                conn.Open();

                functionReturnValue = "Success";
            }
            catch (Exception ex)
            {
                functionReturnValue = "Error : " + ex.Message;
            }
            return functionReturnValue;
        }

        public string Disconnect()
        {
            string functionReturnValue = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    functionReturnValue = "Error : Connection is already closed";
                    return functionReturnValue;
                }
                conn.Close();
                functionReturnValue = "Success";
            }
            catch (Exception ex)
            {
                functionReturnValue = "Error : " + ex.Message;
            }
            return functionReturnValue;
        }

        public string GetConnectionStatus()
        {
            string functionReturnValue = null;
            try
            {
                switch (conn.State)
                {
                    case ConnectionState.Broken:
                        functionReturnValue = "Connection is broken";
                        break;

                    case ConnectionState.Closed:
                        functionReturnValue = "Connection is closed";
                        break;

                    case ConnectionState.Connecting:
                        functionReturnValue = "Connection is connecting";
                        break;

                    case ConnectionState.Executing:
                        functionReturnValue = "Connection is executing";
                        break;

                    case ConnectionState.Fetching:
                        functionReturnValue = "Connection is fetching";
                        break;

                    case ConnectionState.Open:
                        functionReturnValue = "Connection is open";
                        break;
                }
            }
            catch (Exception ex)
            {
                functionReturnValue = "Error : " + ex.Message;
            }
            return functionReturnValue;
        }

        //This fuction use for select sql query(without parameter) and return to dataset

        public DataSet zd_SelectQuery(string CS, string Sqlquery, string TableName, ref string strError)
        {
            DataSet functionReturnValue = null;
            try
            {
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(Sqlquery, CS);
                da.Fill(ds, TableName);

                da.Dispose();

                functionReturnValue = ds;

                ds.Dispose();
            }
            catch (Exception ex)
            {
                strError = ex.Message.ToString();
                functionReturnValue = null;
            }
            return functionReturnValue;
        }

        //This fuction use for select sql query(with parameter) and return to dataset

        public int zd_IsertQuery(string CS, string Sqlquery, CommandType cmdType, ref string strError)
        {
            int functionReturnValue = 0;
            SqlConnection cn = new SqlConnection(CS);
            try
            {
                SqlCommand com = new SqlCommand();

                com.Connection = cn;
                com.CommandType = cmdType;
                com.CommandText = Sqlquery;
                cn.Open();
                functionReturnValue = com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 | ex.Number == 2627)
                {
                    strError = ex.Number.ToString();
                }
                else
                {
                    strError = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message.ToString();
            }
            finally
            {
                cn.Close();
            }
            return functionReturnValue;
        }
    }//End Class main
}//End Namespace