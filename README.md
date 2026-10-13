TaskManager — a task management API in development. Currently provides two basic GET endpoints.


.NET SDK - 10.0.103


Build:
```powershell
dotnet build TaskManager.slnx
```
Run:
```powershell
dotnet run --project backend/TaskManager.Api --launch-profile http
```


Run commands from the repository root. Use the application URL shown in the "Now listening on" line.
Available routes and expected responses:

1. / -> "TaskManager API is running"	[GET: 200 OK]
2. /api/info -> {"name":"TaskManager","version":"0.1.0"}	[GET: 200 OK]
