using System.IdentityModel.Tokens.Jwt;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

public interface IAuthService
{
    public Task<ServiceResult> Register(RegisterUser registerUser);

    public Task<ServiceResult<JwtSecurityToken>> Login(LoginUser loginUser);
}