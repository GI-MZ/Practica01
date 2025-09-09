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

        public List<DetallleFactura> Detalles { get; set; }

        public List<DetallleFactura> GetDetalles()
        {
            return Detalles;
        }
  


    }
}
