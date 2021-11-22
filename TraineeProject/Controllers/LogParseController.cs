using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraineeProject.Models;
using TraineeProject.Models.Views;
using TraineeProject.Repository;

namespace TraineeProject.Controllers
{
    [Route("/api/parse/")]
    [ApiController]
    public class LogParseController
    {
        private readonly IParseRepository<LogParseApiView> _parseRepository;

        public LogParseController(IParseRepository<LogParseApiView> parseRepository)
        {
            _parseRepository = parseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogParseApiView>>> GetParses([FromQuery] string characterName = "")
        {
            var parses = await _parseRepository.GetAllParses(characterName: characterName);
            return new OkObjectResult(parses);
        }

        [Route("character/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogParseApiView>>> GetParsesByCharacterId(int id)
        {
            var parses = await _parseRepository.GetAllParsesByCharacterId(id);
            return new OkObjectResult(parses);
        }

    }
}
