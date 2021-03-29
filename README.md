# Parcel app

This repository is home to my test assignement to Helmes.

## Requierments

This project is made in two parts. Back-End in .NET5.0 and front-end in React framework using Node.js LTS. Make sure to have these installed, also database connection is required.

# Getting back-end service to work

In commandNavigate to solution folder at

```bash
 ../ParcelSolution
```

First it is necessary to set up database server connection. You can to that in /WebApp/appsetting.json.
Application default is set to use Microsoft SQL Server(MSSQLExpressConnection in appsettings). If you also want to use MS SQL server( also Express server),
 just replace the connection string with yours.

```json
   "ConnectionStrings": {
      "DefaultConnection": "DataSource=app.db;Cache=Shared",
      "PostgresConnection": "User ID=postgres;Password=yourpass;Host=localhost;Port=5432;Database=parcel;Pooling=true;",
      "MSSQLExpressConnection": "Server=localhost\\SQLEXPRESS01;Database=parcel;Trusted_Connection=True;"
  },
```
If you wish to use other databases you also have to configure connection type in Startup.cs by changing options.something.
Different databases may need different NuGet packages installed.

```c#
//=============== Configure DB connection ===============
public void ConfigureServices(IServiceCollection services)
        {
            //=============== Configure DB connection ===============
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MSSQLExpressConnection")));
//=======================================================
```
## Install or update tooling
Run in ../ParcelSolution
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## Database managing
First command is for creating migrations for the database. Latest migrations are already applied for MSSQL DB. 
In case you need to delete them for other DBs, they are located in DAL.App.EF project. Just delete the migrations folder.

Second command creates/updates your database with coorect tables and restricts etc.
Third command is to delete existing DB from your server.
Run in ../ParcelSolution

```bash
dotnet ef migrations --project DAL.App.EF --startup-project WebApp add InitialDbCreation 
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
```

## Starting back-end 
Run in ../ParcelSolution

```bash
dotnet run --project WebApp
```

Default localhost server is https://localhost:5001/
API documentation powered by Swagger can be found at https://localhost:5001/swagger/

# Getting front-end application to work

Navigate to project folder at ../parcel-client-app

## Installing neccessary packages

```bash
npm install
```

## Running the app
Start the app with

```bash
npm start
```

Default localhost is at http://localhost:3000/

#About the project