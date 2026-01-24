using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Client.Data;
using Web.Client.Models;

namespace Web.Client.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Calculate balance
        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .ToListAsync();

        decimal balance = 0;
        foreach (var t in transactions)
        {
            if (t.Category?.Type == TransactionType.Thu)
                balance += t.Amount;
            else
                balance -= t.Amount;
        }
        ViewBag.Balance = balance;
        ViewBag.IsNegative = balance < 0;

        // Open challenges count
        ViewBag.OpenChallenges = await _context.Challenges.CountAsync(c => c.Status == ChallengeStatus.Open);

        // Top 5 members
        ViewBag.TopMembers = await _context.Members
            .Where(m => m.Status == MemberStatus.Active)
            .OrderByDescending(m => m.RankLevel)
            .Take(5)
            .ToListAsync();

        // Latest news
        ViewBag.LatestNews = await _context.News
            .Include(n => n.Author)
            .OrderByDescending(n => n.IsPinned)
            .ThenByDescending(n => n.PublishedAt)
            .Take(5)
            .ToListAsync();

        // Open challenges for display
        ViewBag.OpenChallengesList = await _context.Challenges
            .Include(c => c.Creator)
            .Include(c => c.Participants)
            .Where(c => c.Status == ChallengeStatus.Open)
            .OrderByDescending(c => c.Id)
            .Take(6)
            .ToListAsync();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
