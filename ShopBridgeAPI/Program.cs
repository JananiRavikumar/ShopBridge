using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Profiles;
using ShopBridgeAPI.Services.DataServices;
using ShopBridgeAPI.Services.DataServices.Interfaces;
using ShopBridgeAPI.Services.DataServices.ValidationService;
using ShopBridgeAPI.Services.ValidationService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IGetProductService, GetProductService>();
builder.Services.AddScoped<IProductValidationService, ProductValidationService>();
builder.Services.AddDbContext<ShopBridgeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShopBridgeDBConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopBridge API", Version = "v1" });
    option.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://dev-xfx4kbp81aq1x4dj.us.auth0.com/connect/authorize"),
                    TokenUrl = new Uri("https://dev-xfx4kbp81aq1x4dj.us.auth0.com/oauth/token"),

                }
            }
        });

    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Id = "oauth2",
                        Type = ReferenceType.SecurityScheme
                    },
                    Scheme = "oauth2",
                    Name = "oauth2",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
});
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddMvc();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://dev-xfx4kbp81aq1x4dj.us.auth0.com/",
        ValidateAudience = false, //"https://shopbridge-v2.com",
        ValidateIssuerSigningKey = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateAccess", policy =>
                          policy.RequireClaim("permissions", "create:products"));
    options.AddPolicy("UpdateAccess", policy =>
                      policy.RequireClaim("permissions", "update:product"));
    options.AddPolicy("DeleteAccess", policy =>
                      policy.RequireClaim("permissions", "delete:products"));
    options.AddPolicy("GetAccess", policy =>
                      policy.RequireClaim("permissions", "get:products"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.Run();
