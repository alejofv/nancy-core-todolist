A sample project using NancyFxand dotnet Core 2.0 to expose a ToDo list API 

---

## Dependencies

[NancyFx](https://github.com/NancyFx/Nancy) as the API framework
  
[CsvHelper](https://github.com/JoshClose/CsvHelper) as a simple storage solution

---

## Interesting points (as personal notes)

To create the project's basic structure:

* Start with an Empty asp.net core project (`dotnet new web`)
* Include pre-release version of Nancy (`dotnet add package Nancy --version 2.0.0-clinteastwood`)
  
  
Nancy is integrated with OWIN (among others), so this is the configuration code (in `Startup.cs`):

```cs
app.UseOwin(x => x.UseNancy());
``` 
  
  
The Nancy Modules are automatically discovered, so no service registration code (in `Statup.cs`) is needed.

Likewise, Nancy uses a simple IoC container by default, so Module dependencies are also not registered (in the _default_ service registration method). To maintain the "_super-duper-happy-path_", the services classes must have a default constructor (or one with resolvable dependencies).  

To make a response with an specific http status code, the `Nancy.HttpStatusCodes` enum (update: even the corresponding `int` value!) can be implicitly converted to a Response.

Likewise, a string or a dynamic object can be implicitly converted to a Response (for responses with body/payload). When doing so, the Status code is 200 (OK) by default, unless explicitly assigned:

```cs
var response = (Nancy.Response)item.Id;
response.StatusCode = Nancy.HttpStatusCode.Created;
```
  

To read raw body data from the request as a string:

```cs 
RequestStream.FromStream(Request.Body).AsString()
```
