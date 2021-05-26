using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizardGame.DataAccess;
using WizardGame.Model;

namespace WizardGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatisticsController : ControllerBase
    {
        private readonly GameContext _context;

        public GameStatisticsController(GameContext context)
        {
            _context = context;
        }

        // GET: api/GameStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameStatistic>>> GetGameStatisticsAsync() =>
            await _context.GameStatistics.Include(g => g.PlayerProfile).ToListAsync();

        // GET: api/GameStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameStatistic>> GetGameStatisticAsync(int id)
        {
            var gameStatistic = await _context.GameStatistics.FindAsync(id);

            if (gameStatistic == null)
            {
                return NotFound();
            }

            return gameStatistic;
        }

        // PUT: api/GameStatistics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameStatisticAsync(int id, GameStatistic gameStatistic)
        {
            if (id != gameStatistic.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameStatistic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStatisticExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameStatistics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameStatistic>> PostGameStatisticAsync(GameStatistic gameStatistic)
        {
            _context.GameStatistics.Add(gameStatistic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameStatistic", new { id = gameStatistic.Id }, gameStatistic);
        }

        // DELETE: api/GameStatistics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameStatistic>> DeleteGameStatisticAsync(int id)
        {
            var gameStatistic = await _context.GameStatistics.FindAsync(id);
            if (gameStatistic == null)
            {
                return NotFound();
            }

            _context.GameStatistics.Remove(gameStatistic);
            await _context.SaveChangesAsync();

            return gameStatistic;
        }

        private bool GameStatisticExists(int id)
        {
            return _context.GameStatistics.Any(e => e.Id == id);
        }
    }
}
