using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Client.Data;
using Web.Client.Models;

namespace Web.Client.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.Challenge)
                .OrderByDescending(m => m.PlayedAt)
                .ToListAsync();
            ViewBag.Members = await _context.Members.Where(m => m.Status == MemberStatus.Active).ToListAsync();
            return View(matches);
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            var members = _context.Members.Where(m => m.Status == MemberStatus.Active).ToList();
            ViewData["Player1Id"] = new SelectList(members, "Id", "FullName");
            ViewData["Player2Id"] = new SelectList(members, "Id", "FullName");
            ViewData["Player3Id"] = new SelectList(members, "Id", "FullName");
            ViewData["Player4Id"] = new SelectList(members, "Id", "FullName");
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChallengeId,Format,IsRanked,Player1Id,Player2Id,Player3Id,Player4Id,ScoreTeam1,ScoreTeam2,Notes")] Match match)
        {
            // Validate: Không cho phép 1 người xuất hiện 2 lần
            var playerIds = new List<int> { match.Player1Id, match.Player2Id };
            if (match.Player3Id.HasValue) playerIds.Add(match.Player3Id.Value);
            if (match.Player4Id.HasValue) playerIds.Add(match.Player4Id.Value);

            if (playerIds.Count != playerIds.Distinct().Count())
            {
                TempData["Error"] = "Không được chọn trùng người chơi trong cùng một trận đấu!";
                var members = _context.Members.Where(m => m.Status == MemberStatus.Active).ToList();
                ViewData["Player1Id"] = new SelectList(members, "Id", "FullName");
                ViewData["Player2Id"] = new SelectList(members, "Id", "FullName");
                ViewData["Player3Id"] = new SelectList(members, "Id", "FullName");
                ViewData["Player4Id"] = new SelectList(members, "Id", "FullName");
                return View(match);
            }

            if (ModelState.IsValid)
            {
                match.PlayedAt = DateTime.Now;
                _context.Add(match);
                await _context.SaveChangesAsync();

                // Xử lý cập nhật Rank nếu là trận Ranked
                if (match.IsRanked)
                {
                    await UpdateRanksAfterMatch(match);
                }

                return RedirectToAction(nameof(Index));
            }

            var membersList = _context.Members.Where(m => m.Status == MemberStatus.Active).ToList();
            ViewData["Player1Id"] = new SelectList(membersList, "Id", "FullName", match.Player1Id);
            ViewData["Player2Id"] = new SelectList(membersList, "Id", "FullName", match.Player2Id);
            ViewData["Player3Id"] = new SelectList(membersList, "Id", "FullName", match.Player3Id);
            ViewData["Player4Id"] = new SelectList(membersList, "Id", "FullName", match.Player4Id);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
