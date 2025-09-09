using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data.Interfaces
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);
        bool Save(Articulo articulo);
        //delete
    }
}
