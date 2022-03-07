using System;
using System.Linq;

namespace Bookstore.Models
{
    public interface IBookstoreRepository // this must be an INTERFACE instead of a class
    {
        IQueryable<Book> Books { get; }

        public void SaveBook(Book b);
        public void CreateBook(Book b);
        public void DeleteBook(Book b);
    }
}
