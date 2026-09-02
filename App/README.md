# 🚀 CodeTrix-LMS Backend (.NET 10 C#) — System Architecture & Implementation Guide

This document serves as the primary technical specification and implementation guide for the **CodeTrix-LMS** backend platform.

---

## 🛠 Tech Stack
* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Language:** C# 13
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core (`Npgsql.EntityFrameworkCore.PostgreSQL`)
* **Authentication & Security:** JWT Authentication, BCrypt (Password Hashing)
* **Architecture:** Feature-by-Folder (Modular Layered Flow)
* **API Documentation & Testing:** Swagger / OpenAPI, Postman
* **Version Control:** Git / GitHub

---

## 🏗 Folder Structure

The project follows a **Feature-by-Folder** organizational pattern:

```text
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
    │   └── DbInitializer.cs
    │
    ├── Users/
    │   ├── UserModel.cs
    │   ├── UserService.cs
    │   ├── IUserService.cs
    │   ├── UserDTO/
    │   │   ├── CreateUserDto.cs
    │   │   ├── UpdateUserDto.cs
    │   │   └── UserResponseDto.cs
    │   ├── UserRepository.cs
    │   ├── IUserRepository.cs
    │   └── UserController.cs
    │
    ├── Tasks/
    │   ├── TaskModel.cs
    │   ├── TaskService.cs
    │   ├── ITaskService.cs
    │   ├── TaskDTO/
    │   │   ├── CreateTaskDto.cs
    │   │   ├── UpdateTaskDto.cs
    │   │   └── TaskResponseDto.cs
    │   ├── TaskRepository.cs
    │   ├── ITaskRepository.cs
    │   └── TaskController.cs
    │
    ├── LessonSchedule/
    │   ├── LessonModel.cs
    │   ├── LessonService.cs
    │   ├── ILessonService.cs
    │   ├── LessonDTO/
    │   │   ├── CreateLessonDto.cs
    │   │   ├── UpdateLessonDto.cs
    │   │   └── LessonResponseDto.cs
    │   ├── LessonRepository.cs
    │   ├── ILessonRepository.cs
    │   └── LessonController.cs
    │
    ├── Group/
    │   ├── GroupModel.cs
    │   ├── GroupService.cs
    │   ├── IGroupService.cs
    │   ├── GroupDTO/
    │   │   ├── CreateGroupDto.cs
    │   │   ├── UpdateGroupDto.cs
    │   │   └── GroupResponseDto.cs
    │   ├── GroupRepository.cs
    │   ├── IGroupRepository.cs
    │   └── GroupController.cs
    │
    └── Authentication/
        ├── Authentication.cs
        ├── HashedSystem.cs
        ├── IAuthenticationService.cs
        ├── AuthenticationService.cs
        └── AuthenticationController.cs

```

---

## 🏛 Architecture & Data Flow

Each module follows a strict layered request/response lifecycle:

```text
Users/ (or any feature module)
│
├── UserController           <-- Handles HTTP Requests & validates DTOs
│       │
│       ▼
├── UserService              <-- Business Logic, Hashing & Rules
│       │
│       ▼
├── UserRepository           <-- Prepares Entity Framework Core queries
│       │
│       ▼
└── ApplicationDbContext    <-- Interacts directly with PostgreSQL

```

### 🔄 Request Lifecycle Example:

```text
POST /api/users
      │
      ▼
UserController
      │
      ▼
IUserService / UserService
      │
      ▼
IUserRepository / UserRepository
      │
      ▼
ApplicationDbContext
      │
      ▼
PostgreSQL Database

```

---

## 🗄 Database Design & Schema (PostgreSQL)

