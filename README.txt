A bug tracker written in Blazor .NET 5.0 C#
# Chad Tysinger 6/20/2021

Blazor Client, API


This is a simple bug tracker to demonstrate the power of Blazor.   This is a demo project written in a few hours in on weekend so the following
will need to be noted.  

* People - currently hard coded  // TODO  add People class, add CRUD functionality here.
* Password - the password is not secured so will need to move out of code and changed.
* No Refactoring has taken place, so lots of old code, comments are still visible. // TODO refactor and explain more complex areas
* Full rights to all users that use the page //TODO add login and security levels.



Currently using PostgreSQL 


NUGET SETUP:

Go into server folder first then:
From Command prompt
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package  Microsoft.EntityFrameworkCore.Design
dotnet add package  Microsoft.EntityFrameworkCore.SqlServer
dotnet add package  Npgsql.EntityFrameworkCore.PostgreSQL


Initial Data Migrations:
dotnet ef migrations add InitialMigration --context PGContext
dotnet ef database update --context PGContext


