using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data.Helpers;
using Practica01.Data.Interfaces;
using Practica01.Domain;

namespace Practica01.Data.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS");

            foreach (DataRow row in dt.Rows)
            {
                Articulo a = new Articulo();
                a.Id = (int)row["id_articulo"];
                a.Nombre = (string)row["nombre"];
                a.PrecioUnitario = (float)row["precio_unitario"];
                lst.Add(a);
            }
            return lst;
        }

        public Articulo? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Nombre = "@id_articulo",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS_POR_ID");

            if (dt !=null && dt.Rows.Count >0)
            {
                Articulo a = new Articulo()
                {
                    Id = (int)dt.Rows[0]["id_articulo"],
                    Nombre = (string)dt.Rows[0]["nombre"],
                    PrecioUnitario = (float)dt.Rows[0]["precio_unitario"]
                };
                return a;
            }
            return null;
        }

        public bool Save(Articulo articulo)
        {
            bool exito;

            List<SpParameter> param = new List<SpParameter>
            {
                new SpParameter()
                {
                    Nombre = "@id_articulo",
                    Valor = articulo.Id
                },
                new SpParameter()
                {
                    Nombre="@nombre",
                    Valor=articulo.Nombre
                },
                new SpParameter()
                {
                    Nombre="@precio_unitario",
                    Valor= articulo.PrecioUnitario
                }

            };
            exito = DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULO", param);
            return exito;
        }
    }
}
