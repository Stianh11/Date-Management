using Date_Management_Project.Data;
using Date_Management_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) SQL Server connection
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("DefaultConnection not found!");

// 2) EF + Identity on the *same* AppDbContext
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(conn));

builder.Services
    .AddDefaultIdentity<IdentityUser>(opt => opt.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// 3) register our holiday‐API client + working-days service
builder.Services.AddHttpClient<IHolidayApiService, HolidayApiService>(c =>
    c.BaseAddress = new Uri("https://date.nager.at/"));
builder.Services.AddScoped<WorkingDaysService>();

// 4) MVC + RazorPages (for Identity UI)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// 5) apply any pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// your routes + Identity pages
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
