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

        public async Task<AuthorBookLink> Add(AuthorBookLink link)
        {

            db.AuthorBookLinks.Add(link);
            await db.SaveChangesAsync();

            return link;
        }

        public async Task Remove(AuthorBookLink link)
        {
            db.AuthorBookLinks.Remove(link);
            await db.SaveChangesAsync();
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
