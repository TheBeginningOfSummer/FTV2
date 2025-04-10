using System;
using System.Data;
using System.Data.SQLite;

namespace Services
{
    public class SQLiteTool
    {
        private static void SetCommand(SQLiteCommand command, SQLiteConnection connection, string sqlString, params SQLiteParameter[] parameters)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                command.Parameters.Clear();
                command.Connection = connection;
                command.CommandText = sqlString;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 30;
                if (parameters != null)
                {
                    foreach (SQLiteParameter parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="sqlString">SQL命令</param>
        /// <param name="parameters">可选参数</param>
        /// <returns>所查询的数据</returns>
        public static DataSet ExecuteQuery(string connectionString, string sqlString, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        SetCommand(command, connection, sqlString, parameters);
                        SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                        dataAdapter.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception)
                    {
                        return dataSet;
                    }
                }
            }
        }
        /// <summary>
        /// 数据库执行指令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="sqlString">SQL命令</param>
        /// <param name="parameters">可选参数</param>
        /// <returns>是否成功</returns>
        public static bool ExecuteSQL(string connectionString, string sqlString, params SQLiteParameter[] parameters)
        {
            bool result = true;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    SetCommand(command, connection, sqlString, parameters);
                    SQLiteTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static DataTable GetTableList(string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                DataTable schemaTable = connection.GetSchema("TABLES");
                schemaTable.Columns.Remove("TABLE_CATALOG");
                schemaTable.Columns.Remove("TABLE_SCHEMA");
                schemaTable.Columns["TABLE_NAME"].SetOrdinal(0);
                return schemaTable;
            }
        }
    }
}
