using NewApp;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("EMS");

builder.Services.AddSqlite<EMSContext>(connString);

var app = builder.Build();

app.MapEmpEndpoints();

await app.MigrateDbAsync();

app.Run();