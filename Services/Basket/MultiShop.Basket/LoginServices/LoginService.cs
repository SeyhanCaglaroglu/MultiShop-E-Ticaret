namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        // Burda Token uzerindeki id degerini alip bu degere atiyip sepet islemlerini bu GetUserId ye gore yapilcak
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value; 
    }
}
