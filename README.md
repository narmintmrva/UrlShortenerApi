# 🔗 URL Shortener API

A simple ASP.NET Core Web API for shortening URLs, similar to TinyURL or Bitly.

## 🚀 Features
- Convert long URLs into short codes
- Redirect using short code
- Optional expiry date
- Click count tracking
- Custom short codes

## 🛠 Tech Stack
- ASP.NET Core (.NET 8)
- Entity Framework Core
- MSSQL Server
- AutoMapper
- Swagger for documentation

## 📌 Endpoints
- `POST /api/shorturl` – Create a short URL
- `GET /r/{code}` – Redirect to original URL
- `GET /api/shorturl/{code}` – View analytics (click count, etc.)

## 🔧 Setup
1. Update your DB connection string in `appsettings.json`
2. Run migrations:
   `Add-Migration InitialCreate`
    `Update-Database`
3. Start the project with Visual Studio or `dotnet run`
4. Test via Swagger at `/swagger`

## 📝 License
MIT – Use freely for learning or building upon.
