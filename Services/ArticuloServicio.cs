using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data.Implementations;
using Practica01.Data.Interfaces;
using Practica01.Domain;

namespace Practica01.Services
{
    public class ArticuloServicio
    {
        private IArticuloRepository _repository;
        public ArticuloServicio()
        {
            _repository = new ArticuloRepository();
        }
        public List<Articulo> GetArticulos()
        {
            return _repository.GetAll();
        }
        public Articulo? GetArticulo(int id)
        {
            return _repository.GetById(id);
        }
        public bool SaveArticulo(Articulo articulo)
        {
            return _repository.Save(articulo);
        }
    }
}
