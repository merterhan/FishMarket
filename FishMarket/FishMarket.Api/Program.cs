using AutoMapper;
using FishMarket.Api.Mappings;
using FishMarket.DataAccess.Abstract;
using FishMarket.DataAccess.Concrete.EntityFrameworkCore;
using FishMarket.Service.Abstract;
using FishMarket.Service.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region DataProtection
builder.Services.AddDataProtection();
builder.Services.AddCors();

var serviceProvider = builder.Services.BuildServiceProvider();
var _provider = serviceProvider.GetService<IDataProtectionProvider>();
var protector = _provider.CreateProtector(builder.Configuration["Protector_Key"]);
#endregion


#region AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new FishMarketMapper(protector));
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);
#endregion

#region Service Resolves
builder.Services.AddScoped<IFishDal, EFFishDal>();
builder.Services.AddTransient<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddTransient<IFishService, FishManager>();
builder.Services.AddTransient<ITokenService, TokenManager>();

#endregion

builder.Services.AddControllers();

#region JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidAudience = builder.Configuration["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"]))
    };
});
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,

      },
      new List<string>()
    }});
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.UseAuthentication();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.WithMethods("GET");
    p.AllowAnyHeader();
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.UseAuthorization();

app.MapControllers();

app.Run();
