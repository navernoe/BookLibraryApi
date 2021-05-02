using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookLibraryApi.Repositories;
using BookLibraryApi.Models;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private BookRepository bookRepository;
        private AuthorBookLinkRepository linkRepository;

        public BookController(ILogger<BookController> logger, BookLibraryContext db)
        {
            _logger = logger;
            bookRepository = new BookRepository(db);
            linkRepository = new AuthorBookLinkRepository(db);
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Book> GetAll()
        {
            return bookRepository.GetAll();
        }

        [HttpGet]
        public Book Get([FromQuery] long id)
        {
            return bookRepository.GetById(id);
        }

        [HttpPost]
        public ObjectResult Add(string bookName)
        {
            if (String.IsNullOrEmpty(bookName))
            {
                return BadRequest("Wrong book's name");
            }

            Book newBook = bookRepository.Add
                (
                     new Book { Name = bookName }
                );

            return Created(bookName, newBook);
        }

        [HttpPost]
        [Route("update")]
        public ObjectResult Update(long bookId, string name)
        {
            try
            {
                Book book = bookRepository.GetById(bookId);
                string oldName = book.Name;
                bookRepository.UpdateName(book, name);

                return Ok($"Author's name {oldName} was update to {book.Name}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public ObjectResult Remove(long id)
        {
            try
            {
                linkRepository.RemoveBookLinks(id);
                string removedBookName = bookRepository.Remove(id);

                return Ok($"Book {removedBookName} with id = {id} was removed");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
