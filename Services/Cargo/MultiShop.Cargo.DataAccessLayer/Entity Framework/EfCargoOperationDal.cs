using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repository;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Entity_Framework
{
    public class EfCargoOperationDal:GenericRepository<CargoOperation>,ICargoOperationDal
    {
        public EfCargoOperationDal(CargoContext context) : base(context)
        {
            
        }
    }
}
