using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TraineeProject.Models;
using TraineeProject.Repository;

namespace TraineeProject.Controllers
{
    [Route("/api/parse/")]
    [ApiController]
    public class LogParseController
    {
        private readonly IParseRepository _parseRepository;

        public LogParseController(IParseRepository parseRepository)
        {
            _parseRepository = parseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogParse>>> GetParses()
        {
            var parses = await _parseRepository.GetAllParses();
            return parses.Where(p => !p.Private).ToList();
        }

        [Route("character/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogParse>>> GetParsesByCharacterId(int id)
        {
            var parses = await _parseRepository.GetAllParsesByCharacterId(id);
            return parses.ToList();
        }

    }
}
