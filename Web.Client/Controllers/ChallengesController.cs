using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Client.Data;
using Web.Client.Models;

namespace Web.Client.Controllers
{
    public class ChallengesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChallengesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Challenges
        public async Task<IActionResult> Index()
        {
            var challenges = await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .OrderByDescending(c => c.Id)
                .ToListAsync();
            ViewBag.Members = await _context.Members.Where(m => m.Status == MemberStatus.Active).ToListAsync();
            return View(challenges);
        }

        // GET: Challenges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.Participants)
                    .ThenInclude(p => p.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            // Load members who haven't joined yet
            var participantIds = challenge.Participants.Select(p => p.MemberId).ToList();
            ViewBag.Members = await _context.Members
                .Where(m => m.Status == MemberStatus.Active && !participantIds.Contains(m.Id))
                .ToListAsync();

            return View(challenge);
        }

        // GET: Challenges/Create - Redirect to Index
        public IActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: Challenges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatorId,Title,Description,Mode,RewardDescription,EntryFee")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                challenge.Status = ChallengeStatus.Open;
                _context.Add(challenge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Challenges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Members.Where(m => m.Status == MemberStatus.Active), "Id", "FullName", challenge.CreatorId);
            return View(challenge);
        }

        // POST: Challenges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatorId,Title,Description,Mode,RewardDescription,EntryFee,Status")] Challenge challenge)
        {
            if (id != challenge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challenge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengeExists(challenge.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Members.Where(m => m.Status == MemberStatus.Active), "Id", "FullName", challenge.CreatorId);
            return View(challenge);
        }

        // POST: Challenges/Join/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int challengeId, int memberId)
        {
            var challenge = await _context.Challenges.FindAsync(challengeId);
            if (challenge == null || challenge.Status != ChallengeStatus.Open)
            {
                TempData["Error"] = "Kèo đã đóng hoặc không tồn tại.";
                return RedirectToAction(nameof(Details), new { id = challengeId });
            }

            var existingParticipant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ChallengeId == challengeId && p.MemberId == memberId);
            if (existingParticipant != null)
            {
                TempData["Error"] = "Bạn đã tham gia kèo này.";
                return RedirectToAction(nameof(Details), new { id = challengeId });
            }

            var participant = new Participant
            {
                ChallengeId = challengeId,
                MemberId = memberId
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tham gia thành công!";
            return RedirectToAction(nameof(Details), new { id = challengeId });
        }

        // POST: Challenges/Close/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                challenge.Status = ChallengeStatus.Closed;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Challenges/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                challenge.Status = ChallengeStatus.Completed;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Challenges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenges
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }

            return View(challenge);
        }

        // POST: Challenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                _context.Challenges.Remove(challenge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengeExists(int id)
        {
            return _context.Challenges.Any(e => e.Id == id);
        }
    }
}
