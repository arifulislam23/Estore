using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Estore.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EstoreContextConnection") ?? throw new InvalidOperationException("Connection string 'EstoreContextConnection' not found.");

builder.Services.AddDbContext<EstoreContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EstoreContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
 

var app = builder.Build();

 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area=Admin}/{controller=Dashboard}/{action=Index}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");




app.MapRazorPages();

app.Run();
