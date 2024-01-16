namespace CloudCustomers.Api.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
}
