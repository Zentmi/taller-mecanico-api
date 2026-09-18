
using System.Text.Json.Serialization;
using TallerMecanico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TallerMecanico.Models;
using TallerMecanico.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TallerMecanico.Configuration;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<TallerMecanicoContext>(options => options.UseSqlServer
(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)
            )
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UnidadService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el JWT obtenido mediante /api/Auth/login."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
builder.Services.AddScoped<OrdenServicioService>();
builder.Services.AddScoped<ServicioService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
