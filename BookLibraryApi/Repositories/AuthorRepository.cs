using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibraryApi.Models;

namespace BookLibraryApi.Repositories
{
    public class AuthorRepository
    {
        private BookLibraryContext db;

        public AuthorRepository(BookLibraryContext db)
        {
            this.db = db;
        }

        public async Task<Author> Add(Author author)
        {
            db.Authors.Add(author);
            await db.SaveChangesAsync();

            return author;
        }

        public async Task<string> Remove(int id)
        {
            Author authorById = db.Authors.Where(a => a.Id == id).First<Author>();
            db.Authors.Remove(authorById);
            await db.SaveChangesAsync();

            return authorById.Name;
        }

        public Author GetById(int id)
        {
            Author authorById = db.Authors.Where(a => a.Id == id).First<Author>();

            return authorById;
        }

        public IEnumerable<Author> GetAll()
        {
            return db.Authors;
        }
    }
}
