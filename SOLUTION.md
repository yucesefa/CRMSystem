# 📘 Çözüm Dokümantasyonu – CRM Sistemi

## 🎯 Amaç
Bu proje, temel bir müşteri yönetimi sistemidir. Admin ve normal kullanıcıların giriş yapabildiği, JWT ile güvenliğin sağlandığı, kullanıcı ve müşteri CRUD işlemlerinin yapıldığı bir sistemdir.

---

## 🏗️ Mimarî

**Katmanlar:**

- `CRMSystem.API` (Web API)
- `CRMSystem.UI` (Razor Pages UI)
- `Data` Katmanı (EF Core)
- `Application` Katmanı (DTO, servisler)

**Kullanılan Desenler:**
- Repository Pattern
- DTO ile veri taşıma
- Middleware ile JWT doğrulama
- Role-based Authorization

---

## 🔒 Güvenlik
- JWT ile authentication.
- Rol bazlı authorization (`[Authorize(Roles = "Admin")]`).
- Cookie'de token saklama (`HttpOnly`, `Secure`, `SameSite`).

---

## 🔧 Geliştirme Notları

- Proje PostgreSQL ile code-first mantığıyla kuruldu.
- Swagger ile API test edildi.
- Razor tarafında `HttpClient` ile API istekleri yapılır.
- Kullanıcı adı `User.Identity.Name`, rol `ClaimTypes.Role` claim’inden alınır.

---

## ⚠️ Bilinen Eksikler
- Kullanıcı şifre sıfırlama yok.
- Unit testler dahil edilmedi.