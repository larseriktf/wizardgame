using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizardGame.DataAccess;
using WizardGame.Model;

namespace WizardGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDataController : ControllerBase
    {
        private readonly GameContext _context;

        public GameDataController(GameContext context)
        {
            _context = context;
        }

        // GET: api/GameData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameData>>> GetAllGamesAsync() =>
            await _context.GameStatistics.Include(g => g.Player).ToListAsync();

        // GET: api/GameData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameData>> GetSpecificGameAsync(int id)
        {
            var gameStatistic = await _context.GameStatistics.FindAsync(id);

            if (gameStatistic == null)
            {
                return NotFound();
            }

            return gameStatistic;
        }

        // GET: api/GameData/Player/5
        [HttpGet("Player/{id}")]
        public async Task<ActionResult<IEnumerable<GameData>>> GetPlayerGamesAsync(int id) =>
            await _context.GameStatistics
            .Include(g => g.Player)
            .Where(g => g.PlayerId == id)
            .ToListAsync();

        // @Todo: Make this work
        // GET: api/GameStatistics/Top
        //[HttpGet("Top")]
        //public async Task<ActionResult<IEnumerable<GameStatistic>>> GetTopGamesAsync()
        //{
        //    IEnumerable<GameStatistic> list = _context.GameStatistics.Include(g => g.PlayerProfile).ToListAsync().Result;
        //}


        // PUT: api/GameData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameStatisticAsync(int id, GameData gameStatistic)
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

        // POST: api/GameData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameData>> PostGameStatisticAsync(GameData gameStatistic)
        {
            Player player = _context.PlayerProfiles
                .Include(p => p.GameData)
                .Single(p => p.Id == gameStatistic.PlayerId);

            gameStatistic.Player = player;

            _context.GameStatistics.Add(gameStatistic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameStatistic", new { id = gameStatistic.Id }, gameStatistic);
        }

        // DELETE: api/GameData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameData>> DeleteGameStatisticAsync(int id)
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
