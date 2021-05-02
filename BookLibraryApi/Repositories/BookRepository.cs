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

        public async Task<Book> Add(Book book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();

            return book;
        }

        public async Task<string> Remove(int id)
        {
            Book bookById = db.Books.Where(b => b.Id == id).First<Book>();
            db.Books.Remove(bookById);
            await db.SaveChangesAsync();

            return bookById.Name;
        }

        public Book GetById(int id)
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
