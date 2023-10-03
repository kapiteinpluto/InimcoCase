
using inimco_case_backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;


var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(80); // Listen on port 80 for HTTP
//});

builder.Services.AddEntityFrameworkMySQL()
                .AddDbContext<InimcoContext>(options =>
                {
                    options.UseMySQL("server=localhost;port=3306;user=inimco;password=inimco;database=inimco");
                });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapGet("/user/{id}", async (int id, InimcoContext dbContext) =>
await dbContext.Users.FindAsync(id)
    is User user
        ? Results.Ok(new {
            id = user.Id,
            firstName = user.FirstName,
            lastName = user.LastName,
            skills = user.Skills,
            socialMedia = user.SocialMedia,
        })
        : Results.NotFound()
    );

app.MapPut("/user/{id}", async (int id, User user, InimcoContext dbContext) =>
{
    var users = await dbContext.Users.FindAsync(id);

    if (users is null) return Results.NotFound();

    users.Id = user.Id;
    users.FirstName = user.FirstName;
    users.LastName = user.LastName;
    users.SocialMediaString = user.SocialMediaString;
    users.SkillsString = user.SkillsString;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();