using System.ComponentModel.DataAnnotations;

namespace BookBorrowingApp.Domain.Entities;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    public int Rating { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public bool Is_Book_Available { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid Lent_By_User_id { get; set; } = default!;
    public Guid Currently_Borrowed_By_User_Id { get; set; } = default!;
}
