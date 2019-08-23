using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BLRMIS.Console.ApiDownloadData.DAL
{
    public class SQLProvider
    {
        public const string DefaultConnection = "ConnectionString1";
        protected static Dictionary<string, string> ConnectionStrings = InitConnectionStrings();

        private static Dictionary<string, string> InitConnectionStrings()
        {
            var connectionStrings = new Dictionary<string, string>
            {
                {DefaultConnection, ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString},
            };


            return connectionStrings;
        }

        public string ConnectioName { get; set; }

        protected string GetConnectionString()
        {
            return ConnectionStrings[ConnectioName];
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandName"></param>
        /// <param name="useStoredProcedure"></param>
        /// <returns></returns>
        public SqlCommand GetSqlCommand(string connectionString, string commandName, bool useStoredProcedure)
        {
            var conn = new SqlConnection(connectionString);
            return GetSqlCommand(conn, commandName, useStoredProcedure);
        }


        /// <summary>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandName"></param>
        /// <param name="useStoredProcedure"></param>
        /// <returns></returns>
        public SqlCommand GetSqlCommand(SqlConnection connection, string commandName, bool useStoredProcedure)
        {
            var cmd = new SqlCommand(commandName, connection)
            {
                CommandType = useStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
               //CommandTimeout = ConfigurationReader.GetDefaultCommandTimeOutDuration()
            };
            return cmd;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SqlException">
        ///     A connection-level error occurred while opening the connection. If the
        ///     <see cref="P:System.Data.SqlClient.SqlException.Number" /> property contains the value 18487 or 18488, this
        ///     indicates that the specified password has expired or must be reset. See the
        ///     <see cref="M:System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String)" /> method for more
        ///     information.The &lt;system.data.localdb&gt; tag in the app.config file has invalid or unknown elements.
        /// </exception>
        public SqlConnection GetConnection()
        {
            return GetConnection(GetConnectionString());
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="SqlException">
        ///     A connection-level error occurred while opening the connection. If the
        ///     <see cref="P:System.Data.SqlClient.SqlException.Number" /> property contains the value 18487 or 18488, this
        ///     indicates that the specified password has expired or must be reset. See the
        ///     <see cref="M:System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String)" /> method for more
        ///     information.The &lt;system.data.localdb&gt; tag in the app.config file has invalid or unknown elements.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Cannot open a connection without specifying a data source or server.orThe
        ///     connection is already open.
        /// </exception>
        public static SqlConnection GetConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     The value supplied in the <paramref name="dbType" /> parameter is an invalid
        ///     back-end data type.
        /// </exception>
        public static SqlParameter GetInParameter(string paramName, SqlDbType dbType, object value)
        {
            var param = new SqlParameter(paramName, dbType) { Value = value };
            return param;
        }

        /// <summary>
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     The value supplied in the <paramref name="dbType" /> parameter is an invalid
        ///     back-end data type.
        /// </exception>
        public static SqlParameter GetOutParameter(string paramName, SqlDbType dbType)
        {
            var param = new SqlParameter(paramName, dbType) { Direction = ParameterDirection.Output };
            return param;
        }

        /// <summary>
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     The value supplied in the <paramref name="dbType" /> parameter is an invalid
        ///     back-end data type.
        /// </exception>
        public static SqlParameter GetOutParameter(string paramName, SqlDbType dbType, object value)
        {
            var param = new SqlParameter(paramName, dbType)
            {
                Value = value,
                Direction = ParameterDirection.InputOutput
            };
            return param;
        }


        /// <exception cref="InvalidCastException">
        ///     A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than
        ///     Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A
        ///     <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml
        ///     was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than
        ///     Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.Xml.XmlReader" />.
        /// </exception>
        /// <exception cref="SqlException">
        ///     An exception occurred while executing the command against a locked row. This exception
        ///     is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming
        ///     operation. For more information about streaming, see SqlClient Streaming Support.
        /// </exception>
        /// <exception cref="IOException">
        ///     An error occurred in a <see cref="T:System.IO.Stream" />,
        ///     <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.
        ///     For more information about streaming, see SqlClient Streaming Support.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The current state of the connection is closed.
        ///     <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader" /> requires an open
        ///     <see cref="T:System.Data.SqlClient.SqlConnection" />.The <see cref="T:System.Data.SqlClient.SqlConnection" />
        ///     closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming
        ///     Support.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" />
        ///     or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information
        ///     about streaming, see SqlClient Streaming Support.
        /// </exception>
        public static SqlDataReader ExecuteReader(SqlCommand command)
        {
            var results = command.ExecuteReader();
            return results;
        }


        public static object ExecuteScalar(SqlCommand command)
        {
            var result = command.ExecuteScalar();
            return result;
        }

        /// <exception cref="InvalidCastException">
        ///     A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than
        ///     Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A
        ///     <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml
        ///     was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than
        ///     Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to
        ///     <see cref="T:System.Xml.XmlReader" />.
        /// </exception>
        /// <exception cref="SqlException">
        ///     An exception occurred while executing the command against a locked row. This exception
        ///     is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming
        ///     operation. For more information about streaming, see SqlClient Streaming Support.
        /// </exception>
        /// <exception cref="IOException">
        ///     An error occurred in a <see cref="T:System.IO.Stream" />,
        ///     <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.
        ///     For more information about streaming, see SqlClient Streaming Support.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" />
        ///     or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information
        ///     about streaming, see SqlClient Streaming Support.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped
        ///     during a streaming operation. For more information about streaming, see SqlClient Streaming Support.
        /// </exception>
        public static void ExecuteNonQuery(SqlCommand command)
        {
            command.ExecuteNonQuery();
        }
    }
}