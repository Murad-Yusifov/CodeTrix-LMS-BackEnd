using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Course;
using BackEndCodeTrix.Src.CourseRequest;
using BackEndCodeTrix.Src.Auth;

namespace BackEndCodeTrix.Src.Data;

public static class SeedData
{
    public static void Seed(ApplicationDbContext context, PasswordHasher hasher)
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
                PasswordHash = hasher.Hash("Admin123!"),
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
                PasswordHash = hasher.Hash("Mentor123!"),
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
                PasswordHash = hasher.Hash("Student123!"),
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
                PasswordHash = hasher.Hash("Student123!"),
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
                PasswordHash = hasher.Hash("Student123!"),
                RoleId = 1,
                GroupId = null
            };

            context.Users.Add(student3);
        }

        context.SaveChanges();

        // =====================================================
        // 3. COURSES
        // =====================================================

        if (!context.Courses.Any())
        {
            var courses = new List<CourseModel>
            {
                new CourseModel
                {
                    CourseId = 1,
                    Title = "Frontend Development",
                    Slug = "frontend-development",
                    Description =
                        "Learn HTML, CSS, JavaScript, React and modern frontend development.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },

                new CourseModel
                {
                    CourseId = 2,
                    Title = "Backend Development with C#",
                    Slug = "backend-development-csharp",
                    Description =
                        "Learn C#, ASP.NET Core Web API, Entity Framework Core and databases.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },

                new CourseModel
                {
                    CourseId = 3,
                    Title = "React Development",
                    Slug = "react-development",
                    Description =
                        "Learn React, routing, state management and building modern web applications.",
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
        }

        // =====================================================
        // 4.1. COURSE REQUESTS
        // =====================================================

        if (!context.CourseRequests.Any())
        {
            var courseRequests = new List<CourseRequestModel>
                {
                    new CourseRequestModel
                    {
                        CourseRequestId = 1,
                        CourseId = 1,
                        Name = "Ahmed Aliyev",
                        Email = "ahmed@example.com",
                        Phone = "+994501234567",
                        Message = "I want to join the Frontend Development course.",
                        Status = CourseRequestStatus.Pending,
                        CreatedAt = DateTime.UtcNow.AddDays(-3),
                        UpdatedAt = null
                    },

                    new CourseRequestModel
                    {
                        CourseRequestId = 2,
                        CourseId = 2,
                        Name = "Nigar Hasanli",
                        Email = "nigar@example.com",
                        Phone = "+994502345678",
                        Message = "I am interested in learning ASP.NET Core and C#.",
                        Status = CourseRequestStatus.Contacted,
                        CreatedAt = DateTime.UtcNow.AddDays(-5),
                        UpdatedAt = DateTime.UtcNow.AddDays(-2)
                    },

                    new CourseRequestModel
                    {
                        CourseRequestId = 3,
                        CourseId = 3,
                        Name = "Elvin Mammadov",
                        Email = "elvin@example.com",
                        Phone = "+994503456789",
                        Message = "I would like to join the React course.",
                        Status = CourseRequestStatus.Approved,
                        CreatedAt = DateTime.UtcNow.AddDays(-7),
                        UpdatedAt = DateTime.UtcNow.AddDays(-1)
                    },

                    new CourseRequestModel
                    {
                        CourseRequestId = 4,
                        CourseId = 2,
                        Name = "Aysel Karimova",
                        Email = "aysel@example.com",
                        Phone = "+994504567890",
                        Message = "Please contact me about the Backend Development course.",
                        Status = CourseRequestStatus.Rejected,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        UpdatedAt = DateTime.UtcNow.AddDays(-6)
                    }
                };

            context.CourseRequests.AddRange(courseRequests);
            context.SaveChanges();
        }

        // =====================================================
        // 4. GROUPS
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
                CreatedByMentorId = mentor.UserId,
                CourseId = 1
            };

            context.Groups.Add(group);
            context.SaveChanges();
        }

        // =====================================================
        // 5. ASSIGN STUDENTS TO GROUP
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
        // 6. TASKS
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
        // 7. STUDENT TASKS
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
        // 8. LESSONS
        // =====================================================

        if (!context.Lessons.Any())
        {
            var lessons = new List<LessonModel>
            {
                new LessonModel
                {
                    LessonId = 1,
                    GroupId = 1,
                    LessonName = "JS Essentials",

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
                    LessonName = "React Essentials",

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
        // 9. ATTENDANCE
        // =====================================================

        if (!context.StudentAttendances.Any())
        {
            var attendance1 = new AttendanceModel
            {
                AttendanceId = 1,
                LessonId = 1,
                GroupId = 1,
                RecordedAt = DateTime.UtcNow
            };

            var attendance2 = new AttendanceModel
            {
                AttendanceId = 2,
                LessonId = 2,
                GroupId = 1,
                RecordedAt = DateTime.UtcNow
            };

            context.StudentAttendances.AddRange(
                attendance1,
                attendance2
            );

            context.SaveChanges();

            // =================================================
            // STUDENTS WHO ATTENDED
            // =================================================

            var attendanceStudents = new List<AttendanceStudentModel>
    {
        // ---------------------------------------------
        // Lesson 1
        // Murad   → Present
        // Leyla   → Present
        // Ali     → Absent (not included)
        // ---------------------------------------------

        new AttendanceStudentModel
        {
            AttendanceStudentId = 1,
            AttendanceId = 1,
            StudentId = 3
        },

        new AttendanceStudentModel
        {
            AttendanceStudentId = 2,
            AttendanceId = 1,
            StudentId = 5
        },

        // ---------------------------------------------
        // Lesson 2
        // Murad   → Present
        // Ali     → Present
        // Leyla   → Absent (not included)
        // ---------------------------------------------

        new AttendanceStudentModel
        {
            AttendanceStudentId = 3,
            AttendanceId = 2,
            StudentId = 3
        },

        new AttendanceStudentModel
        {
            AttendanceStudentId = 4,
            AttendanceId = 2,
            StudentId = 4
        }
    };

            context.AttendanceStudents.AddRange(attendanceStudents);
            context.SaveChanges();
        }
    }
}