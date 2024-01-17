using CloudCustomers.Api.Models;

namespace CloudCustomers.Api;

public class UserService(HttpClient httpClient) : IUserService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<User>> GetAllUsers()
    {
        var userResponse = await _httpClient.GetAsync("http://youtube.com/");

        return [];
    }
}
