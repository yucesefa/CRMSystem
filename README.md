# 📊 CRM System – ASP.NET Core & Razor Pages

Bu proje, müşteri verilerinin yönetimi için geliştirilen basit bir CRM sistemidir. ASP.NET Core Web API, Razor Pages ve PostgreSQL kullanılarak geliştirilmiştir.

## 🚀 Özellikler

- ✅ JWT ile kullanıcı girişi ve çıkışı
- ✅ Rol bazlı erişim kontrolü (Admin / User)
- ✅ Müşteri CRUD işlemleri (Listeleme, Ekleme, Güncelleme, Silme)
- ✅ Kullanıcı CRUD (sadece admin)
- ✅ Şifre güncelleme
- ✅ Cookie tabanlı oturum yönetimi
- ✅ Gelişmiş hata ve yetki yönetimi
- ✅ Swagger üzerinden API test imkanı

## 🛠️ Kullanılan Teknolojiler

- ASP.NET Core 8 (Web API)
- Razor Pages (UI)
- Entity Framework Core + PostgreSQL
- JWT Authentication
- RESTful API
- Visual Studio 2022

## 🧰 Kurulum Talimatları

1. **Veritabanı ayarlarını yapın:**
   - `appsettings.json` içindeki connection string'i PostgreSQL'e göre ayarlayın.

2. **EF Core Migration Uygulama:**

3. **Projeyi çalıştırın:**
- `CRMSystem.API` ve `CRMSystem.UI` projelerini birlikte başlatın.

## 🔐 Örnek Giriş Bilgileri
Kullanıcı Adı: admin
Şifre: 123456
Rol: Admin


## 🤝 Katkıda Bulun

Katkı sağlamak istiyorsanız `pull request` göndererek başlayabilirsiniz. Detaylar için CONTRIBUTING.md dosyasına göz atabilirsiniz.

---

## 🧾 Lisans

Bu proje özel test ve geliştirme amaçlıdır. Ticari kullanımlarda telif içerikleri göz önünde bulundurulmalıdır.
