using CloudCustomers.Api.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixtures
{
    public static List<User> GetTestUsers() => [
        new User {
            Id = 1,
            Name = "Test User 1",
            Email = "test1@testteam.com",
            Address = new Address {
                Street = "Test Street",
                City = "Test City",
                ZipCode = 000000,
            }
        },
        new User {
            Id = 2,
            Name = "Test User 2",
            Email = "test2@testteam.com",
            Address = new Address {
                Street = "Test Street",
                City = "Test City",
                ZipCode = 000012,
            }
        },
        new User {
            Id = 3,
            Name = "Test User 3",
            Email = "test3@testteam.com",
            Address = new Address {
                Street = "Test Street",
                City = "Test City",
                ZipCode = 000034,
            }
        }
    ];
}
