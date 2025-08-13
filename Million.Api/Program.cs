using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Million.Api.Tests")]
var builder = WebApplication.CreateBuilder(args);

// configure MongoDb settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoContext>();

// register repository & service
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<PropertyImageService>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
builder.Services.AddScoped<PropertyTraceService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();
app.UseHttpsRedirection();

// run migrations at startup
using (var scope = app.Services.CreateScope())
{
    var migrator = new MongoMigrations(scope.ServiceProvider.GetRequiredService<MongoContext>());
    await migrator.RunMigrationsAsync();
}

app.Run();
