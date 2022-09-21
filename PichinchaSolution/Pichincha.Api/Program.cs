using Pichincha.Domain;
using Pichincha.Domain.Common;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.DataAccess;
using Pichincha.Infrastructure.DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<ISqlDataContext, MainContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBaseDomain<,>), typeof(BaseDomain<,>));

builder.Services.AddScoped<IClientDomain, ClientDomain>();
builder.Services.AddScoped<IMovementDomain, MovementDomain>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


// AutoMapper
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
