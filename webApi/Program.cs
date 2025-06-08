using System.Text;
using gestionApi.Repository;
using gestionApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using webApi.Hubs;
using webApi.Repository;
using webApi.Repository.Interfaces;
using webApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
/*==========================================================================================
 Inicio de area de servicio
===========================================================================================  */
//builder.Services.AddOpenApi();
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")?.Split(",");
/*==========================================================================================
 Cors para angular
===========================================================================================  */
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        if (origenesPermitidos != null)
        {
            politica.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
        }
    });
});

builder.Services.AddSignalR();

/*==========================================================================================
 uso del jwt
===========================================================================================  */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Usado en desarrollo local
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IResidenteRepositorio, ResidenteRepositorio>();
builder.Services.AddTransient<IRegistroReciclajeRepositorio, RegistroReciclajeRepositorio>();
builder.Services.AddTransient<IAuthRepositorio, AuthRepositorio>();
builder.Services.AddTransient<IJwtServicio, JwtServicio>();

/*==========================================================================================
 fin de area de servicio
===========================================================================================  */
var app = builder.Build();
/*==========================================================================================
 Inicio de area de middleware
===========================================================================================  */

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
/*==========================================================================================
 fin de area de middleware
===========================================================================================  */
app.MapHub<UbicacionHub>("/ubicacionHub");
app.Run();
