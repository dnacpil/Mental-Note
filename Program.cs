using Microsoft.EntityFrameworkCore;
using MentalNote.Data;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration
    .GetConnectionString("MentalNoteDBConnection") ??
    throw new InvalidOperationException("Connection string 'MentalNoteDBConnection' not found.");
builder.Services.AddDbContext<MentalNoteDbContext>(options =>
    options.UseSqlite(connectionString));
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MentalNoteDbContext>();

//builder.Services.AddScoped<ReminderService>();

builder.Services.AddRazorPages();

//Syncfusion license registration
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt8QHJqUU1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVeQF9kSH1Wf0ZkWXtZcw==;Mgo+DSMBPh8sVXJ3S0R+WFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5iS3xUd0ZmUHxZcHJXTw==;Mgo+DSMBMAY9C3t2VlhiQlVPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSHxRcURgXHZdd3RSTmY=");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
