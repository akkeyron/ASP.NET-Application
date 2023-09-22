using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // when use MVC architecture
// use sql server
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// inject dependency

var app = builder.Build();

// Configure the HTTP request pipeline.
// how to process reqyest come in
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // middleware
app.UseStaticFiles(); // will configure wwwoot paths to make it accessible in the application

app.UseRouting(); // routing in request pipeline

app.UseAuthorization(); // authentication and authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // default routing location, id? can be defined or null

// controller
// action

app.Run(); // run the project
