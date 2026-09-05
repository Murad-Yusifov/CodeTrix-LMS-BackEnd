using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.Lesson;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Data;

public static class SeedData
{
    public static void Seed(ApplicationDbContext context)
    {
        // =====================================================
        // 1. ROLES
        // =====================================================

        if (!context.Roles.Any())
        {
            var roles = new List<RoleModel>
            {
                new RoleModel
                {
                    RoleId = 1,
                    RoleName = "Student"
                },
                new RoleModel
                {
                    RoleId = 2,
                    RoleName = "Mentor"
                },
                new RoleModel
                {
                    RoleId = 3,
                    RoleName = "Admin"
                }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        // =====================================================
        // 2. USERS
        // =====================================================

        var admin = context.Users
            .FirstOrDefault(user => user.UserId == 1);

        if (admin == null)
        {
            admin = new UserModel
            {
                UserId = 1,
                UserName = "Admin",
                UserSurName = "CodeTrix",
                Email = "admin@codetrix.com",
                PhoneNumber = "+994501111111",
                PasswordHash = "Admin123!",
                RoleId = 3,
                GroupId = null
            };

            context.Users.Add(admin);
        }

        var mentor = context.Users
            .FirstOrDefault(user => user.UserId == 2);

        if (mentor == null)
        {
            mentor = new UserModel
            {
                UserId = 2,
                UserName = "John",
                UserSurName = "Mentor",
                Email = "mentor@codetrix.com",
                PhoneNumber = "+994502222222",
                PasswordHash = "Mentor123!",
                RoleId = 2,
                GroupId = null
            };

            context.Users.Add(mentor);
        }

        var student1 = context.Users
            .FirstOrDefault(user => user.UserId == 3);

        if (student1 == null)
        {
            student1 = new UserModel
            {
                UserId = 3,
                UserName = "Murad",
                UserSurName = "Yusifov",
                Email = "murad@codetrix.com",
                PhoneNumber = "+994503333333",
                PasswordHash = "Student123!",
                RoleId = 1,
                GroupId = null
            };

            context.Users.Add(student1);
        }

        var student2 = context.Users
            .FirstOrDefault(user => user.UserId == 4);

        if (student2 == null)
        {
            student2 = new UserModel
            {
                UserId = 4,
                UserName = "Ali",
                UserSurName = "Mammadov",
                Email = "ali@codetrix.com",
                PhoneNumber = "+994504444444",
                PasswordHash = "Student123!",
                RoleId = 1,
                GroupId = null
            };

            context.Users.Add(student2);
        }

        var student3 = context.Users
            .FirstOrDefault(user => user.UserId == 5);

        if (student3 == null)
        {
            student3 = new UserModel
            {
                UserId = 5,
                UserName = "Leyla",
                UserSurName = "Hasanova",
                Email = "leyla@codetrix.com",
                PhoneNumber = "+994505555555",
                PasswordHash = "Student123!",
                RoleId = 1,
                GroupId = null
            };

            context.Users.Add(student3);
        }

        context.SaveChanges();
        

        // =====================================================
        // 3. GROUPS
        // =====================================================

        var group = context.Groups
            .FirstOrDefault(group => group.GroupId == 1);

        if (group == null)
        {
            group = new GroupModel
            {
                GroupId = 1,
                GroupName = "Frontend Group",
                DateOfCreated = DateTime.UtcNow,
                CreatedByMentorId = mentor.UserId
            };

            context.Groups.Add(group);
            context.SaveChanges();
        }

        // =====================================================
        // 4. ASSIGN STUDENTS TO GROUP
        // =====================================================

        var students = context.Users
            .Where(user =>
                user.UserId == 3 ||
                user.UserId == 4 ||
                user.UserId == 5)
            .ToList();

        foreach (var student in students)
        {
            student.GroupId = 1;
        }

        context.SaveChanges();

        // =====================================================
        // 5. TASKS
        // =====================================================

        if (!context.Tasks.Any())
        {
            var tasks = new List<TaskModel>
            {
                new TaskModel
                {
                    TaskId = 1,
                    TaskName = "Create React Login Page",
                    DeadLine = DateTime.UtcNow.AddDays(7),
                    DateTime = DateTime.UtcNow,
                    TaskStatus = "NotAssigned",
                    MentorComment =
                        "Create a responsive login page using React and SCSS.",
                    GroupId = 1
                },

                new TaskModel
                {
                    TaskId = 2,
                    TaskName = "Create React Dashboard",
                    DeadLine = DateTime.UtcNow.AddDays(14),
                    DateTime = DateTime.UtcNow,
                    TaskStatus = "NotAssigned",
                    MentorComment =
                        "Create a dashboard layout using React.",
                    GroupId = 1
                }
            };

            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }

        // =====================================================
        // 6. STUDENT TASKS
        // =====================================================

        if (!context.StudentTasks.Any())
        {
            var studentTasks = new List<StudentTaskModel>
            {
                new StudentTaskModel
                {
                    StudentTaskId = 1,
                    StudentId = 3,
                    TaskId = 1,
                    Status = StudentTaskStatus.Completed,
                    SubmittedAt = DateTime.UtcNow.AddDays(-2),
                    CompletedAt = DateTime.UtcNow.AddDays(-1),
                    MentorComment = "Good work. The UI is clean."
                },

                new StudentTaskModel
                {
                    StudentTaskId = 2,
                    StudentId = 4,
                    TaskId = 1,
                    Status = StudentTaskStatus.Pending,
                    SubmittedAt = DateTime.UtcNow
                },

                new StudentTaskModel
                {
                    StudentTaskId = 3,
                    StudentId = 5,
                    TaskId = 1,
                    Status = StudentTaskStatus.Rejected,
                    SubmittedAt = DateTime.UtcNow.AddDays(-1),
                    MentorComment =
                        "Please improve the responsive design."
                },

                new StudentTaskModel
                {
                    StudentTaskId = 4,
                    StudentId = 3,
                    TaskId = 2,
                    Status = StudentTaskStatus.Pending,
                    SubmittedAt = DateTime.UtcNow
                },

                new StudentTaskModel
                {
                    StudentTaskId = 5,
                    StudentId = 4,
                    TaskId = 2,
                    Status = StudentTaskStatus.NotAssigned
                },

                new StudentTaskModel
                {
                    StudentTaskId = 6,
                    StudentId = 5,
                    TaskId = 2,
                    Status = StudentTaskStatus.NotAssigned
                }
            };

            context.StudentTasks.AddRange(studentTasks);
            context.SaveChanges();
        }

        // =====================================================
        // 7. LESSONS
        // =====================================================

        if (!context.Lessons.Any())
        {
            var lessons = new List<LessonModel>
            {
                new LessonModel
                {
                    LessonId = 1,
                    GroupId = 1,
                    LessonStarts = DateTime.UtcNow
                        .AddDays(1)
                        .Date
                        .AddHours(18),
                    LessonEnds = DateTime.UtcNow
                        .AddDays(1)
                        .Date
                        .AddHours(20),
                    LocationType = LocationType.Online,
                    Classroom = "https://zoom.us/j/123456789"
                },

                new LessonModel
                {
                    LessonId = 2,
                    GroupId = 1,
                    LessonStarts = DateTime.UtcNow
                        .AddDays(3)
                        .Date
                        .AddHours(18),
                    LessonEnds = DateTime.UtcNow
                        .AddDays(3)
                        .Date
                        .AddHours(20),
                    LocationType = LocationType.AtCourse,
                    Classroom = "Classroom 5"
                }
            };

            context.Lessons.AddRange(lessons);
            context.SaveChanges();
        }

        // =====================================================
        // 8. ATTENDANCE
        // =====================================================

        if (!context.StudentAttendances.Any())
        {
            var attendances = new List<AttendanceModel>
            {
                // Lesson 1
                new AttendanceModel
                {
                    AttendanceId = 1,
                    StudentId = 3,
                    LessonId = 1,
                    Status = AttendanceStatus.Present,
                    RecordedAt = DateTime.UtcNow
                },

                new AttendanceModel
                {
                    AttendanceId = 2,
                    StudentId = 4,
                    LessonId = 1,
                    Status = AttendanceStatus.Absent,
                    RecordedAt = DateTime.UtcNow
                },

                new AttendanceModel
                {
                    AttendanceId = 3,
                    StudentId = 5,
                    LessonId = 1,
                    Status = AttendanceStatus.Late,
                    RecordedAt = DateTime.UtcNow
                },

                // Lesson 2
                new AttendanceModel
                {
                    AttendanceId = 4,
                    StudentId = 3,
                    LessonId = 2,
                    Status = AttendanceStatus.Present,
                    RecordedAt = DateTime.UtcNow
                },

                new AttendanceModel
                {
                    AttendanceId = 5,
                    StudentId = 4,
                    LessonId = 2,
                    Status = AttendanceStatus.Present,
                    RecordedAt = DateTime.UtcNow
                },

                new AttendanceModel
                {
                    AttendanceId = 6,
                    StudentId = 5,
                    LessonId = 2,
                    Status = AttendanceStatus.Absent,
                    RecordedAt = DateTime.UtcNow
                }
            };

            context.StudentAttendances.AddRange(attendances);
            context.SaveChanges();
        }
    }
}