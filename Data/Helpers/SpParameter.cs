using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Data.Helpers
{
    public class SpParameter
    {
        public string Nombre { get; set; }
        public object Valor{ get; set; }

        public SpParameter() { }

        public SpParameter(string nombre, object valor)
        {
            this.Nombre = nombre;
            this.Valor = valor;
        }
    }
}
