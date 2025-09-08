using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data.Interfaces
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();
        bool Save(Factura factura);
        Factura GetById(int id);
    }
}
