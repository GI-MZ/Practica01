using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Practica01.Domain;

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
        public DataTable ExecuteSPQuery(string sp, List<SpParameter>? param = null)
        {   DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;////
                
                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                    }
                }
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

        public bool ExecuteSpDml(string sp, List<SpParameter>? param=null)
        {
            bool result;
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

                int affectedRows = cmd.ExecuteNonQuery();
                result = affectedRows > 0;
               

               
            }
            catch (SqlException ex)
            {
                result = false;
            }
            finally
            {
                _connection.Close();
            }
            return result;
            
        }

        public bool ExecuteTransaction(Factura factura)
        {
            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            var cmd = new SqlCommand("SP_INSERTAR_MAESTRO", _connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago.Id);
            cmd.Parameters.AddWithValue("@cliente", factura.Cliente);
            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows <= 0)
            {
                transaction.Rollback();
                return false;
            }
            else
            {
                foreach (DetallleFactura df in factura.Detalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", _connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    //param de entrada
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", factura.Nro);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", df.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", df.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio", df.Articulo.PrecioUnitario);

                    //param de salida
                    SqlParameter param = new SqlParameter("@nro_factura", System.Data.SqlDbType.Int);
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();

                    int affectedRowsDetalle = cmdDetalle.ExecuteNonQuery();
                    if (affectedRowsDetalle<=0)
                    {
                        transaction.Rollback(); 
                        return false;
                    }

                }
                transaction.Commit();
                return true;
            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

    }
}
