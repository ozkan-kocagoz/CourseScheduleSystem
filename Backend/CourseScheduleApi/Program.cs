using CourseScheduleApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Servisini Ekle
// appsettings.json'dan baðlantý bilgisini alýr ve PostgreSQL'e baðlar.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Controller Servislerini Ekle (API Endpointleri için)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Ayarý (Frontend'in Backend'e eriþmesi için izin)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. Middleware (Ara Yazýlým) Ayarlarý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); // Ýzni aktifleþtir
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();