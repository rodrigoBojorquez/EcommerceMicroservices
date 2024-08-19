using Ecommerce.SharedLibrary.DependencyInjection;
using OrderApi.Infrastructure.DependencyInjection;
using OrderApi.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseSharedPolicies();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();