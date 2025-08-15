namespace Library;

public class Book
{
    public readonly string Title;
    public readonly string Author;
    public Patron? Reader { get; private set; }

    public Book(string title, string author, Patron? reader)
    {
        Title = title;
        Author = author;
        Reader = reader;
    }

    public bool IsAvailable => Reader is null;

    public void Borrow(Patron reader)
    {
        if (!IsAvailable) throw new BookNotAvailableException();
        Reader = reader;
        reader.BorrowedBooks.Add(this);
    }

    public void Return()
    {
        if (IsAvailable) throw new BookAlreadyAvailableException();
        Reader!.BorrowedBooks.Remove(this);
        Reader = null;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }
}

public class BookNotAvailableException() : Exception("this book is not available");
public class BookAlreadyAvailableException() : Exception("this book is already available");