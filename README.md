# Levge.ConsistentResponse

[![NuGet Package](https://img.shields.io/nuget/v/Levge.ConsistentResponse.svg)](https://www.nuget.org/packages/Levge.ConsistentResponse)
[![Publish NuGet Package](https://github.com/levge-projects/Levge.ConsistentResponse/actions/workflows/main.yml/badge.svg)](https://github.com/levge-projects/Levge.ConsistentResponse/actions/workflows/main.yml)

📦 A lightweight package for standardizing API responses in .NET applications with built-in pagination support, global exception handling, and model state validation wrapping.

---

## ✨ Features

- Standardized `ApiResponse<T>` wrapper for all endpoints
- Automatic `ModelState` validation error wrapping
- Built-in pagination support via `PaginationData<T>`
- Global exception handler for custom & unexpected errors
- Slugify route transformer support
- Easy integration with a single line of code

---

## 📦 Installation

```bash
dotnet add package Levge.ConsistentResponse
```

---

## 🛠️ Usage

### 1. Register in `Program.cs`:

```csharp
builder.Services.AddLevgeConsistentResponse();
```

### 2. Add middleware in pipeline:

```csharp
app.UseLevgeConsistentResponse();
```

---

## 🧱 Models

- `ApiResponse<T>`: Unified API response format
- `PaginationData<T>`: Contains paged items and metadata
- `PaginationMeta`: Paging info (page, size, total, totalPages)
- `PaginationRequest`: Input model with page, pageSize, search
- `SortOption`, `FilterOption`: For dynamic filtering/sorting

---

## 🧹 Exception Handling

Uses [Levge.Exceptions](https://www.nuget.org/packages/Levge.Exceptions) for consistent error responses:
- `AppException` → 400 with `Message`
- `AppValidationException` → 400 with `Errors`
- Other → 500 with generic message and logging

---

## 🧪 Sample Output

```json
{
  "isSuccess": true,
  "message": "Data retrieved successfully.",
  "data": {
    "items": [...],
    "meta": {
      "page": 1,
      "pageSize": 10,
      "totalCount": 45,
      "totalPages": 5
    }
  }
}
```

---

## 🪄 Optional Route Slugify

PascalCase routes like `/GetAllUsers` → `/get-all-users`

```csharp
services.Configure<RouteOptions>(opt =>
{
    opt.LowercaseUrls = true;
    opt.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
});
```

---

## 📚 License

MIT © [Serdar ÖZKAN](https://www.linkedin.com/in/serdarozkan41)
