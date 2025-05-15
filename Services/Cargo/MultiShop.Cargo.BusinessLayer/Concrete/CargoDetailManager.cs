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
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _cargoDetailDal.DeleteAsync(id);
        }

        public Task<List<CargoDetail>> TGetAllAsync()
        {
            return _cargoDetailDal.GetAllAsync();
        }

        public Task<CargoDetail> TGetByIdAsync(int id)
        {
            return _cargoDetailDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(CargoDetail entity)
        {
            await _cargoDetailDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(CargoDetail entity)
        {
            await _cargoDetailDal.UpdateAsync(entity);
        }
    }
}
