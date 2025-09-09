using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data.Helpers;
using Practica01.Data.Implementations;
using Practica01.Data.Interfaces;
using Practica01.Domain;

namespace Practica01.Services
{
    public class FacturaService
    {
        private IFacturaRepository _repository;
        public FacturaService()
        {
            _repository = new FacturaRepository();

        }

        public List<Factura> GetFacturas()
        {
            return _repository.GetAll();
        }

        public Factura? GetFacturaById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveFactura(Factura factura)
        {
            return _repository.Save(factura);
        }
        //public bool DeleteFactura(int id)
        //{
        //    var facturaEnBD = _repository.GetById(id);
        //    return facturaEnBD != null ? _repository.Delete()id : false;
        //}
        public bool ExecuteTransaction(Factura factura)
        {
            return DataHelper.GetInstance().ExecuteTransaction(factura);
        }
    }
}
