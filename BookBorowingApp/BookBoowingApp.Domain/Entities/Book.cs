using System.ComponentModel.DataAnnotations;

namespace BookBorrowingApp.Domain.Entities;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string CoverImage { get; set; } = default!;
    public List<string>? Images { get; set; } = default!;
    public int Rating { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public bool Is_Book_Available { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid Lent_By_User_id { get; set; } = default!;
    public Guid? Currently_Borrowed_By_User_Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public DateTime LastUpdated { get; set; } = default!;
}
