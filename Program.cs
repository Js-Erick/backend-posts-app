using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChallengePostsAPI.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddDbContext<PostContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StringConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

