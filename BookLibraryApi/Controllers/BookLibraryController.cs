using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using BookLibraryApi.Repositories;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("library")]
    public class BookLibraryController : Controller
    {
        private readonly ILogger<BookLibraryController> _logger;
        private AuthorBookLinkRepository linkRepository;
        public BookLibraryController(ILogger<BookLibraryController> logger, BookLibraryContext db)
        {
            _logger = logger;
            linkRepository = new AuthorBookLinkRepository(db);
        }

        [HttpPost]
        [Route("add")]
        public ObjectResult Add(
            [FromQuery] long authorId,
            [FromQuery] long bookId
        )
        {
            try
            {
                AuthorBookLink link = new AuthorBookLink
                {
                    AuthorId = authorId,
                    BookId = bookId
                };

                linkRepository.Add(link);
            }
            catch(DbUpdateException e)
            {
                return BadRequest("Link already exists");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok($"link book {bookId} to author {authorId} added");
        }

        [HttpDelete]
        [Route("remove")]
        public ObjectResult Remove(
            [FromQuery] long authorId,
            [FromQuery] long bookId
        )
        {
            try
            {
                AuthorBookLink link = linkRepository.Get(bookId, authorId);

                linkRepository.Remove(link);
            }
            catch (InvalidOperationException e)
            {
                if ( e.Message == "Sequence contains no elements" )
                {
                    return NotFound("No such links found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


            return Ok($"link book {bookId} to author {authorId} removed");
        }

        [HttpGet]
        public int GetBooksCountByAuthor([FromQuery] long authorId)
        {
            return linkRepository.GetByAuthor(authorId).Count();
        }
    }
}
