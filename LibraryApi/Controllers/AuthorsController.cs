﻿using LibraryApp.DataAccess;
using LibraryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _db;

        public AuthorsController(LibraryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Authors>> GetAll()
        {
            return await _db.Authors.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Authors author)
        {
            await _db.Authors.AddAsync(author);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(Authors), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Authors author)
        {
            if (id != author.Id)
                return BadRequest();

            _db.Entry(author).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorToDelete = await _db.Authors.FindAsync(id);

            if (authorToDelete == null)
                return NotFound();

            _db.Authors.Remove(authorToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
