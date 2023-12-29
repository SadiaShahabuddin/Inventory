using Inventory.Data.DbInitializer;
using Inventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddRazorPages();
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.LoginPath = "/Identity/Account/Login";
        });
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Seed the database
        SeedDatabase();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
       
        app.MapRazorPages();

        app.Run();

        void SeedDatabase()
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Initialize();
            }
        }
    }
}
