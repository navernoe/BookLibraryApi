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
        private AuthorBookLinkRepository linkRepository;

        public AuthorController(ILogger<AuthorController> logger, BookLibraryContext db)
        {
            _logger = logger;
            authorRepository = new AuthorRepository(db);
            linkRepository = new AuthorBookLinkRepository(db);
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
        public ObjectResult Add(string authorName)
        {
            if (String.IsNullOrEmpty(authorName))
            {
                return BadRequest("Wrong author's name");
            }

            Author newAuthor = authorRepository.Add
                (
                     new Author { Name = authorName }
                );

            return Created(authorName, newAuthor);
        }

        [HttpPost]
        [Route("update")]
        public ObjectResult Update(long authorId, string name)
        {
            try
            {
                Author author = authorRepository.GetById(authorId);
                string oldName = author.Name;
                authorRepository.UpdateName(author, name);

                return Ok($"Author's name {oldName} was update to {author.Name}");
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
                linkRepository.RemoveAuthorLinks(id);
                string removedAuthorName = authorRepository.Remove(id);

                return Ok($"Author {removedAuthorName} with id = {id} was removed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
