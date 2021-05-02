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

        public Author Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();

            return author;
        }

        public string Remove(long id)
        {
            Author authorById = db.Authors.Where(a => a.Id == id).First<Author>();
            db.Authors.Remove(authorById);
            db.SaveChanges();

            return authorById.Name;
        }

        public void UpdateName(Author author, string name)
        {
            author.Name = name;
            db.Authors.Update(author);
            db.SaveChanges();
        }

        public Author GetById(long id)
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
