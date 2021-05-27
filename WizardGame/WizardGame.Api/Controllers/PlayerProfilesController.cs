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
    public class PlayerProfilesController : ControllerBase
    {
        private readonly GameContext _context;

        public PlayerProfilesController(GameContext context)
        {
            _context = context;
        }

        // GET: api/PlayerProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerProfile>>> GetPlayerProfilesAsync() =>
            await _context.PlayerProfiles.Include(p => p.GameStatistics).ToListAsync();

        // GET: api/PlayerProfiles/Selected
        [HttpGet]
        [Route("Selected")]
        public async Task<ActionResult<PlayerProfile>> GetSelectedPlayerProfilesAsync() =>
            await _context.PlayerProfiles.Include(p => p.GameStatistics).SingleAsync(p => p.IsSelected);

        // GET: api/PlayerProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerProfile>> GetPlayerProfileAsync(int id)
        {
            var playerProfile = await _context.PlayerProfiles.FindAsync(id);

            if (playerProfile == null)
            {
                return NotFound();
            }

            return playerProfile;
        }

        // PUT: api/PlayerProfiles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerProfileAsync(int id, PlayerProfile playerProfile)
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

        // POST: api/PlayerProfiles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerProfile>> PostPlayerProfileAsync(PlayerProfile playerProfile)
        {
            _context.PlayerProfiles.Add(playerProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerProfile", new { id = playerProfile.Id }, playerProfile);
        }

        // DELETE: api/PlayerProfiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerProfile>> DeletePlayerProfileAsync(int id)
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

        private bool PlayerProfileExists(int id)
        {
            return _context.PlayerProfiles.Any(e => e.Id == id);
        }
    }
}
