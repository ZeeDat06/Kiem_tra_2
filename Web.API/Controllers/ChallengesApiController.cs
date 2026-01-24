using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChallengesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Challenges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges()
        {
            return await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        // GET: api/Challenges/Open
        [HttpGet("Open")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetOpenChallenges()
        {
            return await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .Where(c => c.Status == ChallengeStatus.Open)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        // GET: api/Challenges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallenge(int id)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            return challenge;
        }

        // GET: api/Challenges/5/Pot - Tính tổng giải thưởng Mini-game
        [HttpGet("{id}/Pot")]
        public async Task<ActionResult<decimal>> GetChallengePot(int id)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            var pot = challenge.Participants.Count * challenge.EntryFee;
            return pot;
        }

        // POST: api/Challenges
        [HttpPost]
        public async Task<ActionResult<Challenge>> PostChallenge(Challenge challenge)
        {
            challenge.Status = ChallengeStatus.Open;
            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge);
        }

        // PUT: api/Challenges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallenge(int id, Challenge challenge)
        {
            if (id != challenge.Id)
            {
                return BadRequest();
            }

            _context.Entry(challenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(id))
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

        // PUT: api/Challenges/5/Close
        [HttpPut("{id}/Close")]
        public async Task<IActionResult> CloseChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            challenge.Status = ChallengeStatus.Closed;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Challenges/5/Complete
        [HttpPut("{id}/Complete")]
        public async Task<IActionResult> CompleteChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            challenge.Status = ChallengeStatus.Completed;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Challenges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            _context.Challenges.Remove(challenge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Challenges/5/Join/3 - Member tham gia Challenge
        [HttpPost("{challengeId}/Join/{memberId}")]
        public async Task<IActionResult> JoinChallenge(int challengeId, int memberId)
        {
            var challenge = await _context.Challenges.FindAsync(challengeId);
            if (challenge == null || challenge.Status != ChallengeStatus.Open)
            {
                return BadRequest("Kèo đã đóng hoặc không tồn tại.");
            }

            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
            {
                return NotFound("Member không tồn tại.");
            }

            var existingParticipant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ChallengeId == challengeId && p.MemberId == memberId);
            if (existingParticipant != null)
            {
                return BadRequest("Member đã tham gia kèo này.");
            }

            var participant = new Participant
            {
                ChallengeId = challengeId,
                MemberId = memberId
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ChallengeExists(int id)
        {
            return _context.Challenges.Any(e => e.Id == id);
        }
    }
}
