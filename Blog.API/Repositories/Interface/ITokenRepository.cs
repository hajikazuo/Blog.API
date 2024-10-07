using Microsoft.AspNetCore.Identity;

namespace Blog.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, IList<string> roles);
    }
}
