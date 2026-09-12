using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Course;
using BackEndCodeTrix.Src.CourseRequest;
using BackEndCodeTrix.Src.Auth;

namespace BackEndCodeTrix.Src.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<GroupModel> Groups { get; set; }

    public DbSet<TaskModel> Tasks { get; set; }

    public DbSet<LessonModel> Lessons { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<StudentTaskModel> StudentTasks { get; set; }
    public DbSet<AttendanceModel> StudentAttendances { get; set; }
    public DbSet<CourseModel> Courses { get; set; }
    public DbSet<CourseRequestModel> CourseRequests { get; set; }
    public DbSet<RefreshTokenModel> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<RoleModel>()
     .HasKey(r => r.RoleId);

        modelBuilder.Entity<RoleModel>()
            .Property(r => r.RoleId)
            .ValueGeneratedNever();

        // 👇 Tell EF Core what your Primary Key is (change 'Id' to your property name)
        modelBuilder.Entity<GroupModel>()
            .HasKey(g => g.GroupId); // or g.GroupId, etc.

        modelBuilder.Entity<LessonModel>()
         .HasKey(l => l.LessonId);

        // modelBuilder.Entity<StudentTaskModel>()
        // .HasKey(l => l.StudentTaskId);

        modelBuilder.Entity<StudentTaskModel>()
       .HasOne(st => st.Student)
       .WithMany()
       .HasForeignKey(st => st.StudentId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentTaskModel>()
            .HasOne(st => st.Task)
            .WithMany(task => task.StudentTasks)
            .HasForeignKey(st => st.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskModel>()
        .HasKey(l => l.TaskId);


        modelBuilder.Entity<UserModel>()
        .HasKey(l => l.UserId);


        // Your existing relationship code
        modelBuilder.Entity<GroupModel>()
            .HasOne(g => g.CreatedByMentor)
            .WithMany(u => u.CreatedGroups)
            .HasForeignKey(g => g.CreatedByMentorId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<GroupModel>()
            .HasOne(g => g.Course)
            .WithMany(c => c.Groups)
            .HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Attendance Existing Relationship code
        modelBuilder.Entity<AttendanceModel>()
     .HasKey(a => a.AttendanceId);

        modelBuilder.Entity<AttendanceModel>()
            .HasOne(a => a.Student)
            .WithMany(u => u.Attendances)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AttendanceModel>()
            .HasOne(a => a.Lesson)
            .WithMany()
            .HasForeignKey(a => a.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseModel>()
       .HasKey(x => x.CourseId);


        modelBuilder.Entity<CourseRequestModel>()
            .HasKey(x => x.CourseRequestId);

        modelBuilder.Entity<CourseRequestModel>()
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefreshTokenModel>()
            .HasKey(x => x.RefreshTokenId);

        modelBuilder.Entity<RefreshTokenModel>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
