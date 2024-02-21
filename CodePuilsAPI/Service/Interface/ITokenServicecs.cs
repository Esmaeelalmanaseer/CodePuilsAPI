using Microsoft.AspNetCore.Identity;

namespace CodePuilsAPI.Service.Interface
{
    public interface ITokenServicecs
    {
        string createToken(IdentityUser user,List<string>Role);
    }
}
