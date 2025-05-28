# Date Management Project

An ASP.NET Core MVC application built with .NET 8 for managing holidays and calculating working days.

## Technologies

* **Framework:** ASP.NET Core MVC (.NET 8 SDK)
* **Language:** C#
* **Views:** Razor Pages (HTML, CSS)
* **ORM:** Entity Framework Core
* **Database:** SQL Server (manage via SQL Server Management Studio)
* **UI:** Bootstrap 5
* **IDE:** Visual Studio 2022

## Prerequisites

* .NET 8 SDK installed
* Visual Studio 2022 with ASP.NET and web development workload
* SQL Server instance (e.g., SQL Server Express) and SQL Server Management Studio

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/Stianh11/Date-Management.git
   cd Date-Management
   ```

2. **Update the database connection**

   * Open `appsettings.json` and set `ConnectionStrings:DefaultConnection` to point to your SQL Server instance. Example:

     ```jsonc
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=DateManagement;Trusted_Connection=True;"
     }
     ```

3. **Apply migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   * In Visual Studio: press **F5** (the default launch URL is [https://localhost:7072](https://localhost:7072) from `launchSettings.json`).
   * Or from a terminal:

     ```bash
     dotnet run
     ```

   Browse to the HTTPS address shown in the console (usually `https://localhost:7072`).

## Project Layout

```
/Controllers    → MVC controllers (HomeController, HolidaysController)
/Data           → EF Core DbContext and migrations
/Models         → Domain entities (Holiday, ErrorViewModel)
/ViewModels     → View models (DateRangeViewModel)
/Views          → Razor views and shared layouts
/wwwroot        → Static assets (Bootstrap, JS, CSS)
Program.cs      → Application startup and DI configuration
Date Management Project.csproj → Project file
```

## Usage

* **Holidays** page: view, add, edit, or delete holiday entries.
* **Working Days Calculator**: select start/end dates, choose country, include/exclude weekends, then calculate the number of working days.

## Notes

* Holidays from the public API (date.nager.at) are cached in the local database on first request.
* Ports and URLs are defined in `Properties/launchSettings.json`.
