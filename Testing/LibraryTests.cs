using Library;

namespace Testing;

public class LibraryTests
{
    private Library.Library _library;
    private Book _book;
    private Patron _p1;
    private Patron _p2;
    
    
    [SetUp]
    public void Setup()
    {
        _library = new Library.Library();
        _book = new Book("how to code", "manish", null);
        _p1 = new Patron("rose");
        _p2 = new Patron("eirik");
        
        _library.AddBook(_book);
        _library.AddPatron(_p1);
        _library.AddPatron(_p2);
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Check_That_BorrowBook_AddsTo_PatronBorrowedBooks(int idx)
    {
        if (idx == 0) _library.BorrowBook(_p1, _book);
        else _p1.BorrowBook(_book);
        Assert.That(_p1.BorrowedBooks, Does.Contain(_book));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Check_That_BorrowedBook_Cannot_Be_Borrowed(int idx)
    {
        if (idx == 0) _library.BorrowBook(_p1, _book);
        else _p1.BorrowBook(_book);
        Assert.That(_book.IsAvailable, Is.False);
        Assert.Throws<BookNotAvailableException>(()=>_book.Borrow(_p2));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Check_That_BorrowedBook_Can_Be_Returned(int idx)
    {
        if (idx == 0) _library.BorrowBook(_p1, _book);
        else _p1.BorrowBook(_book);
        Assert.That(_book.IsAvailable, Is.False);
        if (idx == 0) _library.ReturnBook(_p1, _book);
        else _p1.ReturnBook(_book);
        Assert.That(_book.IsAvailable, Is.True);
    }

    [Test]
    public void Check_That_AvailableBook_Cannot_Be_Returned()
    {
        Assert.That(_book.IsAvailable, Is.True);
        Assert.Throws<BookAlreadyAvailableException>(()=>_book.Return());
    }

    [Test]
    public void Check_That_RegisteredPatron_Cannot_Register()
    {
        var library2 = new Library.Library();
        Assert.Throws<PatronBelongsToLibraryException>(() => library2.AddPatron(_p1));
    }

    [Test]
    public void Check_That_RegisteredPatron_Can_Leave()
    {
        _library.RemovePatron(_p1);
        Assert.That(_p1.InLibrary, Is.False);
    }

    [Test]
    public void Check_That_RegisteredPatron_WithBooks_Cannot_Leave()
    {
        _p1.BorrowBook(_book);
        Assert.Throws<PatronHasUnreturnedBooksException>(() => _library.RemovePatron(_p1));
    }

    [Test]
    public void Check_That_UnregisteredPatron_Cannot_Leave()
    {
        _library.RemovePatron(_p1);
        Assert.Throws<PatronNotInThisLibraryException>(() => _library.RemovePatron(_p1));
    }

    [TestCase("manish", ExpectedResult = 1)]
    [TestCase("rose", ExpectedResult = 0)]
    public int Check_That_GetBookByAuthor_Resolves_Correctly(string author)
    {
        return _library.GetBookByAuthor(author).Count;
    }

    [TestCase("how to code", ExpectedResult = 1)]
    [TestCase("how to cook", ExpectedResult = 0)]
    public int Check_That_GetBookByTitle_Resolves_Correctly(string title)
    {
        return _library.GetBookByTitle(title).Count;
    }

    [Test]
    public void Check_That_PatronCount_Is_Correct()
    {
        Assert.That(_library.Patrons, Has.Count.EqualTo(2));
    }

    [Test]
    public void Check_That_AvailableBook_Can_Remove()
    {
        _library.RemoveBook(_book);
        Assert.That(_library.GetAllBooks(), Is.Empty);
    }

    [Test]
    public void Check_That_UnavailableBook_Cannot_Remove()
    {
        _book.Borrow(_p1);
        Assert.Throws<BookNotAvailableException>(() => _library.RemoveBook(_book));
    }
}