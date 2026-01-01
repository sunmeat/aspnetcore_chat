using Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(); // в проєкті Web App MVC SignalR вже працює із коробки, але в разі чого завжди можна додати пакет Microsoft.AspNetCore.SignalR

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS - це механізм безпеки, який змушує браузер навіть не надсилати HTTP-запити
}

app.UseHttpsRedirection(); // робить перенаправлення HTTP-запитів на HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub"); // <-- еndpoint для SignalR, підключення буде відбуватися зі сторони клієнта за цією адресою, в цьому прикладі у файлі Index.cshtml

app.Run();