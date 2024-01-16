namespace CloudCustomers.Api.Models;

public class Address
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public int ZipCode { get; set; }
}
