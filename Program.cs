using Microsoft.EntityFrameworkCore;
using MentalNote.Models;
using MentalNote.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var connectionString = builder.Configuration
    .GetConnectionString("MentalNoteDBConnection") ??
    throw new InvalidOperationException("Connection string 'MentalNoteDBConnection' not found.");
builder.Services.AddDbContext<MentalNoteDbContext>(options =>
    options.UseSqlite(connectionString));
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
