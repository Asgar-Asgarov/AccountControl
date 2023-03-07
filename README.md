# AccountControl
If you want to use project in your device :
>-Add proper ConnectionString (e.g your local server)
>-Update database (vs: update-database, 
vsc with cmd: dotnet ef database update)

######//MVC creating with cmd
1.crete folder
2.create solution file (   dotnet new sln --name MySolution   )
3.create mvc project ( dotnet new mvc -o YourprojectName )
4.add project to solution file ( dotnet sln [<SOLUTION_FILE>] add [--in-root] [-s|--solution-folder <PATH>] <PROJECT_PATH> [<PROJECT_PATH>...])


######//packages for downloading with cmd
dotnet tool uninstall --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SQLite

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

######//migrations with cmd 
dotnet tool install --global dotnet-ef
dotnet ef migrations add yourMigrationName
dotnet ef database update
dotnet ef migrations remove

######//migrations with Visual Studio
add-migration yourMigrationName
update-database

#####//package identity 
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 7.0.3
