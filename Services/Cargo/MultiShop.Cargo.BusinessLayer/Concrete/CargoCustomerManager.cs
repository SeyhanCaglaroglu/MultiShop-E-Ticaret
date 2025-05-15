using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;

        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _cargoCustomerDal.DeleteAsync(id);
        }

        public Task<List<CargoCustomer>> TGetAllAsync()
        {
            return _cargoCustomerDal.GetAllAsync();
        }

        public  Task<CargoCustomer> TGetByIdAsync(int id)
        {
            return _cargoCustomerDal.GetByIdAsync(id);
        }

        public CargoCustomer TGetCargoCustomerById(string id)
        {
            return _cargoCustomerDal.GetCargoCustomerById(id);
        }

        public async Task TInsertAsync(CargoCustomer entity)
        {
            await _cargoCustomerDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(CargoCustomer entity)
        {
            await _cargoCustomerDal.UpdateAsync(entity);
        }
    }
}
