using Microsoft.AspNetCore.Http;

namespace UserService.Helpers.Extensions
{
    public static class CookieExtension
    {
        public static void AddCookie(this HttpResponse response, string key, string value, int? expireTime = null)
        {
            var option = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true
            };


            // if (expireTime.HasValue)  
            //     option.Expires = DateTime.Now.AddMinutes(expireTime.Value);  
            // else  
            //     option.Expires = DateTime.Now.AddMilliseconds(10);  
   
            response.Cookies.Append(key, value, option);
            
        }
    }
}