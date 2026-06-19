using EnvanteriX.WebUI.Models;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IApiClientService, ApiClientService>();
builder.Services.AddHttpContextAccessor();

var baseType = typeof(BaseApiUrl);
var derivedTypes = AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract && t.IsClass);

foreach (var type in derivedTypes)
{
    var sectionName = type.Name; // appsettings'le e�le�ecek
    var instance = Activator.CreateInstance(type);
    builder.Configuration.GetSection(sectionName).Bind(instance);
    builder.Services.AddSingleton(type, instance);
}

// Session middleware'ini ekleyin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Oturum s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in HttpOnly cookie
    options.Cookie.IsEssential = true; // Cookie'nin gerekli oldu�unu belirtir
    options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});
// Kimlik do�rulama �emas� ekleyin
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Giri� yapmadan eri�ilmeyecek sayfa
        options.LogoutPath = "/Account/Logout"; // ��k�� yapma URL'si
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
