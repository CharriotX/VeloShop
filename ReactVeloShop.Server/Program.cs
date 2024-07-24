using Data.Interface.Repositories;
using Data.Services.Interfaces.AuthService;
using Data.Services.Interfaces.BrandsService;
using Data.Services.Interfaces.CartService;
using Data.Services.Interfaces.CategoriesService;
using Data.Services.Interfaces.ProductsService;
using Data.Services.Interfaces.SubcategoriesService;
using Data.Services.Interfaces.UsersService;
using Data.Services.Services;
using Data.Sql;
using Data.Sql.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using ReactVeloShop.Server.Extensions;
using ReactVeloShop.Server.Extentions;
using ReactVeloShop.Server.Helpers.Jwt;
using ReactVeloShop.Server.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var serviceProvider = builder.Services.BuildServiceProvider();
var opt = serviceProvider.GetRequiredService<IOptions<JwtOptions>>();




builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});
builder.Services.AddCors();

builder.Services.AddDbContext<WebContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.Path = "/";
});

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

ApiExtensions.AddApiAuthentication(builder.Services, opt);
builder.Services.AddWebEncoders();
builder.Services.AddScoped<IUserService>(x =>
    new UserService(
        x.GetRequiredService<IUserRepository>()));
builder.Services.AddScoped<IProductService>(x =>
    new ProductService(
        x.GetRequiredService<IProductRepository>()
         ));

builder.Services.AddScoped<IBrandService>(x =>
    new BrandService(
        x.GetRequiredService<IBrandRepository>()
         ));

builder.Services.AddScoped<ICategoryService>(x =>
    new CategoryService(
        x.GetRequiredService<ICategoryRepository>(),
        x.GetRequiredService<ISubcategoryRepository>()));
builder.Services.AddScoped<ICartService>(x =>
    new CartService(
            x.GetRequiredService<ICartRepository>(),
            x.GetRequiredService<IProductRepository>(),
            x.GetRequiredService<IProductService>()
        ));
builder.Services.AddScoped<ISubcategoryService>(x => new SubcategoryService(x.GetRequiredService<ISubcategoryRepository>()));
builder.Services.AddScoped<IAuthService>(x =>
    new AuthService(
        x.GetRequiredService<ITokenRepository>(),
        x.GetRequiredService<IJwtProvider>(),
        x.GetRequiredService<IUserRepository>(),
        x.GetRequiredService<IPasswordHasher>()));

builder.Services.AddScoped<IProductSpecificationRepository>(x =>
    new ProductSpecificationRepository(x.GetRequiredService<WebContext>(), x.GetRequiredService<ISpecificationRepository>()));
builder.Services.AddScoped<ICategoryRepository>(x => new CategoryRepository(x.GetRequiredService<WebContext>()));

builder.Services.AddScoped<IProductRepository>(x => new ProductRepository(x.GetRequiredService<WebContext>(),
    x.GetRequiredService<ICategoryRepository>(),
    x.GetRequiredService<ISpecificationRepository>(),
    x.GetRequiredService<IBrandRepository>(),
    x.GetRequiredService<IProductSpecificationRepository>(),
    x.GetRequiredService<ISubcategoryRepository>()));

builder.Services.AddScoped<ISubcategoryRepository>(x => new SubcategoryRepository(x.GetRequiredService<WebContext>(), x.GetRequiredService<ICategoryRepository>()));
builder.Services.AddScoped<ISpecificationRepository>(x => new SpecificationRepository(x.GetRequiredService<WebContext>()));
builder.Services.AddScoped<IUserRepository>(x => new UserRepository(x.GetRequiredService<WebContext>()));
builder.Services.AddScoped<ITokenRepository>(x => new TokenRepository(x.GetRequiredService<WebContext>(), x.GetRequiredService<IUserRepository>()));
builder.Services.AddScoped<IBrandRepository>(x => new BrandRepository(x.GetRequiredService<WebContext>(), x.GetRequiredService<ICategoryRepository>()));
builder.Services.AddScoped<ICartRepository>(x => new CartRepository(x.GetRequiredService<WebContext>(), x.GetRequiredService<IProductRepository>()));

var app = builder.Build();
await SeedData.Seed(app);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(builder => builder.WithOrigins("https://localhost:5173", "https://localhost:5174")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
    IdentityModelEventSource.ShowPII = true;
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
