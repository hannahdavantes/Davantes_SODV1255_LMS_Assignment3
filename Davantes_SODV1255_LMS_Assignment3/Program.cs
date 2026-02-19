var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();


app.MapControllers();
app.UseStaticFiles();
app.UseSession();

app.Run();
