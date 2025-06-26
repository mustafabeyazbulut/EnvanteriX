using EnvanteriX.WebUI.Models;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IApiClientService, ApiClientService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

var baseType = typeof(BaseApiUrl);
var derivedTypes = AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract && t.IsClass);

foreach (var type in derivedTypes)
{
    var sectionName = type.Name; // appsettings'le eþleþecek
    var instance = Activator.CreateInstance(type);
    builder.Configuration.GetSection(sectionName).Bind(instance);
    builder.Services.AddSingleton(type, instance);
}

// Session middleware'ini ekleyin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Oturum süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HttpOnly cookie
    options.Cookie.IsEssential = true; // Cookie'nin gerekli olduðunu belirtir
    options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});
// Kimlik doðrulama þemasý ekleyin
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Giriþ yapmadan eriþilmeyecek sayfa
        options.LogoutPath = "/Account/Logout"; // Çýkýþ yapma URL'si
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
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
