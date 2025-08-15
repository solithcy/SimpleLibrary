# Library Project

Library Code Kata

Kata: Library Management System
Problem Statement
You are tasked with creating a simple library management system that allows users to manage books and patrons. The system should support the following functionalities:

1. Book Management:
    - Add a new book to the library.
    - Remove a book from the library.
    - Search for a book by title or author (exact string).
    - List all available books.

2. Patron Management:
    - Add a new patron to the library.
    - Remove a patron from the library.
    - List all patrons.

3. Borrowing and Returning Books:
    - Allow a patron to borrow a book.
    - Allow a patron to return a book.
    - Ensure that a book can only be borrowed if it is available.
    - Keep track of which patron has borrowed which book.

You will need to submit your code along with a thorough test suite and a readme file that explains how to run your code and tests.


# Class Structure

You can structure your classes as follows:
1. Book Class:
    - Properties: `Title`, `Author`, `IsAvailable`
    - Methods: `Borrow()`, `Return()`

2. Patron Class:
    - Properties: `Name`, `BorrowedBooks` (a list of books)
    - Methods: `BorrowBook(Book book)`, `ReturnBook(Book book)`

3. Library Class:
    - Properties: `Books` (a list of books), `Patrons` (a list of patrons)
    - Methods:
        - `AddBook(Book book)`
        - `RemoveBook(Book book)`
        - `GetBookByAuthor(string Author)`
		- `GetBookByTitle(string title)`
        - `GetAllBooks()`
        - `AddPatron(Patron patron)`
        - `RemovePatron(Patron patron)`
        - `BorrowBook(Patron patron, Book book)`
        - `ReturnBook(Patron patron, Book book)`
