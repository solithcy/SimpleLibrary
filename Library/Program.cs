namespace Library;

public static class Program
{
    public static void Main(string[] args)
    {
        var library = new Library();
        var library2 = new Library();
        var book = new Book("how to code", "manish", null);
        var patron1 = new Patron("rose");
        var patron2 = new Patron("eirik");
        
        library.AddBook(book);
        library.AddPatron(patron1);
        patron1.BorrowBook(book);
        Console.WriteLine($"{patron1} owns: " + string.Join(" | ", patron1.BorrowedBooks));

        try
        {
            library.RemovePatron(patron1);
            throw new Exception("patron could leave without returning books");
        }
        catch (PatronHasUnreturnedBooksException)
        {
            Console.WriteLine("patron couldn't leave without returning books");
        }

        try
        {
            patron1.BorrowBook(book);
            throw new Exception("patron borrow unavailable book");
        }
        catch (BookNotAvailableException)
        {
            Console.WriteLine("book couldn't be borrowed as it isn't available");
        }
        
        try
        {
            patron2.ReturnBook(book);
        }
        catch (PatronDoesNotHaveLibraryException)
        {
            Console.WriteLine("this patron doesn't have a library");
        }

        library2.AddPatron(patron2);
        
        try
        {
            library.ReturnBook(patron2, book);
        }
        catch (PatronNotInThisLibraryException)
        {
            Console.WriteLine("this patron is in a different library");
        }

        library2.RemovePatron(patron2);
        library.AddPatron(patron2);
        
        try
        {
            patron2.ReturnBook(book);
        }
        catch (PatronDoesNotOwnBookException)
        {
            Console.WriteLine("book couldn't be returned by someone else");
        }
        
        patron1.ReturnBook(patron1.BorrowedBooks[0]);
        Console.WriteLine($"book is available: {book.IsAvailable}");
        
        library.RemovePatron(patron1);
        Console.WriteLine($"library has {library.PatronCount} patrons");
    }
}