```sql
-- 1. USERS TABLE (Only Admin creates records here)
CREATE TABLE "Users" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "FullName" VARCHAR(150) NOT NULL,
    "Email" VARCHAR(150) UNIQUE NOT NULL,
    "PasswordHash" TEXT NOT NULL,
    "Role" INT NOT NULL, -- 1: Admin, 2: Mentor, 3: Student
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. GROUPS TABLE
CREATE TABLE "Groups" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name" VARCHAR(100) NOT NULL,
    "MentorId" UUID REFERENCES "Users"("Id") ON DELETE SET NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 3. STUDENT-GROUPS (Many-to-Many Junction Table)
CREATE TABLE "StudentGroups" (
    "StudentId" UUID REFERENCES "Users"("Id") ON DELETE CASCADE,
    "GroupId" UUID REFERENCES "Groups"("Id") ON DELETE CASCADE,
    PRIMARY KEY ("StudentId", "GroupId")
);

-- 4. LESSON SCHEDULES TABLE
CREATE TABLE "Lessons" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "GroupId" UUID REFERENCES "Groups"("Id") ON DELETE CASCADE,
    "Title" VARCHAR(200) NOT NULL,
    "LessonDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "RoomOrLink" VARCHAR(255)
);

-- 5. TASKS TABLE
CREATE TABLE "Tasks" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "GroupId" UUID REFERENCES "Groups"("Id") ON DELETE CASCADE,
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "Deadline" TIMESTAMP WITH TIME ZONE NOT NULL
);

-- 6. TASK SUBMISSIONS (State Machine)
CREATE TABLE "TaskSubmissions" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "TaskId" UUID REFERENCES "Tasks"("Id") ON DELETE CASCADE,
    "StudentId" UUID REFERENCES "Users"("Id") ON DELETE CASCADE,
    "SubmissionUrl" TEXT NOT NULL,
    "Status" INT NOT NULL, -- 0: Pending, 1: Approved, 2: FixIt, 3: Rejected
    "SubmittedAt" TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

```

### 🔑 Key Database Rules:

1. **No Public Registration:** Students and Mentors are registered directly by the Admin via `POST /api/users`.
2. **Student - Group Association (M:N):** Handled via `StudentGroups` junction table to allow flexible multi-group assignments.
3. **Task Submissions (State Machine):** Submissions transition through `0: Pending` → `1: Approved` | `2: FixIt` | `3: Rejected`. Pending submissions are locked from duplicate resubmission until reviewed.

---

## 🌐 REST API Endpoints

### 🔐 1. Authentication (`/api/auth`)

#### 🔹 `POST /api/auth/login`

* **Access:** Public (Admin, Mentor, Student)
* **Request:**

```json
{
  "email": "student1@codetrix.edu",
  "password": "AdminSetPassword123"
}

```

* **Response (200 OK):**

```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66aa14",
    "fullName": "Əli Əliyev",
    "role": 3
  }
}

```

---

### 👤 2. User Management (`/api/users`)

#### 🔹 `POST /api/users`

* **Access:** Admin Only
* **Request:**

```json
{
  "fullName": "Leyla Qasımova",
  "email": "leyla@codetrix.edu",
  "password": "InitialTempPassword2026",
  "role": 3
}

```

* **Response (201 Created):**

```json
{
  "success": true,
  "message": "User created successfully.",
  "data": {
    "id": "7cb11f64-1234-4562-b3fc-2c963f66bb22",
    "fullName": "Leyla Qasımova",
    "email": "leyla@codetrix.edu",
    "role": 3
  }
}

```

#### 🔹 `GET /api/users`

* **Access:** Admin Only
* **Response (200 OK):** List of all users.

---

### 👥 3. Group Management (`/api/group`)

#### 🔹 `POST /api/group`

* **Access:** Admin
* **Request:**

```json
{
  "name": "Fullstack .NET Batch 1",
  "mentorId": "7cb11f64-1234-4562-b3fc-2c963f66bb22"
}

```

#### 🔹 `POST /api/group/add-student`

* **Access:** Admin / Mentor
* **Request:**

```json
{
  "studentId": "3fa85f64-5717-4562-b3fc-2c963f66aa14",
  "groupId": "8fa85f64-5717-4562-b3fc-2c963f66cc33"
}

```

---

### 📅 4. Lesson Schedule (`/api/lessons`)

#### 🔹 `GET /api/lessons/group/{groupId}`

* **Access:** Authorized Users
* **Response (200 OK):** List of scheduled lessons for the target group.

#### 🔹 `POST /api/lessons`

* **Access:** Admin / Mentor
* **Request:** Create a new lesson schedule entry.

---

### 📝 5. Tasks & Submissions (`/api/tasks`)

#### 🔹 `POST /api/tasks`

* **Access:** Mentor / Admin
* **Request:** Add a task to a group.

#### 🔹 `POST /api/tasks/submit`

* **Access:** Student
* **Request:** Toggles state to `Pending`.

```json
{
  "taskId": "task-uuid-1",
  "studentId": "3fa85f64-5717-4562-b3fc-2c963f66aa14",
  "submissionUrl": "[https://github.com/student/my-project](https://github.com/student/my-project)"
}

```

#### 🔹 `PUT /api/tasks/review`

* **Access:** Mentor
* **Request:** Review a submission (`1: Approved`, `2: FixIt`, `3: Rejected`).

---

## 📋 Task Splitting Roadmap

### 🔹 PHASE 1: Data & Infrastructure Setup

