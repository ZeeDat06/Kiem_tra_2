# 🏸 Vợt Thủ Phố Núi - PickleBall Management System

Hệ thống quản lý Câu lạc bộ PickleBall - Dự án ASP.NET Core Web API (.NET 8.0 LTS)

## 📋 Mô tả

Dự án gồm 2 phần chính:
- **Web.API**: REST API Server (ASP.NET Core Web API)
- **Web.Client**: Web Client giao diện người dùng (ASP.NET Core MVC)

## 🚀 Hướng dẫn chạy project

### Cách 1: Chạy với Docker Compose (Khuyến nghị)

```bash
# Clone project về máy
git clone <repository-url>
cd Kiemtra2

# Khởi chạy toàn bộ hệ thống (Database + API + Client)
docker compose up -d

# Xem logs để theo dõi
docker compose logs -f

# Dừng hệ thống
docker compose down
```

**Truy cập sau khi khởi chạy:**
- 🌐 **Web Client**: http://localhost
- 🔌 **Web API**: http://localhost:5000
- 📚 **Swagger API Docs**: http://localhost:5000/swagger

### Cách 2: Chạy Local (Development)

#### Yêu cầu:
- .NET 8.0 SDK
- SQL Server (LocalDB hoặc SQL Server Express)

#### Bước 1: Cấu hình Connection String

Mở file `appsettings.Development.json` trong thư mục `Web.API` và `Web.Client`, cập nhật connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PickleBallDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

#### Bước 2: Chạy Web.API

```bash
cd Web.API
dotnet restore
dotnet ef database update
dotnet run
```

#### Bước 3: Chạy Web.Client (Terminal mới)

```bash
cd Web.Client
dotnet restore
dotnet run
```

## 🐳 Deploy với Docker

### Build và Push Docker Image

```bash
# Build image cho API
cd Web.API
docker build -t yourusername/votthupho-api:v1 .

# Build image cho Client
cd ../Web.Client
docker build -t yourusername/votthupho-client:v1 .

# Push lên Docker Hub
docker login
docker push yourusername/votthupho-api:v1
docker push yourusername/votthupho-client:v1
```

### Docker Compose Configuration

File `docker-compose.yml` đã được cấu hình sẵn với:
- **SQL Server 2022**: Database server
- **Web.API**: REST API trên port 5000
- **Web.Client**: Web interface trên port 80

## 🌐 Deploy lên VPS Linux (Ubuntu)

### 1. Cài đặt môi trường trên VPS

```bash
# Cập nhật hệ thống
sudo apt-get update

# Cài đặt .NET 8.0
sudo apt-get install -y dotnet-sdk-8.0 aspnetcore-runtime-8.0

# Cài đặt Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh
sudo usermod -aG docker $USER
```

### 2. Chạy với Docker Compose

```bash
# Upload source code hoặc clone từ Git
git clone <repository-url>
cd Kiemtra2

# Khởi chạy
docker compose up -d
```

### 3. Cấu hình Nginx Reverse Proxy (Optional)

```bash
sudo apt-get install -y nginx
```

Tạo file cấu hình `/etc/nginx/sites-available/votthupho`:

```nginx
server {
    listen 80;
    server_name your_domain.com;

    location / {
        proxy_pass http://localhost:80;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    location /api {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

### 4. Cài đặt SSL với Let's Encrypt

```bash
sudo apt-get install -y certbot python3-certbot-nginx
sudo certbot --nginx -d your_domain.com
```

## 📊 Database

### Migration

Project sử dụng Entity Framework Core với các migration:
- `InitialCreate`: Tạo cấu trúc database ban đầu
- `UpdateMatchAndNewsSchema`: Cập nhật schema cho Match và News
- `SeedingData`: Dữ liệu mẫu ban đầu

### Seeding Data

Khi chạy migration, hệ thống tự động tạo dữ liệu mẫu:
- **Members**: 5 thành viên mẫu
- **TransactionCategories**: 4 danh mục giao dịch (Thu/Chi)
- **Challenges**: 3 thử thách mẫu
- **News**: 3 bài tin tức mẫu

## 🔗 API Endpoints

| Endpoint | Mô tả |
|----------|-------|
| `GET /api/Members` | Lấy danh sách thành viên |
| `GET /api/Members/{id}` | Lấy thông tin thành viên |
| `POST /api/Members` | Tạo thành viên mới |
| `PUT /api/Members/{id}` | Cập nhật thành viên |
| `DELETE /api/Members/{id}` | Xóa thành viên |
| `GET /api/Challenges` | Lấy danh sách thử thách |
| `GET /api/Matches` | Lấy danh sách trận đấu |
| `GET /api/News` | Lấy danh sách tin tức |
| `GET /api/Transactions` | Lấy danh sách giao dịch |

📚 **Xem đầy đủ tại**: http://localhost:5000/swagger

## 📁 Cấu trúc Project

```
Kiemtra2/
├── docker-compose.yml          # Docker Compose configuration
├── README.md                   # Tài liệu hướng dẫn
├── Kiemtra2.sln               # Solution file
│
├── Web.API/                    # REST API Server
│   ├── Controllers/            # API Controllers
│   ├── Data/                   # DbContext & Migrations
│   ├── Models/                 # Entity Models
│   ├── Dockerfile              # Docker build file
│   ├── Program.cs              # Entry point
│   └── appsettings.json        # Configuration
│
└── Web.Client/                 # Web Client
    ├── Controllers/            # MVC Controllers
    ├── Views/                  # Razor Views
    ├── Models/                 # View Models
    ├── Dockerfile              # Docker build file
    └── appsettings.json        # Configuration
```

## 📝 Các tính năng chính

1. **Quản lý Members (Thành viên)**
   - CRUD thành viên
   - Theo dõi rank level
   - Trạng thái hoạt động

2. **Quản lý Challenges (Thử thách)**
   - Tạo và quản lý các giải đấu
   - Đăng ký tham gia
   - Theo dõi kết quả

3. **Quản lý Matches (Trận đấu)**
   - Ghi nhận kết quả trận đấu
   - Hỗ trợ đánh đơn/đánh đôi

4. **Quản lý News (Tin tức)**
   - Đăng tin tức CLB
   - Phân loại theo danh mục

5. **Quản lý Transactions (Thu chi)**
   - Theo dõi thu chi
   - Phân loại giao dịch

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 8.0 Web API
- **Frontend**: ASP.NET Core MVC, Bootstrap 5
- **Database**: SQL Server 2022
- **ORM**: Entity Framework Core 8.0
- **Containerization**: Docker & Docker Compose
- **API Documentation**: Swagger/OpenAPI

## 👥 Tác giả

Dự án Vợt Thủ Phố Núi - Bài kiểm tra thực hành BackEnd

## 📄 License

MIT License
