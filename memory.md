# Project Memory

## Overview
- **Project Name:** api
- **Type:** ASP.NET Core 9 Minimal API (Web API)
- **Target Framework:** .NET 9.0 (`net9.0`)
- **API Style:** Minimal API (ไม่ใช่ Controller-based)

## Project Structure
```
.github/
  copilot-instructions.md    # คำแนะนำสำหรับ Copilot เกี่ยวกับ ASP.NET REST API
api/
  Program.cs                 # Entry point หลัก - กำหนด endpoints และ middleware
  api.csproj                 # ไฟล์โปรเจกต์ .NET
  api.http                   # ไฟล์ทดสอบ HTTP requests
  appsettings.json           # การตั้งค่าหลัก
  appsettings.Development.json # การตั้งค่าสำหรับ Development
  Properties/
    launchSettings.json      # การตั้งค่า launch profiles
```

## Dependencies
- **Microsoft.AspNetCore.OpenApi** v9.0.0 — สำหรับสร้าง OpenAPI documentation
- **Microsoft.OpenApi** v1.6.17 — transitive dependency

## Endpoints
| Method | Path               | Description                          |
|--------|--------------------|--------------------------------------|
| GET    | `/weatherforecast` | คืนค่า weather forecast 5 วัน (ข้อมูลสุ่ม) |

## Models
- **WeatherForecast** — `record` ที่มี properties:
  - `Date` (`DateOnly`)
  - `TemperatureC` (`int`)
  - `Summary` (`string?`)
  - `TemperatureF` (computed: `32 + (int)(TemperatureC / 0.5556)`)

## Configuration
- **HTTP Profile:** `http://localhost:5170`
- **HTTPS Profile:** `https://localhost:7141` + `http://localhost:5170`
- **Environment:** `ASPNETCORE_ENVIRONMENT=Development`
- **OpenAPI:** เปิดใช้งานเฉพาะใน Development (`app.MapOpenApi()`)
- **HTTPS Redirection:** เปิดใช้งาน (`app.UseHttpsRedirection()`)

## Key Decisions
- ใช้ **Minimal API** แทน Controller-based API เพื่อความกระชับ
- ยังไม่มี database — ใช้ข้อมูลสุ่มแบบ in-memory
- ยังไม่มี authentication/authorization
- ยังไม่มี custom error handling หรือ validation
- ยังไม่มี unit tests

## Next Steps (ตาม copilot-instructions.md)
- [ ] เพิ่ม CRUD endpoints สำหรับ resource ใหม่
- [ ] เพิ่ม Data Access Layer (Entity Framework Core)
- [ ] เพิ่ม Model Validation (Data Annotations / FluentValidation)
- [ ] เพิ่ม Error Handling Middleware
- [ ] เพิ่ม Authentication (JWT Bearer)
- [ ] เพิ่ม API Versioning
- [ ] เพิ่ม Logging (Serilog)
- [ ] เพิ่ม Unit Tests และ Integration Tests
- [ ] เพิ่ม Caching และ Performance Optimization
