# PickleBall Management System

Hệ thống quản lý PickleBall Club - Bài kiểm tra thực hành BackEnd

## 📋 Mô tả

Dự án gồm 2 phần:
- **Web.API**: REST API Server (ASP.NET Core MVC)
- **Web.Client**: Web Client giao diện người dùng (ASP.NET Core MVC)

### Connection String

Mở file `appsettings.json` trong thư mục `Web.API` và `Web.Client`, cập nhật connection string phù hợp với máy của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PickleBallDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

## 🚀 Hướng dẫn chạy project

1. Mở terminal và di chuyển đến thư mục project:
   ```bash
   cd Web.API
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Chạy migration để tạo database và seeding data:
   ```bash
   dotnet ef database update
   ```

4. Chạy Web.API:
   ```bash
   dotnet run
   ```

5. Mở terminal mới, di chuyển đến Web.Client và chạy:
   ```bash
   cd Web.Client
   dotnet run
   ```

## 📊 Database

### Migration

Project đã có sẵn các file migration trong thư mục `Web.API/Migrations/`:
- `InitialCreate`: Tạo cấu trúc database ban đầu
- `UpdateMatchAndNewsSchema`: Cập nhật schema cho Match và News

### Seeding Data

Khi chạy `Update-Database`, hệ thống sẽ tự động tạo dữ liệu mẫu bao gồm:
- **Members**: 5 thành viên mẫu
- **TransactionCategories**: 4 danh mục giao dịch (Thu/Chi)
- **Challenges**: 3 thử thách mẫu
- **News**: 3 bài tin tức mẫu

## 🔗 URLs mặc định

- **Web.API**: https://localhost:7001 (hoặc http://localhost:5001)
- **Web.Client**: https://localhost:7002 (hoặc http://localhost:5002)

## 📁 Cấu trúc Project

```
Kiemtra2/
├── Web.API/                    # REST API Server
│   ├── Controllers/            # API Controllers
│   ├── Data/                   # DbContext
│   ├── Migrations/             # EF Core Migrations
│   ├── Models/                 # Entity Models
│   └── Views/                  # Views (Home page)
│
├── Web.Client/                 # Web Client
│   ├── Controllers/            # MVC Controllers
│   ├── Models/                 # Entity Models
│   └── Views/                  # Razor Views
│
└── Kiemtra2.sln               # Solution file
```

## 📝 Các tính năng chính

1. **Quản lý Members (Thành viên)**
   - CRUD thành viên
   - Theo dõi rank level

2. **Quản lý Challenges (Thử thách)**
   - Tạo và quản lý các giải đấu
   - Đăng ký tham gia

3. **Quản lý Matches (Trận đấu)**
   - Ghi nhận kết quả trận đấu
   - Hỗ trợ đơn/đôi

4. **Quản lý News (Tin tức)**
   - Đăng và quản lý tin tức

5. **Quản lý Transactions (Giao dịch)**
   - Theo dõi thu/chi
   - Phân loại theo danh mục
