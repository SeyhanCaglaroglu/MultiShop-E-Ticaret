using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _dapperContext;

        public DiscountService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";
            var parameters = new DynamicParameters();

            parameters.Add("@code",createCouponDto.Code);
            parameters.Add("@rate", createCouponDto.Rate);
            parameters.Add("@isActive",createCouponDto.IsActive);
            parameters.Add("@validDate", createCouponDto.ValidDate);

            using(var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "Delete From Coupons Where CouponId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            string query = "Select * From Coupons";
            
            using(var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdCouponDto> GetByIdCouponAsync(int id)
        {
            string query = "Select * From Coupons Where CouponId=@id";
            //Bu nesne Sql sorgusunda kullanilacak parametrelerin tanimlanmasini saglar.
            var parameters = new DynamicParameters();
            parameters.Add("@id",id);

            using(var connection = _dapperContext.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query,parameters);
                return value;
            }
        }

        public async Task<GetByIdCouponDto> GetCodeDetailByCodeAsync(string code)
        {
            string query = "Select * From Coupons Where Code=@code";
            //Bu nesne Sql sorgusunda kullanilacak parametrelerin tanimlanmasini saglar.
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            using (var connection = _dapperContext.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query, parameters);
                return value;
            }
        }

        public async Task<int> GetDiscountCouponCount()
        {
            string query = "Select Count(*) From Coupons";            

            using (var connection = _dapperContext.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<int>(query);
                return value;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            string query = "Update Coupons Set Code=@code,Rate=@rate,IsActive=@isActive,ValidDate=@validDate Where CouponId=@id";

            var parameters = new DynamicParameters();

            parameters.Add("@code", updateCouponDto.Code);
            parameters.Add("@rate", updateCouponDto.Rate);
            parameters.Add("@isActive", updateCouponDto.IsActive);
            parameters.Add("@validDate", updateCouponDto.ValidDate);
            parameters.Add("@id",updateCouponDto.CouponId);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
