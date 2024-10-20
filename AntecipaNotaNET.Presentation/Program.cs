using System.Reflection;
using AntecipaNotaNET.Application.Services;
using AntecipaNotaNET.Domain.Interfaces.Repositories;
using AntecipaNotaNET.Domain.Interfaces.Services;
using AntecipaNotaNET.Infrastructure.Context;
using AntecipaNotaNET.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Serviço para antecipação de recebíveis.",
            Description = "API para antecipação de recebíveis de notas.",
            Version = "1.0.0",
            Contact = new OpenApiContact
            {
                Email = "gabrielgbr.contato@gmail.com",
                Name = "Gabriel Silva",
                Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://www.mit.edu/~amini/LICENSE.md")
            }
        }
    );

    var arquivoXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var caimnhoXml = Path.Combine(AppContext.BaseDirectory, arquivoXml);
    c.IncludeXmlComments(caimnhoXml);
});

builder.Services.AddScoped<IRepositoryCarrinho, RepositoryCarrinho>();

builder.Services.AddScoped<IRepositoryEmpresa, RepisotoryEmpresa>();

builder.Services.AddScoped<IRepositoryNotaFiscal, RepositoryNotaFiscal>();

builder.Services.AddScoped<IServiceRecebivel, ServiceRecebivel>();

builder.Services.AddDbContext<ApplicationDbContext>(
    e => e.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")),
    ServiceLifetime.Singleton
);

var app = builder.Build();

using var context = app
    .Services.CreateScope()
    .ServiceProvider.GetRequiredService<ApplicationDbContext>();

context.Database.Migrate();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço Recebivel");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
