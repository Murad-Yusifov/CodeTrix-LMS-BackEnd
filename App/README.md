BackEndCodeTrix/
│
├── Program.cs
├── appsettings.json
│
└── Src/
    │
    ├── Data/
    │   ├── ApplicationDbContext.cs
    │   ├── SeedData.cs
    |   └── DbInitializer.cs
    ├── Users/
    │   │
    │   ├── UserModel.cs
    │   │
    │   ├── UserService.cs
    │   ├── IUserService.cs
    │   │
    │   ├── UserDTO/
    │   │   ├── CreateUserDto.cs
    │   │   ├── UpdateUserDto.cs
    │   │   └── UserResponseDto.cs
    │   │
    │   ├── UserRepository.cs
    │   ├── IUserRepository.cs
    │   │
    │   └── UserController.cs
    │
    ├── Tasks/
    │   │
    │   ├── TaskModel.cs
    │   │
    │   ├── TaskService.cs
    │   ├── ITaskService.cs
    │   │
    │   ├── TaskDTO/
    │   │   ├── CreateTaskDto.cs
    │   │   ├── UpdateTaskDto.cs
    │   │   └── TaskResponseDto.cs
    │   │
    │   ├── TaskRepository.cs
    │   ├── ITaskRepository.cs
    │   │
    │   └── TaskController.cs
    │
    ├── LessonSchedule/
    │   │
    │   ├── LessonModel.cs
    │   │
    │   ├── LessonService.cs
    │   ├── ILessonService.cs
    │   │
    │   ├── LessonDTO/
    │   │   ├── CreateLessonDto.cs
    │   │   ├── UpdateLessonDto.cs
    │   │   └── LessonResponseDto.cs
    │   │
    │   ├── LessonRepository.cs
    │   ├── ILessonRepository.cs
    │   │
    │   └── LessonController.cs
    │
    ├── Group/
    │   │
    │   ├── GroupModel.cs
    │   │
    │   ├── GroupService.cs
    │   ├── IGroupService.cs
    │   │
    │   ├── GroupDTO/
    │   │   ├── CreateGroupDto.cs
    │   │   ├── UpdateGroupDto.cs
    │   │   └── GroupResponseDto.cs
    │   │
    │   ├── GroupRepository.cs
    │   ├── IGroupRepository.cs
    │   │
    │   └── GroupController.cs
    │
    └── Authentication/
        │
        ├── Authentication.cs
        ├── HashedSystem.cs
        ├── IAuthenticationService.cs
        ├── AuthenticationService.cs
        └── AuthenticationController.cs


Neccarry Technologies:

.NET 10
<!-- ASP.NET Core Web API  -->
<!-- Entity Framework Core -->
<!-- PostgreSQL -->
Npgsql
JWT Authentication
BCrypt
REST API
Swagger
Postman
Git/GitHub



Architecture:

  Users/
│
├── UserController
│       ↓
├── UserService
│       ↓
├── UserRepository
│       ↓
└── Database


Example EndPoint:

POST /api/users
      ↓
UserController
      ↓
IUserService
      ↓
UserService
      ↓
IUserRepository
      ↓
UserRepository
      ↓
ApplicationDbContext
      ↓
PostgreSQL



