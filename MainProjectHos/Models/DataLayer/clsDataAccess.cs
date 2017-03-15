using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataLayer
{
    public class clsDataAccess
    {

        string mstrConnection = string.Empty;
        SqlConnection msqlcon = null;
        SqlCommand msqlcmd = null;
        SqlDataAdapter msqlsda = null;
        SqlTransaction sqltrans = null;

        public clsDataAccess()
        {
            mstrConnection = GetConnection();
        }

        public DataTable GetDataTable(string spName)
        {
            DataTable ldt = new DataTable();
            msqlcon = new SqlConnection(mstrConnection);
            msqlcmd = new SqlCommand();
            msqlsda = new SqlDataAdapter();
            msqlcmd.Connection = msqlcon;
            msqlcmd.CommandType = CommandType.StoredProcedure;
            msqlcmd.CommandText = spName;
            msqlsda.SelectCommand = msqlcmd;
            msqlsda.Fill(ldt);
            return ldt;
        }

        public DataTable GetDataTable(string spName, List<SqlParameter> sqlParam)
        {
            DataTable ldt = new DataTable();
            msqlcon = new SqlConnection(mstrConnection);
            msqlcmd = new SqlCommand();
            msqlsda = new SqlDataAdapter();
            msqlcmd.Connection = msqlcon;
            msqlcmd.CommandType = CommandType.StoredProcedure;
            msqlcmd.CommandText = spName;
            if (sqlParam != null)
            {
                if (sqlParam.Count > 0)
                {
                    for (int i = 0; i < sqlParam.Count; i++)
                    {
                        msqlcmd.Parameters.Add(sqlParam[i]);
                    }
                }
            }
            msqlsda.SelectCommand = msqlcmd;
            msqlsda.Fill(ldt);
            return ldt;
        }


        public int ExecuteQuery(string spName, List<SqlParameter> sqlParam)
        {
            int cnt = 0;
            try
            {
                msqlcon = new SqlConnection(mstrConnection);
                msqlcmd = new SqlCommand();
                msqlcmd.Connection = msqlcon;
                msqlcmd.CommandType = CommandType.StoredProcedure;
                msqlcmd.CommandText = spName;
                if (sqlParam != null)
                {
                    if (sqlParam.Count > 0)
                    {
                        for (int i = 0; i < sqlParam.Count; i++)
                        {
                            msqlcmd.Parameters.Add(sqlParam[i]);
                        }
                    }
                }
                msqlcon.Open();
                cnt = msqlcmd.ExecuteNonQuery();
                msqlcon.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return cnt;
        }


        public int ExecuteScalar(string lstrQuery)
        {
            int cnt = 0;
            try
            {
                msqlcon = new SqlConnection(mstrConnection);
                msqlcmd = new SqlCommand();
                msqlcmd.Connection = msqlcon;
                msqlcmd.CommandType = CommandType.Text;
                msqlcmd.CommandText = lstrQuery;
                msqlcon.Open();
                cnt = Convert.ToInt32(msqlcmd.ExecuteScalar());
                msqlcon.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return cnt;
        }


        public int ExecuteTransaction(List<string> spName, List<List<SqlParameter>> sqlParam)
        {
            int cnt = 0;

            if (spName.Count > 0)
            {
                try
                {
                    msqlcon = new SqlConnection(mstrConnection);
                    msqlcmd = new SqlCommand();

                    for (int i = 0; i < spName.Count; i++)
                    {
                        msqlcmd = msqlcon.CreateCommand();
                        msqlcmd.Connection = msqlcon;
                        msqlcmd.CommandType = CommandType.StoredProcedure;
                        msqlcmd.CommandText = spName[i];

                        List<SqlParameter> param = sqlParam[i];

                        if (param != null)
                        {
                            if (param.Count > 0)
                            {
                                for (int j = 0; j < param.Count; j++)
                                {
                                    msqlcmd.Parameters.Add(param[j]);
                                }
                            }
                        }

                        if (i == 0)
                        {
                            msqlcmd.Connection.Open();
                            sqltrans = msqlcmd.Connection.BeginTransaction();
                        }
                        if (msqlcmd.Connection.State == ConnectionState.Closed)
                        {
                            msqlcmd.Connection.Open();
                        }
                        msqlcmd.Transaction = sqltrans;
                        msqlcmd.ExecuteNonQuery();
                    }

                    sqltrans.Commit();
                    cnt = 1;
                }
                catch (Exception ex)
                {
                    if (sqltrans != null)
                    {
                        sqltrans.Rollback();
                        cnt = 0;
                    }
                }
                finally
                {
                    msqlcmd.Connection.Close();
                }
            }
            return cnt;
        }


        public string GetConnection()
        {
            string lstrConnection = string.Empty;
            //lstrConnection = "Persist Security Info=False;User ID=sa;Password=deepak;Initial Catalog=GovindGreenHouse;Data Source=DEEPAK-PC";   
            lstrConnection = System.Configuration.ConfigurationManager.ConnectionStrings["SevadhamHospitalConnectionString"].ConnectionString;
            return lstrConnection;
        }

    }
}