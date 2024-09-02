This Web API sample application provides you the basic elements of an application that uses .NET DataStore for data access.

Prerequisite:
Because the .NET DataStore model (D_Department.cs) in the application is already bound with
specific DataContext (database connection), you need to set up the database following these steps:
1. Download the database backup file from https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2012.bak.
2. Install SQL Server if it is not installed.
3. Restore database using the downloaded database backup file.
4. In appsettings.json, modify the database connection name from the default "Local" to a name you prefer to use, 
   and modify the datasource, initial catalog, username and password of the database connection string.
5. In the Program.cs, go to the following line, and replace the ConnectionString name "Local"
   with the database connection name specified in step #4.
   services.AddDataContext<SampleDataContext>(m => m.UseSqlServer(Configuration["ConnectionStrings:Local"]));

How to run the sample app:
Enter the URL http://localhost:5950/api/sample/{id}.

Basic elements of the application:
1. The Program.cs file.
   This file is the entry point for the application. It sets up the IWebHost, and configures the application infrastructure.
   For more description, you may refer to
   https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-6.0&tabs=windows.

2. The SampleDataContext.cs file.
   The SampleDataContext.cs file contains the class that manages database connections and transactions.

3. The appsettings.json file.
   The database connection string and other app settings are saved in appsettings.json.

4. The DataWindows folder.
   The D_Department.cs file is the .NET DataStore model that is generated from a PowerBuilder DataWindow using the DataWindow Converter tool.

5. The ISampleService.cs and SampleService.cs files.
   The interface file ISampleService.cs contains only the declaration of the methods for the services.
   The service file SampleService.cs contains the implementation of the services that the applicationprovides.

6. The SampleController.cs file.
   It contains the sample API controller class, which provides methods that respond to HTTP requests.

7. The launchSettings.json file.
   It is a standard file in ASP.NET Core. This file sets up the environments that SnapDevelop can launch automatically.
