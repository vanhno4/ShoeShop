using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;

var builder = WebApplication.CreateBuilder(args);

// --- THÊM DỊCH VỤ SESSION ---
builder.Services.AddDistributedMemoryCache(); // Cần cho Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// --- KẾT THÚC THÊM ---

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
    });

var projectRootPath = builder.Environment.ContentRootPath;
var dbPath = Path.Combine(projectRootPath, "ShoeShop.db");
var connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
);

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

SeedDatabase(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// --- SỬ DỤNG SESSION ---
// (Phải nằm SAU UseRouting và TRƯỚC MapRazorPages)
app.UseSession();
// --- KẾT THÚC THÊM ---

app.MapRazorPages();
app.Run();

void SeedDatabase(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            DataSeeder.Seed(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}