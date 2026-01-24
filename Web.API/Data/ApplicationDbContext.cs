using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.API.Models;

namespace Web.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Member relationships
            modelBuilder.Entity<Member>()
                .HasOne(m => m.IdentityUser)
                .WithMany()
                .HasForeignKey(m => m.IdentityUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Challenge - Creator relationship
            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.Creator)
                .WithMany(m => m.CreatedChallenges)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Match relationships
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player1)
                .WithMany()
                .HasForeignKey(m => m.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player2)
                .WithMany()
                .HasForeignKey(m => m.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player3)
                .WithMany()
                .HasForeignKey(m => m.Player3Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player4)
                .WithMany()
                .HasForeignKey(m => m.Player4Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Challenge)
                .WithMany(c => c.Matches)
                .HasForeignKey(m => m.ChallengeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure News - Author relationship
            modelBuilder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany()
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Participant relationships
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Challenge)
                .WithMany(c => c.Participants)
                .HasForeignKey(p => p.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Member)
                .WithMany(m => m.Participants)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Booking relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Transaction - Category relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure default values
            modelBuilder.Entity<Member>()
                .Property(m => m.RankLevel)
                .HasDefaultValue(1.0);

            modelBuilder.Entity<Challenge>()
                .Property(c => c.EntryFee)
                .HasDefaultValue(0);

            // Seed Data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Members
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    FullName = "Nguyễn Văn An",
                    DOB = new DateTime(1990, 5, 15),
                    PhoneNumber = "0901234567",
                    JoinDate = new DateTime(2024, 1, 1),
                    RankLevel = 3.5,
                    Status = MemberStatus.Active
                },
                new Member
                {
                    Id = 2,
                    FullName = "Trần Thị Bình",
                    DOB = new DateTime(1995, 8, 20),
                    PhoneNumber = "0912345678",
                    JoinDate = new DateTime(2024, 2, 15),
                    RankLevel = 2.5,
                    Status = MemberStatus.Active
                },
                new Member
                {
                    Id = 3,
                    FullName = "Lê Hoàng Cường",
                    DOB = new DateTime(1988, 3, 10),
                    PhoneNumber = "0923456789",
                    JoinDate = new DateTime(2024, 3, 1),
                    RankLevel = 4.0,
                    Status = MemberStatus.Active
                },
                new Member
                {
                    Id = 4,
                    FullName = "Phạm Minh Đức",
                    DOB = new DateTime(1992, 11, 25),
                    PhoneNumber = "0934567890",
                    JoinDate = new DateTime(2024, 4, 10),
                    RankLevel = 3.0,
                    Status = MemberStatus.Active
                },
                new Member
                {
                    Id = 5,
                    FullName = "Hoàng Thị Em",
                    DOB = new DateTime(1998, 7, 5),
                    PhoneNumber = "0945678901",
                    JoinDate = new DateTime(2024, 5, 20),
                    RankLevel = 2.0,
                    Status = MemberStatus.Inactive
                }
            );

            // Seed TransactionCategories
            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory
                {
                    Id = 1,
                    CategoryName = "Phí thành viên",
                    Type = TransactionType.Thu
                },
                new TransactionCategory
                {
                    Id = 2,
                    CategoryName = "Phí tham gia giải đấu",
                    Type = TransactionType.Thu
                },
                new TransactionCategory
                {
                    Id = 3,
                    CategoryName = "Chi phí sân bãi",
                    Type = TransactionType.Chi
                },
                new TransactionCategory
                {
                    Id = 4,
                    CategoryName = "Chi phí giải thưởng",
                    Type = TransactionType.Chi
                }
            );

            // Seed Challenges
            modelBuilder.Entity<Challenge>().HasData(
                new Challenge
                {
                    Id = 1,
                    CreatorId = 1,
                    Title = "Giải PickleBall Mùa Xuân 2026",
                    Description = "Giải đấu mở rộng dành cho tất cả thành viên CLB",
                    Mode = ChallengeMode.Singles,
                    RewardDescription = "Cúp vô địch + 500.000 VNĐ",
                    EntryFee = 100000,
                    Status = ChallengeStatus.Open
                },
                new Challenge
                {
                    Id = 2,
                    CreatorId = 3,
                    Title = "Giải Đôi Nam Nữ",
                    Description = "Giải đấu đôi nam nữ phối hợp",
                    Mode = ChallengeMode.Doubles,
                    RewardDescription = "Huy chương + Voucher 1 triệu",
                    EntryFee = 200000,
                    Status = ChallengeStatus.Open
                },
                new Challenge
                {
                    Id = 3,
                    CreatorId = 1,
                    Title = "Mini Game Cuối Tuần",
                    Description = "Trò chơi vui nhộn cuối tuần",
                    Mode = ChallengeMode.MiniGame,
                    RewardDescription = "Phần quà bất ngờ",
                    EntryFee = 50000,
                    Status = ChallengeStatus.Completed
                }
            );

            // Seed News
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Title = "Chào mừng thành viên mới tháng 1/2026",
                    Content = "CLB PickleBall xin chào mừng các thành viên mới gia nhập trong tháng 1/2026. Chúc các bạn có những trải nghiệm tuyệt vời!",
                    AuthorId = 1,
                    PublishedAt = new DateTime(2026, 1, 5),
                    IsPinned = true
                },
                new News
                {
                    Id = 2,
                    Title = "Lịch thi đấu tháng 1/2026",
                    Content = "Thông báo lịch thi đấu các giải trong tháng 1/2026. Các thành viên vui lòng đăng ký trước ngày 20/1.",
                    AuthorId = 3,
                    PublishedAt = new DateTime(2026, 1, 10),
                    IsPinned = false
                },
                new News
                {
                    Id = 3,
                    Title = "Quy định mới về trang phục thi đấu",
                    Content = "Từ ngày 1/2/2026, tất cả thành viên tham gia thi đấu phải mặc trang phục thể thao phù hợp.",
                    AuthorId = 1,
                    PublishedAt = new DateTime(2026, 1, 15),
                    IsPinned = false
                }
            );
        }
    }
}
