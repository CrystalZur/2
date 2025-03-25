using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();


public class Accessory
{
    public string Name { get; set; }
    public string Creator { get; set; }
    public string Material { get; set; }
    public double Price { get; set; }

    

}

class ApiDbContext : ApiDbContext
{
    public virtual DbSet<Accessory> Accessories { get; set; }

    public ApiDbContext(Dboptions)
}