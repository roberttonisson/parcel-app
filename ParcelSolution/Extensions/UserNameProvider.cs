using Contracts.DAL.Base;
using Microsoft.AspNetCore.Http;

#pragma warning disable 1591


namespace Extensions
{
    public class UserNameProvider : IUserNameProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserNameProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "-";
    }
}