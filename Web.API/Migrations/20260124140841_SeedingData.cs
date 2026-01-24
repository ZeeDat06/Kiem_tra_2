using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "133_Members",
                columns: new[] { "Id", "DOB", "FullName", "IdentityUserId", "JoinDate", "PhoneNumber", "RankLevel", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Văn An", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0901234567", 3.5, 0 },
                    { 2, new DateTime(1995, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Thị Bình", null, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678", 2.5, 0 },
                    { 3, new DateTime(1988, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lê Hoàng Cường", null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0923456789", 4.0, 0 },
                    { 4, new DateTime(1992, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phạm Minh Đức", null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0934567890", 3.0, 0 },
                    { 5, new DateTime(1998, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàng Thị Em", null, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0945678901", 2.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "133_TransactionCategories",
                columns: new[] { "Id", "CategoryName", "Type" },
                values: new object[,]
                {
                    { 1, "Phí thành viên", 0 },
                    { 2, "Phí tham gia giải đấu", 0 },
                    { 3, "Chi phí sân bãi", 1 },
                    { 4, "Chi phí giải thưởng", 1 }
                });

            migrationBuilder.InsertData(
                table: "133_Challenges",
                columns: new[] { "Id", "CreatorId", "Description", "EntryFee", "Mode", "RewardDescription", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Giải đấu mở rộng dành cho tất cả thành viên CLB", 100000m, 0, "Cúp vô địch + 500.000 VNĐ", 0, "Giải PickleBall Mùa Xuân 2026" },
                    { 2, 3, "Giải đấu đôi nam nữ phối hợp", 200000m, 1, "Huy chương + Voucher 1 triệu", 0, "Giải Đôi Nam Nữ" },
                    { 3, 1, "Trò chơi vui nhộn cuối tuần", 50000m, 2, "Phần quà bất ngờ", 2, "Mini Game Cuối Tuần" }
                });

            migrationBuilder.InsertData(
                table: "133_News",
                columns: new[] { "Id", "AuthorId", "Content", "IsPinned", "PublishedAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, "CLB PickleBall xin chào mừng các thành viên mới gia nhập trong tháng 1/2026. Chúc các bạn có những trải nghiệm tuyệt vời!", true, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chào mừng thành viên mới tháng 1/2026" },
                    { 2, 3, "Thông báo lịch thi đấu các giải trong tháng 1/2026. Các thành viên vui lòng đăng ký trước ngày 20/1.", false, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lịch thi đấu tháng 1/2026" },
                    { 3, 1, "Từ ngày 1/2/2026, tất cả thành viên tham gia thi đấu phải mặc trang phục thể thao phù hợp.", false, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quy định mới về trang phục thi đấu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "133_Challenges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "133_Challenges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "133_Challenges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "133_Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "133_Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "133_Members",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "133_News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "133_News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "133_News",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "133_TransactionCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "133_TransactionCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "133_TransactionCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "133_TransactionCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "133_Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "133_Members",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
