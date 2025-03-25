var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Cup> cups = new List<Cup>()
{
    new() {Name = "asd", Material = "asd", Creator = "aasd", Size = 1000, Price = 50}
};

app.MapGet("cups", () => cups);
app.MapPost("cups", (Cup cup) => cups.Add(cup));
app.MapPut("cups", (CupUpdateDTO dto) =>
{
    var cup = cups.Find(o => o.Name == dto.Name);
    if (cup.Material != dto.Material && dto.Material != "")
    {
        cup.Material = dto.Material;
    }
    if (cup.Size != dto.Size && dto.Size != 0) 
    { 
        cup.Size = dto.Size;
    }
    if (cup.Price != dto.Price && dto.Price != 0) 
    { 
        cup.Price = dto.Price; 
    }
    return Results.Json(cup);
});
app.MapDelete("cups", (string cupName) =>
{
    var cup = cups.Find(o => o.Name == cupName);
    if (cup != null)
    {
        cups.Remove(cup);
        return Results.Json(cup);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

record class CupUpdateDTO(string Name, string Material, int Size, double Price);

public class Cup
{
    public string Name { get; set; }
    public string Material { get; set; }
    public string Creator { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
}