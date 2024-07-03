using Microsoft.EntityFrameworkCore;
namespace NewApp;

public static class EmpEndpoints
{
    const string GetEmp = "Getemp";
    public static RouteGroupBuilder MapEmpEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("EMS").WithParameterValidation();

        // GET All Employees
        group.MapGet("/", async (EMSContext dbContext) =>
            await dbContext.Employees
                     .Select(employee => employee.ToEmpDto())
                     .AsNoTracking()
                     .ToListAsync());

        // GET Employees by id
        group.MapGet("/{id}", async (int id, EMSContext dbContext) =>
        {
            Employee? employee = await dbContext.Employees.FindAsync(id);
            return employee is null ? 
                Results.NotFound() : Results.Ok(employee.ToEmpDto());
        }).WithName(GetEmp);

        // POST Employees
        group.MapPost("/", async (CreateDto newEmp, EMSContext dbContext) =>
        {
            Employee emp = newEmp.ToEntity();
            dbContext.Employees.Add(emp);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetEmp, new { id = emp.Id }, emp);
        });

        //PUT Employee
        group.MapPut("/{id}", async (int id, UpdateDto update,EMSContext dbContext) =>
        {
            var existingEmp = await dbContext.Employees.FindAsync(id);
            if (existingEmp is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingEmp).CurrentValues.SetValues(update.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        //DELETE Employee
        group.MapDelete("/{id}", async (int id, EMSContext dbContext) =>
        {
            await dbContext.Employees.Where(emp => emp.Id ==id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
