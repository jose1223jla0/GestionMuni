using gestionApi.Repository;
using gestionApi.Services;
using webApi.Repository;
using webApi.Repository.Interfaces;
using webApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
/*==========================================================================================
 Inicio de area de servicio
===========================================================================================  */
builder.Services.AddOpenApi();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IResidenteRepositorio,ResidenteRepositorio>();
builder.Services.AddTransient<IRegistroReciclajeRepositorio,RegistroReciclajeRepositorio>();


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

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
/*==========================================================================================
 fin de area de middleware
===========================================================================================  */
app.Run();

