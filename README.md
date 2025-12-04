## Proje Yapısı

Proje dosyaları aşağıdaki klasör yapısına göre düzenlenmiştir:

* **`Backend/`**: .NET 8.0 Web API ile geliştirilen sunucu tarafı.
    * PostgreSQL veritabanı bağlantısı (Entity Framework Core).
    * SMTP E-posta entegrasyonu (MailKit).
    * RESTful API endpoint'leri (`/api/auth`, `/api/departments`, `/api/users`).
* **`Frontend/`**: HTML5, CSS3 ve Vanilla JavaScript ile geliştirilen istemci tarafı.
    * `login.html`: Giriş ekranı.
    * `dashboard.html`: Yönetim paneli.
    * `assets/`: CSS ve JS dosyaları.

## Kurulum ve Çalıştırma

Projeyi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

### 1. Veritabanı Kurulumu (PostgreSQL)
* PostgreSQL'de `CourseScheduleDB` adında bir veritabanı oluşturun.
* `Backend/CourseScheduleApi/appsettings.json` dosyasındaki `ConnectionStrings` bölümüne kendi veritabanı şifrenizi girin.
* Proje ilk çalıştığında tablolar otomatik oluşmazsa, ilgili SQL scriptlerini çalıştırın veya Code-First migration uygula.

### 2. Backend'i Ayağa Kaldırma
Terminali açın ve Backend klasörüne gidin:
```bash
cd Backend/CourseScheduleApi
dotnet run
