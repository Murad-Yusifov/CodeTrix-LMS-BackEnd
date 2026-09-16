// Program.cs
using BackEndCodeTrix.Extensions;
using BackEndCodeTrix.Src.Auth;
using BackEndCodeTrix.Src.Data;


var builder = WebApplication.CreateBuilder(args);

// Framework UI / API Tools
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Adding Swagger 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Custom Separated Infrastructure Services
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Run Database Seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<PasswordHasher>();

    SeedData.Seed(context, hasher);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
   {
       options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
       options.RoutePrefix = string.Empty;
   });
}

// Middleware Request Pipeline
app.UseHttpsRedirection();

app.UseCors("AdminPanel");

app.UseAuthentication();   // MUST come before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
