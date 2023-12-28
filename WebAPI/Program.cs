using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers.Autofac;
using DataAccess.Concrete;
using Core.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

builder.Services.AddDbContext<FonContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
//
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(scheme =>
{
    scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = ctx =>
        {
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = ctx =>
        {
            ctx.NoResult();
            ctx.Response.StatusCode = 500;
            ctx.Response.ContentType = "text/plain";
            ctx.Response.WriteAsync(ctx.Exception.ToString()).Wait();
            return Task.CompletedTask;
        },
        OnChallenge = ctx =>
        {
            return Task.CompletedTask;
        },
        OnMessageReceived = ctx =>
        {
            return Task.CompletedTask;
        }
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
app.UseDefaultFiles();

//
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
  //  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
    //RequestPath = new PathString("/wwwroot/Resources")
//});

app.UseAuthorization();

app.MapControllers();

app.Run();