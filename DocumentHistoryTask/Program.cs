using DocumentHistoryTask.Data;
using DocumentHistoryTask.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Конфигурация: сначала secrets, потом env
builder.Configuration
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

// Логирование
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(opt =>
{
    opt.TimestampFormat = "HH:mm:ss ";
    opt.IncludeScopes = false;
});

// Строка подключения
var connectionString = builder.Configuration.GetConnectionString("AsistLabTaskPostgresDB")
    ?? builder.Configuration["AsistLabTaskPostgresDB"]
    ?? throw new InvalidOperationException("Не удалось получить строку подключения!");

string host = Environment.GetEnvironmentVariable("DB_HOST") ?? builder.Configuration["DB_HOST"] ?? throw new InvalidOperationException("Не удалось получить DB_HOST!");
string port = Environment.GetEnvironmentVariable("DB_PORT") ?? builder.Configuration["DB_PORT"] ?? throw new InvalidOperationException("Не удалось получить DB_PORT!");
string dbname = Environment.GetEnvironmentVariable("DB_NAME") ?? builder.Configuration["DB_NAME"] ?? throw new InvalidOperationException("Не удалось получить DB_NAME!");
string username = Environment.GetEnvironmentVariable("DB_ADMIN_USER") ?? builder.Configuration["DB_ADMIN_USER"] ?? throw new InvalidOperationException("Не удалось получить DB_ADMIN_USER!");
string userpass = Environment.GetEnvironmentVariable("DB_ADMIN_PASSWORD") ?? builder.Configuration["DB_ADMIN_PASSWORD"] ?? throw new InvalidOperationException("Не удалось получить DB_ADMIN_PASSWORD!");

var goodConneсtString = connectionString
    .Replace("${DB_HOST}", host)
    .Replace("${DB_PORT}", port)
    .Replace("${DB_NAME}", dbname)
    .Replace("${DB_ADMIN_USER}", username)
    .Replace("${DB_ADMIN_PASSWORD}", userpass);

//<Блок с подключением и настройкой БД>
builder.Services.AddDbContext<DocumentDbContext>(opt =>
    opt.UseNpgsql(goodConneсtString));

// Все сервисы
// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DocumentDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

//// ---------- Проверка подключения и миграции ----------
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
//    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        await db.Database.GetDbConnection().OpenAsync();
//        if (db.Database.CanConnect())
//        {
//            logger.LogInformation("Успешное подключение к базе данных. Применяем миграции...");
//            db.Database.Migrate();
//        }
//        else
//        {
//            logger.LogWarning("Не удалось подключиться к базе данных.");
//            return;
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "Ошибка при подключении к базе данных.");
//        return;

//    }
//    finally
//    {
//        await db.Database.GetDbConnection().CloseAsync();
//    }
//}

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
