using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class Factura
    {
        public int Nro { get; set; }
        public DateTime Date { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }

        private List<DetallleFactura> Detalles { get; set; }

        public List<DetallleFactura> GetDetalles()
        {
            return Detalles;
        }
        //public Factura(DetalleFactura d)
        //{
        //    Detalles = new List<DetallleFactura>();
        //}
        //public void AddDetalle(DetallleFactura detalle)
        //{
        //    if (detalle != null)
        //        Detalles.Add(detalle);
        //}
        //public void Remove(int index)
        //{
        //    Detalles.RemoveAt(index);
        //}
        //public float Total()
        //{
        //    float total = 0;
        //    foreach (var detalle in Detalles)
        //        total += detalle.Subtotal();
        //    return total;
        //}


    }
}
