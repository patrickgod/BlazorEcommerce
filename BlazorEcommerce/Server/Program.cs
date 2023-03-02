global using BlazorEcommerce.Shared;
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Server.Data;
global using BlazorEcommerce.Server.Services.ProductService;
global using BlazorEcommerce.Server.Services.CategoryService;
global using BlazorEcommerce.Server.Services.CartService;
global using BlazorEcommerce.Server.Services.AuthService;
global using BlazorEcommerce.Server.Services.OrderService;
global using BlazorEcommerce.Server.Services.PaymentService;

global using BlazorEcommerce.Server.Services.ProductTypeService;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BlazorEcommerce.Server.Services.PersonService;
using AutoMapper;
using BlazorEcommerce.Server.Mapper;
using BlazorEcommerce.Server.Services.ClassService;
using BlazorEcommerce.Server.Services;
using BlazorEcommerce.Server.Services.FinanceService;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTE0NzE2NUAzMjMwMmUzNDJlMzBNRE1HZkF2bFVzSGRFZTY2aVRSM01ES3VQN1c2anFkeE12aFJKZE5KV3VrPQ==");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<Lazy<IPersonService>>(provider => new Lazy<IPersonService>(provider.GetService<IPersonService>));
builder.Services.AddScoped<Lazy<PersonService>>();
builder.Services.AddScoped<FinanceService>();


builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IClassService, ClassService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{

    //ZS new line added
    endpoints.MapDefaultControllerRoute();

    //Original code
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");

});

//app.MapControllers();
//app.MapFallbackToFile("index.html");

app.Run();
