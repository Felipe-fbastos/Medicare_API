using Medicare_API.Controller;
using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

        // Registra o DataContext com o contêiner de serviços
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SomeeConnection")));

        // Outros serviços e configurações
        builder.Services.AddControllers();

// Adiciona os serviços necessários para controllers
builder.Services.AddControllers();

// Configuração do Swagger (se estiver usando)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita o roteamento e mapeia as controllers
app.UseRouting();
app.MapControllers();  // Mapeia as controllers para que a API saiba qual caminho seguir

app.Run();
