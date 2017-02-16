using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Wms12m.Configuration;
using Wms12m.Entity;
using Wms12m.Security;

namespace Wms12m.Log
{
    public sealed class Logger
    {
        //public string ConnectionString { get; set; }
        public void Writer(LogCs Parametre)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["LogConnectionString"].ConnectionString;
            string username = Identity.Current.User.UserName;
            string machine = Identity.Current.Action.Machine;
            string ipAddress = Identity.Current.Action.IpAddress;
            string url = Identity.Current.Action.Url;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("Insert into ApplicationLog(UserName,Machine,IpAddress,Description,Method,Url) values(@UserName, @Machine, @IpAddress, @Description,@Method,@Url)", connection);
            command.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@Machine", System.Data.SqlDbType.VarChar).Value = machine;
            command.Parameters.Add("@IpAddress", System.Data.SqlDbType.VarChar).Value = ipAddress;
            command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = Parametre.Description;
            command.Parameters.Add("@Method", System.Data.SqlDbType.VarChar).Value = Parametre.Method;
            command.Parameters.Add("@Url", System.Data.SqlDbType.VarChar).Value = url;
            if (connection.State == ConnectionState.Closed) connection.Open();
            command.BeginExecuteNonQuery(AsyncCommandCompletionCallback, command);
        }
        private static void AsyncCommandCompletionCallback(IAsyncResult result)
        {
            SqlCommand command = null;
            try
            {
                command = (SqlCommand)result.AsyncState;
                command.EndExecuteNonQuery(result);
            }
            catch (SqlException)
            {
                // Todo: Log with another Logger.
                //throw new LogException(LogExceptionMessage.LOG_WRITE_DB_ERROR, ex);
            }
            catch (Exception)
            {
                // Todo: Log with another Logger.
                //throw new LogException(LogExceptionMessage.LOG_WRITE_FATAL_ERROR, exx);
            }
            finally
            {
                command.Connection.Close();
                command.Dispose();
            }
        }
    }
}
