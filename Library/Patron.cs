namespace Library;

public class Patron
{
    public readonly string Name;
    public List<Book> BorrowedBooks => _library is null ? [] : _library.GetBooksByPatron(this);

    private Library? _library = null;
    public bool InLibrary => _library is not null;

    public Patron(string name)
    {
        Name = name;
    }

    public void BorrowBook(Book book)
    {
        if (!InLibrary) throw new PatronDoesNotHaveLibraryException();
        _library!.BorrowBook(this, book);
    }

    public void ReturnBook(Book book)
    {
        if (!InLibrary) throw new PatronDoesNotHaveLibraryException();
        _library!.ReturnBook(this, book);
    }

    internal void RegisterLibrary(Library lib)
    {
        if (InLibrary)
        {
            throw new PatronBelongsToLibraryException();
        }

        _library = lib;
    }

    internal void UnregisterLibrary()
    {
        if (!InLibrary) throw new PatronDoesNotHaveLibraryException();
        _library = null;
    }

    public override string ToString()
    {
        return Name;
    }
}

public class PatronBelongsToLibraryException() : Exception("this patron already belongs to a library");

public class PatronDoesNotHaveLibraryException() : Exception("this patron does not have a library");
