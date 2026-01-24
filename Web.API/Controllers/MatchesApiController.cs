using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.Challenge)
                .OrderByDescending(m => m.PlayedAt)
                .ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.Challenge)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // POST: api/Matches
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            // Validate: Không cho phép 1 người xuất hiện 2 lần
            var playerIds = new List<int> { match.Player1Id, match.Player2Id };
            if (match.Player3Id.HasValue) playerIds.Add(match.Player3Id.Value);
            if (match.Player4Id.HasValue) playerIds.Add(match.Player4Id.Value);

            if (playerIds.Count != playerIds.Distinct().Count())
            {
                return BadRequest("Không được chọn trùng người chơi trong cùng một trận đấu!");
            }

            match.PlayedAt = DateTime.Now;
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            // Xử lý cập nhật Rank nếu là trận Ranked
            if (match.IsRanked)
            {
                await UpdateRanksAfterMatch(match);
            }

            return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, match);
        }

        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.Id)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task UpdateRanksAfterMatch(Match match)
        {
            // Xác định đội thắng dựa trên tỉ số
            bool team1Wins = match.ScoreTeam1 > match.ScoreTeam2;

            // Team 1: Player1 + Player3
            var player1 = await _context.Members.FindAsync(match.Player1Id);
            if (player1 != null)
            {
                if (team1Wins)
                    player1.RankLevel += 0.1;
                else
                {
                    player1.RankLevel -= 0.1;
                    if (player1.RankLevel < 0) player1.RankLevel = 0;
                }
            }

            if (match.Player3Id.HasValue)
            {
                var player3 = await _context.Members.FindAsync(match.Player3Id.Value);
                if (player3 != null)
                {
                    if (team1Wins)
                        player3.RankLevel += 0.1;
                    else
                    {
                        player3.RankLevel -= 0.1;
                        if (player3.RankLevel < 0) player3.RankLevel = 0;
                    }
                }
            }

            // Team 2: Player2 + Player4
            var player2 = await _context.Members.FindAsync(match.Player2Id);
            if (player2 != null)
            {
                if (!team1Wins)
                    player2.RankLevel += 0.1;
                else
                {
                    player2.RankLevel -= 0.1;
                    if (player2.RankLevel < 0) player2.RankLevel = 0;
                }
            }

            if (match.Player4Id.HasValue)
            {
                var player4 = await _context.Members.FindAsync(match.Player4Id.Value);
                if (player4 != null)
                {
                    if (!team1Wins)
                        player4.RankLevel += 0.1;
                    else
                    {
                        player4.RankLevel -= 0.1;
                        if (player4.RankLevel < 0) player4.RankLevel = 0;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
