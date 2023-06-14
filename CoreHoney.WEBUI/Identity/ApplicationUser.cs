using Microsoft.AspNetCore.Identity;

namespace CoreHoney.WEBUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
