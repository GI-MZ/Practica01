using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Practica01.Data.Helpers;
using Practica01.Data.Interfaces;
using Practica01.Domain;

namespace Practica01.Data.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Factura> GetAll()
        {
            throw new NotImplementedException();
        }

        public Factura GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Factura factura)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection ?cnn = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //parametros de entrada
                cmd.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago.Id);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);
                //parametros de salida
                SqlParameter param = new SqlParameter("@nro_factura", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int FacturaNro = (int)param.Value;

                foreach (var detalle in factura.GetDetalles())
                {
                    var cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", factura.Nro);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", detalle.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio", detalle.Precio);
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", factura.Nro);
                    cmdDetalle.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;

        }
    }
}
