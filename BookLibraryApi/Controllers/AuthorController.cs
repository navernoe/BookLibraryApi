using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibraryApi.Repositories;
using BookLibraryApi.Models;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private AuthorRepository authorRepository;

        public AuthorController(ILogger<AuthorController> logger, BookLibraryContext db)
        {
            _logger = logger;
            authorRepository = new AuthorRepository(db);
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Author> GetAll()
        {
            return authorRepository.GetAll();
        }

        [HttpGet]
        public Author Get([FromQuery] int id)
        {
            return authorRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ObjectResult> Add(string authorName)
        {
            if (String.IsNullOrEmpty(authorName))
            {
                return BadRequest("Wrong author's name");
            }

            Author newAuthor = await authorRepository.Add
                (
                     new Author { Name = authorName }
                );

            return Created(authorName, newAuthor);
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<ObjectResult> Remove(int id)
        {
            try
            {
                string removedAuthorName = await authorRepository.Remove(id);

                return Ok($"Author {removedAuthorName} with id = {id} was removed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
