using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibraryApi.Models;

namespace BookLibraryApi.Repositories
{
    public class AuthorBookLinkRepository
    {
        private BookLibraryContext db;

        public AuthorBookLinkRepository(BookLibraryContext db)
        {
            this.db = db;
        }

        public AuthorBookLink Add(AuthorBookLink link)
        {

            db.AuthorBookLinks.Add(link);
            db.SaveChanges();

            return link;
        }

        public void Remove(AuthorBookLink link)
        {
            db.AuthorBookLinks.Remove(link);
            db.SaveChanges();
        }

        public void RemoveBookLinks(long bookId)
        {
            db.AuthorBookLinks.RemoveRange(db.AuthorBookLinks.Where(l => l.BookId == bookId));
            db.SaveChanges();
        }

        public void RemoveAuthorLinks(long authorId)
        {
            db.AuthorBookLinks.RemoveRange(db.AuthorBookLinks.Where(l => l.AuthorId == authorId));
            db.SaveChanges();
        }

        public AuthorBookLink Get(long bookId, long authorId)
        {
           return db.AuthorBookLinks
                    .Where(l => l.AuthorId == authorId && l.BookId == bookId)
                    .First<AuthorBookLink>();
        }

        public IEnumerable<AuthorBookLink> GetByAuthor(long authorId)
        {
            return db.AuthorBookLinks.Where(l => l.AuthorId == authorId);
        }
    }
}
