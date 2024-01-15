using GraphQl_Demo.GraphQl.Record;

namespace GraphQl_Demo.GraphQl.Query;

public class BookQuery
{
    // Dummy Book Database
    private readonly List<Book> _books =
    [
        new Book(1,
            "GraphQL DotNet Web Api",
            "Learn DotNet 8 Web Api with added GraphQL endpoint.",
            4.3,
            new Author(1, "Rahul Dey", 4.5)
        ),
        new Book(2,
            "GraphQL Express Api",
            "Learn Node JS Express Api with added GraphQL endpoint.",
            4.8,
            new Author(2, "ChaiAurCode", 4.9)
        ),
        new Book(3,
            "GraphQL Java Spring Boot Api",
            "Learn Java Spring Boot Api with added GraphQL endpoint.",
            4.6,
            new Author(3, "Telusko", 4.4)
        ),
    ];

    public List<Book> GetBooks() => _books;

    public Book? GetBook(int id) => _books.FirstOrDefault(x => x.Id == id);

    public Author? GetAuthor(int id) => _books.Where(x => x.Author.Id == id).FirstOrDefault()?.Author;

    public List<Author> GetAuthors() => _books.Select(x => x.Author).ToList();
}
