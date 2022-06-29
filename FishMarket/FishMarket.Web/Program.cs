using FishMarket.Client;
using FishMarket.DataAccess.Abstract;
using FishMarket.DataAccess.Concrete.EntityFrameworkCore;
using FishMarket.Service.Abstract;
using FishMarket.Service.Concrete;
using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IFishDal, EFFishDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<IFishPriceDal, EFFishPriceDal>();
builder.Services.AddScoped<IFishService, FishManager>();
builder.Services.AddScoped<IFishPriceService, FishPriceManager>();
builder.Services.AddScoped<ITokenService, TokenManager>();
builder.Services.AddScoped<IUtilityService, UtilityManager>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors();
builder.Services.AddRefitClient<IFishMarketClient>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
});
builder.Services.AddRefitClient<IUserClient>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiUri"]);
});



var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
