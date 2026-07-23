using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Group;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddScoped<IGroupService, GroupService>();


builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services
        .GetRequiredService<ApplicationDbContext>();

    SeedData.Seed(context);
}

// There's no swagger yet
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }



app.UseHttpsRedirection();

app.MapControllers();

app.Run();