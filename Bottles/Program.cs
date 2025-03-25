var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Bottle> bottles = new List<Bottle>()
{
    new() {Name = "a", Manufacterer = "boba", Capacity = 450, Price = 45.99}
};

app.MapGet("bottles", () =>  bottles);

app.MapPost("bottles", (Bottle b) => bottles.Add(b));

app.MapPut("bottles", (BottleUpdateDTO dto) =>
{
    var bottle = bottles.Find(o => o.Name == dto.Name);
    if (bottle != null)
    {
        bottle.Capacity = dto.Capacity;
        bottle.Price = dto.Price;
        return Results.Json(bottle);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("bottles", (string bottleName) =>
{
    var bottle = bottles.Find(o => o.Name == bottleName);
    if (bottle != null) 
    { 
        bottles.Remove(bottle);
        return Results.Json(bottle);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

record class BottleUpdateDTO(string Name, int Capacity, double Price);

public class Bottle
{
    public string Name { get; set; }
    public string Manufacterer { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
}