* **Task 1.1:** Initialize .NET 10 Web API project and install dependencies (`Npgsql.EntityFrameworkCore.PostgreSQL`, `BCrypt.Net-Next`, `Microsoft.AspNetCore.Authentication.JwtBearer`).
* **Task 1.2:** Configure `ApplicationDbContext.cs`, `DbInitializer.cs`, and `SeedData.cs`.
* **Task 1.3:** Configure PostgreSQL connection string in `appsettings.json` and run initial EF Core migration.

### 🔹 PHASE 2: Authentication & Admin User Creation Module

* **Task 2.1:** Implement `UserModel.cs` and creation DTOs (`CreateUserDto`, `UserResponseDto`).
* **Task 2.2:** Implement BCrypt password hashing in `HashedSystem.cs`.
* **Task 2.3:** Create `POST /api/users` endpoint for Admin user creation.
* **Task 2.4:** Implement `POST /api/auth/login` endpoint with JWT token generation (no public register endpoint).

### 🔹 PHASE 3: Group & Student Association Module

* **Task 3.1:** Implement `GroupModel.cs` and `StudentGroup` junction entity.
* **Task 3.2:** Define Group DTOs (`CreateGroupDto`, `GroupResponseDto`).
* **Task 3.3:** Implement `GroupRepository` and `GroupService` methods for student assignment and removal.
* **Task 3.4:** Expose `/api/group` endpoints in `GroupController`.

### 🔹 PHASE 4: Lesson Schedule Module

* **Task 4.1:** Build `LessonModel.cs` and related DTOs.
* **Task 4.2:** Implement scheduling logic in `LessonRepository` and `LessonService`.
* **Task 4.3:** Expose scheduling endpoints via `LessonController`.

### 🔹 PHASE 5: Tasks & Submission State Machine Module

* **Task 5.1:** Define `TaskModel.cs`, `TaskSubmissionModel.cs`, and submission status Enum.
* **Task 5.2:** Build Task DTOs.
* **Task 5.3:** Implement status transition rules (locking pending submissions, handling fix-it state) in `TaskService`.
* **Task 5.4:** Complete `TaskController` endpoints and verify with Swagger/Postman.

---

## ⚡ Quick Start

1. **Clone the repository:**
```bash
git clone [https://github.com/codetrix/BackEndCodeTrix.git](https://github.com/codetrix/BackEndCodeTrix.git)
cd BackEndCodeTrix

```


2. **Configure database credentials in `appsettings.json`:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=codetrix_lms;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyForJwtTokenGeneration2026",
    "Issuer": "CodeTrixLMS",
    "Audience": "CodeTrixEcosystem"
  }
}

```


3. **Apply EF Core Migrations:**
```bash
dotnet ef database update

```


4. **Run the API:**
```bash
dotnet run

```


5. **Swagger UI:** Navigating to `http://localhost:5000/swagger` will expose all active endpoints.

Example Structure: https://github.com/kawser2133/clean-structured-project/tree/development/Project.Infrastructure




<!-- EndPoints -->

/api
│
├── User
│   ├── GET    /api/User
│   ├── GET    /api/User/{id}
│   ├── POST   /api/User
│   ├── PUT    /api/User/{id}
│   ├── DELETE /api/User/{id}
│   └── GET    /api/User/group/{groupId} ?/Updated: api/user/id/group
│
├── Group
│   ├── GET    /api/Group
│   ├── GET    /api/Group/{id}
│   ├── POST   /api/Group
│   ├── PUT    /api/Group/{id}
│   └── DELETE /api/Group/{id}
│
├── Task
│   ├── GET    /api/Task
│   ├── GET    /api/Task/{id}
│   ├── GET    /api/Task/group/{groupId}
│   ├── POST   /api/Task
│   ├── PUT    /api/Task/{id}
│   └── DELETE /api/Task/{id}
│
├── StudentTask
│   ├── GET    /api/StudentTask
│   ├── GET    /api/StudentTask/{id}
│   ├── GET    /api/StudentTask/student/{studentId}
│   ├── POST   /api/StudentTask
│   ├── PUT    /api/StudentTask/{id}
│   └── DELETE /api/StudentTask/{id}
│
├── Lesson
│   ├── GET    /api/Lesson
│   ├── GET    /api/Lesson/{id}
│   ├── GET    /api/Lesson/group/{groupId}
│   ├── POST   /api/Lesson
│   ├── PUT    /api/Lesson/{id}
│   └── DELETE /api/Lesson/{id}
│
└── Role
    ├── GET    /api/Role
    └── GET    /api/Role/{id}

    {
  "sub": "15",
  "email": "mentor@example.com",
  "name": "John Smith",
  "role": "Mentor"
}