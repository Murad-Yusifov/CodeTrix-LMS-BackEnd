// Program.cs
using BackEndCodeTrix.Extensions;
using BackEndCodeTrix.Src.Data;


var builder = WebApplication.CreateBuilder(args);

// Framework UI / API Tools
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Custom Separated Infrastructure Services
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Run Database Seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.Seed(context);
}

// Middleware Request Pipeline
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
