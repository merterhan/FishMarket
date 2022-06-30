using FishMarket.Client;
using FishMarket.DataAccess.Abstract;
using FishMarket.DataAccess.Concrete.EntityFrameworkCore;
using FishMarket.Service.Abstract;
using FishMarket.Service.Concrete;
using FishMarket.Web.Service;
using Microsoft.AspNetCore.Http.Json;
using Refit;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddScoped<IFishDal, EFFishDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<IFishPriceDal, EFFishPriceDal>();
builder.Services.AddScoped<IFishService, FishManager>();
builder.Services.AddScoped<IFishPriceService, FishPriceManager>();
builder.Services.AddScoped<ITokenService, TokenManager>();
builder.Services.AddScoped<IUtilityService, UtilityManager>();
#endregion


#region Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddCors();

#region Refit Options
var userApi = RestService.For<IUserClient>(builder.Configuration["ApiUri"]);
var token = await userApi.GetToken();

builder.Services.AddRefitClient<IFishMarketClient>(new RefitSettings()
{
    AuthorizationHeaderValueGetter = () =>
        Task.FromResult(token)
}).ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
});
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
});
#endregion

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});


var app = builder.Build();

app.UseSession();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.WithMethods("GET");
    p.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FishMarket}/{action=List}/{id?}");

app.Run();
