using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeProject.Database;
using TraineeProject.Models;

namespace TraineeProject.Controllers
{
    [Route("api/character/")]
    [ApiController]
    public class FFXIVCharactersController : ControllerBase
    {
        private readonly LogContext _context;

        public FFXIVCharactersController(LogContext context)
        {
            _context = context;
        }

        // GET: api/Character
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetFFXIVCharacters()
        {
            return await _context.Character.ToListAsync();
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterFFXIV(int id)
        {
            var character = await _context.Character.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/character/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutCharacterFFXIV(int id, Character characterFFXIV)
        {
            if (id != characterFFXIV.Id)
            {
                return BadRequest();
            }

            _context.Entry(characterFFXIV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterFFXIVExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Character
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Character>> PostCharacterFFXIV(Character characterFFXIV)
        // {
        //     _context.Character.Add(characterFFXIV);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetCharacterFFXIV", new { id = characterFFXIV.Id }, characterFFXIV);
        // }

        // DELETE: api/Character/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacterFFXIV(int id)
        {
            var characterFFXIV = await _context.Character.FindAsync(id);
            if (characterFFXIV == null)
            {
                return NotFound();
            }

            _context.Character.Remove(characterFFXIV);
            await _context.SaveChangesAsync();

            return characterFFXIV;
        }*/

        private bool CharacterFFXIVExists(int id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
