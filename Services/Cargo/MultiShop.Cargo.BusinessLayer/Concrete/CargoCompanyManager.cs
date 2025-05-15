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
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _cargoCompanyDal.DeleteAsync(id);
        }

        public Task<List<CargoCompany>> TGetAllAsync()
        {
            return _cargoCompanyDal.GetAllAsync();
        }

        public Task<CargoCompany> TGetByIdAsync(int id)
        {
            return _cargoCompanyDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(CargoCompany entity)
        {
            await _cargoCompanyDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(CargoCompany entity)
        {
            await _cargoCompanyDal.UpdateAsync(entity);
        }
    }
}
