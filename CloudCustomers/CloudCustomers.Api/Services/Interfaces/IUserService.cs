using CloudCustomers.Api.Models;

namespace CloudCustomers.Api;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
}
