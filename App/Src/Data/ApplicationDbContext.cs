using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 👇 Tell EF Core what your Primary Key is (change 'Id' to your property name)
        modelBuilder.Entity<GroupModel>()
            .HasKey(g => g.GroupId); // or g.GroupId, etc.

        modelBuilder.Entity<LessonModel>()
         .HasKey(l => l.LessonId);

        modelBuilder.Entity<StudentTaskModel>()
        .HasKey(l => l.StudentTaskId);

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
    }
}
