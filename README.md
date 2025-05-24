# CRM System

Bu proje, .NET Core Web API, Razor Pages ve PostgreSQL kullanılarak geliştirilmiş basit bir CRM sistemidir.

## Özellikler

- Kullanıcı Girişi / Çıkışı (JWT Authentication)
- Rol Bazlı Yetkilendirme (Admin / User)
- Müşteri CRUD İşlemleri
- Kullanıcı Yönetimi (Admin paneli üzerinden)
- Şifre Güncelleme
- Cookie tabanlı oturum yönetimi

## Teknolojiler

- ASP.NET Core 8
- Razor Pages
- PostgreSQL (EF Core)
- JWT Authentication
- RESTful API
- Visual Studio 2022

## Başlangıç

1. API projesinde `appsettings.json` dosyasına veritabanı bağlantısını ekleyin.
2. EF Core ile migration'ları uygulayın:
3. API’yi başlatın (`CRMSystem.API`)
4. UI projesini başlatın (`CRMSystem.UI`)
5. Giriş için örnek kullanıcı:
Kullanıcı: admin
Şifre: admin123
