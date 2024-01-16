using CloudCustomers.Api.Models;

namespace CloudCustomers.Api;

public class UserService : IUserService
{
    public UserService()
    {

    }

    public Task<List<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
