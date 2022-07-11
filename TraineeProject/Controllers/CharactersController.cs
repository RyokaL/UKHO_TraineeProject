using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeProject.Models;
using TraineeProject.Models.Request;
using TraineeProject.Models.Views;
using TraineeProject.Repository;

namespace TraineeProject.Controllers
{
    [Route("api/character/")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository<CharacterApiView> _repository;

        public CharactersController(ICharacterRepository<CharacterApiView> repository)
        {
            _repository = repository;
        }

        // GET: api/Character
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterApiView>>> GetCharacters()
        {
            IEnumerable<CharacterApiView> characters = await _repository.GetAllCharacters();
            return characters.ToList();
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<CharacterApiView>>> GetCharactersByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            IEnumerable<CharacterApiView> characters = await _repository.GetCharactersForUserId(userId);
            return characters.ToList();
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterApiView>> GetCharacter(int id)
        {
            var character = await _repository.GetCharacterById(id);

            if (character == null)
            {
                return NotFound();
            }

            //if (character.Private)
            //{
            //    return BadRequest();
            //}

            return character;
        }

        // PUT: api/character/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutCharacterFFXIV(CharacterRequest characterFFXIV)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            return await _repository.AddUserIdToCharacter(characterFFXIV, userId) == null ? BadRequest() : Ok(); 
        }

        [Authorize]
        [HttpPut("user/private")]
        public async Task<IActionResult> UpdateCharacterPrivateFFXIV(CharacterRequest characterFFXIV)
        {
            return await _repository.UpdateCharacterPrivacy(characterFFXIV) == null ? BadRequest() : Ok();
        }

        // POST: api/Character
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
         public async Task<ActionResult<Character>> PostCharacterFFXIV(CharacterRequest characterFFXIV)
         {
            CharacterApiView ret = null;
            try
            {
                ret = await _repository.AddCharacter(characterFFXIV);
            }
            catch(Exception e)
            {
                return BadRequest();
            }

            return ret == null ? new ConflictResult() : CreatedAtAction("GetCharacter", new { id = ret.Id }, ret);
        }

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

        // private bool CharacterFFXIVExists(int id)
        // {
        //     return _context.Character.Any(e => e.Id == id);
        // }
    }
}
