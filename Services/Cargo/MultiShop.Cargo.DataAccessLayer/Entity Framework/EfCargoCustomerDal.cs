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
    public class EfCargoCustomerDal:GenericRepository<CargoCustomer>,ICargoCustomerDal
    {
        private readonly CargoContext _context;
        public EfCargoCustomerDal(CargoContext context) :base(context) 
        {
            _context = context;
        }

        public CargoCustomer GetCargoCustomerById(string id)
        {
            var value = _context.CargoCustomers.Where(x=>x.UserCustomerId == id).FirstOrDefault();
            return value;
        }
    }
}
