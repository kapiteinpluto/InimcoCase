var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/user/{id}", async (int id, EmployeeTrackingContext dbContext) =>
await dbContext.Employees.FindAsync(id)
    is Employee employee
        ? Results.Ok(employee)
        : Results.NotFound()
    );

app.MapPut("/user/{id}", async (int id, Employee employee, EmployeeTrackingContext dbContext) =>
{
    var employees = await dbContext.Employees.FindAsync(id);

    if (employees is null) return Results.NotFound();

    employees.Id = employee.Id;
    employees.Name = employee.Name;
    employees.Position = employee.Position;
    employees.Department = employee.Department;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();