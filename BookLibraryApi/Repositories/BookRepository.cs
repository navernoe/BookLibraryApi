using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibraryApi.Models;

namespace BookLibraryApi.Repositories
{
    public class BookRepository
    {
        private BookLibraryContext db;

        public BookRepository(BookLibraryContext db)
        {
            this.db = db;
        }

        public Book Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return book;
        }

        public string Remove(long id)
        {
            Book bookById = db.Books.Where(b => b.Id == id).First<Book>();
            db.Books.Remove(bookById);
            db.SaveChanges();

            return bookById.Name;
        }

        public void UpdateName(Book book, string name)
        {
            book.Name = name;
            db.Books.Update(book);
            db.SaveChanges();
        }

        public Book GetById(long id)
        {
            Book bookById = db.Books.Where(b => b.Id == id).First<Book>();

            return bookById;
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }
    }
}
