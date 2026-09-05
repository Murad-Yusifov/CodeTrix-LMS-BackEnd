using BackEndCodeTrix.Mapping;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Lesson;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.Tasks.Students;
using BackEndCodeTrix.Src.Users;
using Microsoft.EntityFrameworkCore;

namespace BackEndCodeTrix.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Database Configuration (SQLite)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));

        // 2. AutoMapper Setup
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // 3. Application Services & Repositories

        // Users
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        // Groups
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IGroupService, GroupService>();

        // Tasks
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();

        // Student Tasks
        services.AddScoped<IStudentTaskRepository, StudentTaskRepository>();

        services.AddScoped<IStudentTaskService, StudentTaskService>();

        // Lessons
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ILessonService, LessonService>();

        return services;
    }
}
