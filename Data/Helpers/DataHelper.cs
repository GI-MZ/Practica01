using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Practica01.Data.Helpers
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(@"Data Source=GISSEL-MENDEZ\SQLEXPRESS;Initial Catalog=ARTICULOS_FACTURA;Integrated Security=True;Encrypt=False");
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public DataTable ExecuteSPQuery(string sp)
        {   DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null; //si falla la conexion y no cargo nada
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public bool ExecuteSpDml(string sp, List<SpParameter>? param)
        {
            int rows;
            try
            {
                
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                    }
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();

               
            }
            catch (SqlException ex)
            {
                rows = 0;
            }
            return rows;
            
        }
        public int ExecuteTransaction(string sp, List <SpParameter>? param, SqlTransaction transaction)
        {
            int rows;
            try
            {

            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

    }
}
