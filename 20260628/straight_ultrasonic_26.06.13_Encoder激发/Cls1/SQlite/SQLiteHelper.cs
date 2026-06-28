using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SmartStudio
{
    public class  DBHelper
    {
        private SQLiteConnection m_connection = null;
        private string m_connectionString = "";
        private SQLiteDataAdapter m_dataAdapter = null;
        private DataSet m_dataSet = null;
        private string DBName = "DB\\SQlite_DATA.db";

        public DataSet DataSet
        {
            get { return m_dataSet; }
        }

        public DBHelper(string connectionString)
        {
            m_connectionString = connectionString;
        }

        /// <summary>
        /// 创建一个新的数据库
        /// </summary>
        /// <param name="dbName">数据库文件名称，包含基于可执行文件的根目录路径</param>
        /// <example>CreateNewDatabase（"DB\\PSI_DATA.db"）</example>
        public void CreateNewDatabase(string dbName)
        {
            DBName = dbName;
            string datasource = System.AppDomain.CurrentDomain.BaseDirectory + dbName;
            if (!System.IO.File.Exists(datasource))
            {
                SQLiteConnection.CreateFile(datasource);
            }
        }

        /// <summary>
        /// 打开数据库----- 在具体操作（添加、删除、修改）时要先打开数据库
        /// </summary>
        /// <returns></returns>
        private SQLiteConnection DBOpen()
         {
             SQLiteConnection conn = new SQLiteConnection();
             SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
             connstr.DataSource = System.AppDomain.CurrentDomain.BaseDirectory + DBName;
             conn.ConnectionString = connstr.ToString();
             conn.Open();
             return conn;
         }

        /// <summary>
        /// SQL命令执行操作
        /// </summary>
        /// <param name="commandText">SQL命令字符串</param>
        public void DoSQLiteOperation(string commandText)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = commandText;
            command.Connection = DBOpen();
            command.ExecuteNonQuery();
            command.Dispose();
        }

        /// <summary>
        /// 查询到表中
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public bool QueryToTable(string commandText)
        {
            try
            {
                m_connection = new SQLiteConnection(m_connectionString);
                m_connection.Open();
                m_dataAdapter = new SQLiteDataAdapter(commandText, m_connection);
                m_dataSet = new DataSet();
                m_dataAdapter.Fill(m_dataSet);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_connection.Close();
            }
        }
    }
} 