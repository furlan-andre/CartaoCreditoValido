using CartaoCreditoValido.Application;
using CartaoCreditoValido.Infra;
using CartaoCreditoValido.Infra.Messaging;
using CartaoCreditoValido.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureDatabase(builder.Configuration);
builder.Services.AddMessageria(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddRespositorios();
builder.Services.AddMediator();

var app = builder.Build();

await app.Services.EnsureDatabaseUpdatedAsync();

using (var scope = app.Services.CreateScope())
{
    var topologyInitializer = scope.ServiceProvider.GetRequiredService<RabbitMqTopologyInitializer>();
    await topologyInitializer.InitializeAsync();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

