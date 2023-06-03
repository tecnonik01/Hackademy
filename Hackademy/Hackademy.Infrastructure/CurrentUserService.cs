using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Infrastructure
{
    public class CurrentUserService
    {
        public int UserId { get; set; }
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

            }
            catch (Exception)
            {
                return;
            }
            
        }
    }
}
