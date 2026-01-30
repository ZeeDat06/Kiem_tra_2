# Giai đoạn 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copy file solution và các file csproj
COPY Kiemtra2.sln ./
COPY Web.API/*.csproj ./Web.API/
COPY Web.Client/*.csproj ./Web.Client/

# 2. Restore (Tải thư viện)
RUN dotnet restore

# 3. Copy toàn bộ source code còn lại
COPY . .

# 4. Build và Publish project Web.API
# (Nếu bạn muốn chạy Web.Client thì sửa Web.API thành Web.Client ở 2 dòng dưới)
WORKDIR /app/Web.API
RUN dotnet publish -c Release -o /app/out

# Giai đoạn 2: Run (Chạy ứng dụng)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Mở cổng 8080
EXPOSE 8080

# Chạy file dll (Tên này phải khớp với tên project, thường là Web.API.dll)
ENTRYPOINT ["dotnet", "Web.API.dll"]