using Core.Entities;
using System.Security.Claims;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
