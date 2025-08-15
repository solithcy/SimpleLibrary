namespace Library;

public class Library
{
    // TODO: add private list reasoning to readme
    private List<Book> _books = [];
    private List<Patron> _patrons = [];
    public int PatronCount => _patrons.Count;
    
    public Library()
    {
    }

    public void AddBook(Book book)
    {
        if (_books.Contains(book))
        {
            throw new DuplicateBookException();
        }

        _books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        if (!_books.Contains(book))
        {
            throw new NoBookException();
        }

        _books.Remove(book);
    }

    public List<Book> GetBookByAuthor(string author)
    {
        return _books.Where(b => b.Author.Equals(author)).ToList();
    }

    public List<Book> GetBookByTitle(string title)
    {
        return _books.Where(b => b.Title.Equals(title)).ToList();
    }

    public List<Book> GetAllBooks()
    {
        return [.._books]; // we dont want GetAllBooks response mutations to affect the library.
        // member mutations can affect the library members though.
    }

    public void AddPatron(Patron patron)
    {
        patron.RegisterLibrary(this);
        _patrons.Add(patron);
    }

    public void RemovePatron(Patron patron)
    {
        // we do checks here and not above as they could belong to a different library
        if (!_patrons.Contains(patron))
        {
            if (patron.InLibrary) throw new PatronDoesNotHaveLibraryException();
            throw new PatronNotInThisLibraryException();
        }

        if (patron.BorrowedBooks.Count > 0)
        {
            throw new PatronHasUnreturnedBooksException();
        }

        patron.UnregisterLibrary();
        _patrons.Remove(patron);
    }

    public void BorrowBook(Patron patron, Book book)
    {
        if (!_patrons.Contains(patron)) throw new PatronNotInThisLibraryException();
        book.Borrow(patron);
    }

    public void ReturnBook(Patron patron, Book book)
    {
        if (!_patrons.Contains(patron)) throw new PatronNotInThisLibraryException();
        if (!book.Reader?.Equals(patron) ?? false) throw new PatronDoesNotOwnBookException();
        book.Return();
    }

    internal List<Book> GetBooksByPatron(Patron p)
    {
        return _books.Where(b => b.Reader?.Equals(p) ?? false).ToList();
    }
}

public class DuplicateBookException() : Exception("this book is already in the library");

public class NoBookException() : Exception("this book is not in the library");

public class PatronNotInThisLibraryException() : Exception("this patron belongs to a different library");

public class PatronDoesNotOwnBookException() : Exception("this book doesn't return to this patron");

public class PatronHasUnreturnedBooksException() : Exception("this patron has unreturned books");