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

        public BookController(ILogger<BookController> logger, BookLibraryContext db)
        {
            _logger = logger;
            bookRepository = new BookRepository(db);
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Book> GetAll()
        {
            return bookRepository.GetAll();
        }

        [HttpGet]
        public Book Get([FromQuery] int id)
        {
            return bookRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ObjectResult> Add(string bookName)
        {
            if (String.IsNullOrEmpty(bookName))
            {
                return BadRequest("Wrong book's name");
            }

            Book newBook = await bookRepository.Add
                (
                     new Book { Name = bookName }
                );

            return Created(bookName, newBook);
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<ObjectResult> Remove(int id)
        {
            try
            {
                string removedBookName = await bookRepository.Remove(id);

                return Ok($"Book {removedBookName} with id = {id} was removed");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
