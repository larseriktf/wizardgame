using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizardGame.DataAccess;
using WizardGame.Model;

namespace WizardGame.Api.Controllers
{
    // [controller] will return PlayerProfiles, because this controller is named PlayerProfilesController
    // Meaning, that the path for this controller is api/PlayerProfiles
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameContext _context;

        public PlayersController(GameContext context) => _context = context;

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayerProfilesAsync() =>
            await _context.PlayerProfiles.Include(p => p.GameData).ToListAsync();

        // GET: api/Players/Selected
        [HttpGet]
        [Route("Selected")]
        public async Task<ActionResult<Player>> GetSelectedPlayerProfilesAsync() =>
            await _context.PlayerProfiles.Include(p => p.GameData).SingleAsync(p => p.IsSelected);

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayerProfileAsync(int id)
        {
            var playerProfile = await _context.PlayerProfiles.FindAsync(id);

            if (playerProfile == null)
            {
                return NotFound();
            }

            return playerProfile;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerProfileAsync(int id, Player playerProfile)
        {
            if (id != playerProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerProfileExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayerProfileAsync(Player playerProfile)
        {
            _context.PlayerProfiles.Add(playerProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerProfile", new { id = playerProfile.Id }, playerProfile);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayerProfileAsync(int id)
        {
            var playerProfile = await _context.PlayerProfiles.FindAsync(id);
            if (playerProfile == null)
            {
                return NotFound();
            }

            _context.PlayerProfiles.Remove(playerProfile);
            await _context.SaveChangesAsync();

            return playerProfile;
        }

        private bool PlayerProfileExists(int id) => _context.PlayerProfiles.Any(e => e.Id == id);
    }
}
