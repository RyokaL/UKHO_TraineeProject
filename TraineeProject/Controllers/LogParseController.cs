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
            IEnumerable<LogParse> parses = await _parseRepository.GetAllParses();
            return parses.Where(p => !p.Private).ToList();
        }
    }
}
