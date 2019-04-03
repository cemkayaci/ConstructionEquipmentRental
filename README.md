# Project - Construction Equipment Rental

## Overview of Used Technologies 

### Backend
- .Net Core Console App
- Autofac
- AutoMapper
- EntityFrameworkCore(Sql)
- Newtonsoft.JSON
- NServiceBus
- NServiceBus.Logging

### Frontend
- Microsoft ASP.NET Core MVC
- Bootstrap
- AutoMapper
- EntityFrameworkCore(InMemory)
- Microsoft Extensions Logging
- NServiceBus
- Newtonsoft.JSON

## Using Instructions
**1. Restoring Nuget Packages**
- use dotnet restore command in command prompt or use Nuget Package Manager in Tools menu.

**2. Change Sql connection string in Backend project**
- Type your own sql connection string to DefaultConnection node in Backend appsettings.json file.
  (The sql user which you would define in your SQL connection, it must have authority to create a database in SQL Server. 
  Application would create a database and seed data itself after first run)

**3. Set multiple startup projects**
  - In Solution Explorer, select the solution (the top node).
  - Choose the solution node's context (right-click) menu and then choose Properties. 
    The Solution Property Pages dialog box appears.
  - Expand the Common Properties node, and choose Startup Project.
  - Click Multiple startup projects radio button.
  - Choose both Backend and Frontend project actions "Start".

**4. Run Solution**
