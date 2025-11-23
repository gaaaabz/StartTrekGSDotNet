using Microsoft.EntityFrameworkCore;
using StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Application.Service;
using StartTrekGS.src.StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de Dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<ITrabalhoRepository, TrabalhoRepository>();
builder.Services.AddScoped<IEsp32Repository, Esp32Repository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

// Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ComentarioService>();
builder.Services.AddScoped<TrabalhoService>();
builder.Services.AddScoped<Esp32Service>();
builder.Services.AddScoped<TipoUsuarioService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ativa os controllers da API
app.MapControllers();

app.Run();
