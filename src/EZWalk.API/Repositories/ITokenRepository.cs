using Microsoft.AspNetCore.Identity;

namespace EZWalk.API.Repositories;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}