# 🏸 Vợt Thủ Phố Núi - PickleBall Management System

Hệ thống quản lý Câu lạc bộ PickleBall - Dự án ASP.NET Core Web API (.NET 8.0 LTS)

## 📋 Mô tả

Dự án gồm 2 phần chính:
- **Web.API**: REST API Server (ASP.NET Core Web API)
- **Web.Client**: Web Client giao diện người dùng (ASP.NET Core MVC)

## 🚀 Hướng dẫn chạy project (Local Development)

### Yêu cầu:
- .NET 8.0 SDK
- SQL Server (LocalDB hoặc SQL Server Express)

### Bước 1: Cấu hình Connection String

Mở file `appsettings.Development.json` trong thư mục `Web.API` và `Web.Client`, cập nhật connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PickleBallDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Bước 2: Chạy Web.API

```bash
cd Web.API
dotnet restore
dotnet ef database update
dotnet run
```

### Bước 3: Chạy Web.Client (Terminal mới)

```bash
cd Web.Client
dotnet restore
dotnet run
```

## 🌐 Deploy lên VPS Linux (Ubuntu)

### 1. Cài đặt môi trường trên VPS

```bash
# SSH vào VPS
ssh root@YOUR_VPS_IP

# Cập nhật hệ thống
sudo apt-get update

# Cài đặt .NET 8.0 Runtime
sudo apt-get install -y aspnetcore-runtime-8.0

# Kiểm tra
dotnet --version
```

### 2. Cài đặt SQL Server 2022

```bash
# Import GPG key
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list

# Cài đặt SQL Server
sudo apt-get update
sudo apt-get install -y mssql-server

# Cấu hình (chọn Developer Edition, đặt mật khẩu SA)
sudo /opt/mssql/bin/mssql-conf setup

# Kiểm tra
systemctl status mssql-server
```

### 3. Publish và Deploy ứng dụng

**Trên máy local:**
```bash
cd Web.API
dotnet publish -c Release -o ./publish

cd ../Web.Client
dotnet publish -c Release -o ./publish
```

**Upload lên VPS và cấu hình:**
```bash
# Tạo thư mục
sudo mkdir -p /var/www/votthupho-api
sudo mkdir -p /var/www/votthupho-client

# Upload files (dùng SCP hoặc WinSCP)
# Cấp quyền
sudo chown -R www-data:www-data /var/www/votthupho-api
sudo chown -R www-data:www-data /var/www/votthupho-client
```

### 4. Tạo Systemd Service

**Cho Web.API** (`/etc/systemd/system/votthupho-api.service`):
```ini
[Unit]
Description=Vot Thu Pho Nui API

[Service]
WorkingDirectory=/var/www/votthupho-api
ExecStart=/usr/bin/dotnet /var/www/votthupho-api/Web.API.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5000

[Install]
WantedBy=multi-user.target
```

**Cho Web.Client** (`/etc/systemd/system/votthupho-client.service`):
```ini
[Unit]
Description=Vot Thu Pho Nui Client

[Service]
WorkingDirectory=/var/www/votthupho-client
ExecStart=/usr/bin/dotnet /var/www/votthupho-client/Web.Client.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5001

[Install]
WantedBy=multi-user.target
```

**Kích hoạt services:**
```bash
sudo systemctl enable votthupho-api votthupho-client
sudo systemctl start votthupho-api votthupho-client
```

### 5. Cấu hình Nginx Reverse Proxy

```bash
sudo apt-get install -y nginx
```

Tạo file `/etc/nginx/sites-available/votthupho`:
```nginx
server {
    listen 80;
    server_name your_domain.com;

    location / {
        proxy_pass http://localhost:5001;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    location /api {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

```bash
sudo ln -s /etc/nginx/sites-available/votthupho /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

### 6. Cài SSL với Let's Encrypt

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

📚 **Swagger API Docs**: http://localhost:5000/swagger

## 📁 Cấu trúc Project

```
Kiemtra2/
├── README.md                   # Tài liệu hướng dẫn
├── Kiemtra2.sln               # Solution file
│
├── Web.API/                    # REST API Server
│   ├── Controllers/            # API Controllers
│   ├── Data/                   # DbContext & Migrations
│   ├── Models/                 # Entity Models
│   ├── Program.cs              # Entry point
│   └── appsettings.json        # Configuration
│
└── Web.Client/                 # Web Client
    ├── Controllers/            # MVC Controllers
    ├── Views/                  # Razor Views
    ├── Models/                 # View Models
    └── appsettings.json        # Configuration
```

## 📝 Các tính năng chính

1. **Quản lý Members (Thành viên)** - CRUD, theo dõi rank level
2. **Quản lý Challenges (Thử thách)** - Tạo giải đấu, đăng ký tham gia
3. **Quản lý Matches (Trận đấu)** - Ghi nhận kết quả đơn/đôi
4. **Quản lý News (Tin tức)** - Đăng tin CLB
5. **Quản lý Transactions (Thu chi)** - Theo dõi thu chi

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 8.0 Web API
- **Frontend**: ASP.NET Core MVC, Bootstrap 5
- **Database**: SQL Server 2022
- **ORM**: Entity Framework Core 8.0
- **API Documentation**: Swagger/OpenAPI

## 👥 Tác giả

Dự án Vợt Thủ Phố Núi - Bài kiểm tra thực hành BackEnd

## 📄 License

MIT License
