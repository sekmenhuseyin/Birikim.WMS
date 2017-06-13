using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m
{
    public class Connection : IDisposable
    {
        SqlConnection m_SqlCon;
        public SqlConnection SqlCon
        {
            get { return m_SqlCon; }
        }
        public SqlParameterCollection Params { get; set; }

        public SqlCommand Cmd { get; set; }

        DataSet m_DSet;
        public DataSet DSet
        {
            get { return m_DSet; }
        }
        public SqlDataReader Reader { get; set; }

        public CommandType CommandType
        {
            get { return Cmd.CommandType; }
            set { Cmd.CommandType = value; }
        }

        static string configConStr;

        private static bool transActive;

        public SqlTransaction Trans
        {
            get { return Cmd.Transaction; }
            set { Cmd.Transaction = value; }
        }


        public DataSet GetTables(string sqlString, params string[] TableNames)
        {
            m_DSet = new DataSet();
            if (SqlCon.State == ConnectionState.Closed)
                SqlCon.Open();
            Cmd.CommandText = sqlString;
            DSet.Load(Cmd.ExecuteReader(), LoadOption.OverwriteChanges, TableNames);
            return m_DSet;
        }

        public DataTable GetTable(string sqlString)
        {
            GetTables(sqlString, "Table1");
            return m_DSet.Tables[0];
        }

        public DataTable GetTable(string sqlString, string TableName)
        {
            GetTables(sqlString, TableName);
            return m_DSet.Tables[0];
        }

        public int SqlExecute(string sqlString)
        {
            if (SqlCon.State == ConnectionState.Closed)
                SqlCon.Open();
            Cmd.CommandText = sqlString;
            return Cmd.ExecuteNonQuery();
        }

        public SqlDataReader GetReader(string sqlString)
        {
            return GetReader(sqlString, CommandBehavior.Default);
        }

        public SqlDataReader GetReader(string sqlString, CommandBehavior behavior)
        {
            if (SqlCon.State == ConnectionState.Closed)
                SqlCon.Open();
            Cmd.CommandText = sqlString;
            return Cmd.ExecuteReader(behavior);
        }

        public object ExecuteScalar(string sqlString)
        {
            if (SqlCon.State == ConnectionState.Closed)
                SqlCon.Open();
            Cmd.CommandText = sqlString;
            return Cmd.ExecuteScalar();
        }


        private static Connection instance;
        private Connection()
        {
            try
            {
                m_SqlCon = new SqlConnection(configConStr);
                Cmd = new SqlCommand("", m_SqlCon);
                Cmd.CommandTimeout = m_SqlCon.ConnectionTimeout;
                Params = Cmd.Parameters;
                m_DSet = new DataSet();
                Cmd.CommandType = CommandType.Text;
                if (transActive)
                {
                    m_SqlCon.Open();
                    Trans = m_SqlCon.BeginTransaction();
                }
            }
            catch { }
        }
        public static Connection GetConnectionWithTrans()
        {
            transActive = true;
            Connection con = GetCon(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
            if (con.Cmd.Transaction == null)
            {
                if (con.SqlCon.State == ConnectionState.Closed)
                    con.SqlCon.Open();
                con.Cmd.Transaction = con.SqlCon.BeginTransaction();
            }
            return con;
        }
        //public static Connection GetConnectionWithTrans(int ConnectionTimeout)
        //{
        //    transActive = true;
        //    Connection con = GetCon(Degiskenler.GetNewConStr(ConnectionTimeout));
        //    if (con.Cmd.Transaction == null)
        //    {
        //        if (con.SqlCon.State == ConnectionState.Closed)
        //            con.SqlCon.Open();
        //        con.Cmd.Transaction = con.SqlCon.BeginTransaction();
        //    }
        //    return con;
        //}
        public static Connection GetConnection()
        {
            transActive = false;
            return GetCon(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
        }
        //public static Connection GetConnection(int ConnectionTimeout)
        //{            
        //    transActive = false;
        //    return GetCon(Degiskenler.GetNewConStr(ConnectionTimeout));
        //}

        public static Connection GetConnection(string ConStr)
        {
            transActive = false;
            return GetCon(ConStr);
        }
        private static Connection GetCon(string ConStr)
        {
            if (configConStr == null || configConStr != ConStr)
            {
                configConStr = ConStr;
                if (instance != null)
                    instance.Dispose();
            }
            ClearParams();
            return GetConnectionInstance();
        }
        private static Connection GetConnectionInstance()
        {
            lock (typeof(Connection))
            {
                if (instance == null)
                {
                    instance = new Connection();
                }
                return instance;
            }
        }

        public static void ClearParams()
        {
            if (instance != null)
                instance.Params.Clear();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (DSet != null)
            {
                DSet.Dispose();
                m_DSet = null;
            }

            if (Params != null)
                Params = null;

            if (Cmd != null)
            {
                Cmd.Dispose();
                Cmd = null;
            }

            if (Reader != null)
            {
                Reader.Close();
                Reader.Dispose();
                Reader = null;
            }

            if (SqlCon != null)
            {
                SqlCon.Dispose();
                m_SqlCon = null;
            }

            if (instance != null)
                instance = null;
        }
        #endregion
    }
}
