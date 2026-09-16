using System.Text;
using BackEndCodeTrix.Mapping;
using BackEndCodeTrix.Src.Auth;
using BackEndCodeTrix.Src.Course;
using BackEndCodeTrix.Src.CourseRequest;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Lesson;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.Tasks.Students;
using BackEndCodeTrix.Src.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEndCodeTrix.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));

        // AutoMapper
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // =========================
        // Repositories & Services
        // =========================

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

        // Attendance
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IAttendanceService, AttendanceService>();

        // Courses
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();

        // Course Requests
        services.AddScoped<ICourseRequestRepository, CourseRequestRepository>();
        services.AddScoped<ICourseRequestService, CourseRequestService>();

        services.AddScoped<PasswordHasher>();

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddCors(options =>
            {
                options.AddPolicy("AdminPanel", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        // =========================
        // Authentication
        // =========================

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration["Jwt:Key"]
                            ?? throw new InvalidOperationException(
                                "JWT Key tapılmadı.")
                        )
                    )
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("JWT ERROR: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

        // Authorization
        services.AddAuthorization();

        return services;
    }
}