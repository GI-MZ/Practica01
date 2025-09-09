using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class DetallleFactura
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }

        public float Subtotal ()
        {
            return Precio * Cantidad;
        }
    }
}
