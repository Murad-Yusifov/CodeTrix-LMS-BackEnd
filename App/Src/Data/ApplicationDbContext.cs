using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Attendance;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<RoleModel>()
        .HasKey(l => l.RoleId);

        modelBuilder.Entity<UserModel>()
        .HasKey(l => l.UserId);


        // Your existing relationship code
        modelBuilder.Entity<GroupModel>()
            .HasOne(g => g.CreatedByMentor)
            .WithMany(u => u.CreatedGroups)
            .HasForeignKey(g => g.CreatedByMentorId)
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
    }
}
