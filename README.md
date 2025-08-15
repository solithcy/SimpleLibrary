# SimpleLibrary

A simple library/

## Prerequisites

- Git
- Dotnet SDK & Runtime

## Usage

This library exposes a few classes:

- `Library`
- `Book`
- `Patron`

Some simple usage can be found in [`Program.cs`](./Library/Program.cs), but I'll add some
code snippets here:
```csharp

var library = new Library();

var book = new Book("how to code", "manish", null);
//                   title          author   reader (null most of the time)

var patron = new Patron("rose");
//                       name

library.AddBook(book);
library.AddPatron(patron);

book.Borrow(patron); // this book is now registered to this patron. this can be viewable at:

Console.WriteLine(book.Reader);
Console.WriteLine(patron.BorrowedBooks[0]);

// books cannot be borrowed twice.
book.Return();

// search methods
List<Book> searchResults = library.GetBookByAuthor("manish");
List<Book> searchResults2 = library.GetBookByTitle("how to code");

library.RemovePatron(patron);
library.RemoveBook(book);

// library.Explode();
// just kidding..

```

## Testing

To run the tests, from the project root run:

```bash
$ dotnet test # no need to cd into Testing
```

This project has 84% test coverage, excluding the demo code in [`Program.cs`](./Library/Program.cs).

## Deviations

This project deviates from the [brief](./Brief.md) in a couple of ways.

- Book.Borrow() takes a Patron as an argument.
- Library.Books and Library.Patrons are not publicly available lists. Instead, Library.GetAllBooks() can be used to get all the books, and Library.Patrons points to a *clone* of the underlying _patrons list. This is so the lists cannot be modified directly and the respective add/remove methods must be used.
 
