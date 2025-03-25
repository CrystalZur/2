var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Liquid> liquids = new List<Liquid>()
{
    new() {Name = "fghfgh", Type = "asd", Price = 50}
};

app.MapGet("liquids", () => liquids);
app.MapPost("liquids", (Liquid liquid) => liquids.Add(liquid));
app.MapPut("liquids", (LiquidUpdateDTO dto) =>
{
    var liquid = liquids.Find(o => o.Name == dto.Name);
    if (liquid.Type != dto.Type && dto.Type != "")
    {
        liquid.Type = dto.Type;
    }
    if (liquid.Price != dto.Price && dto.Price != 0)
    {
        liquid.Price = dto.Price;
    }
    return Results.Json(liquid);
});
app.MapDelete("liquids", (string liquidName) =>
{
    var liquid = liquids.Find(o => o.Name == liquidName);
    if (liquid != null)
    {
        liquids.Remove(liquid);
        return Results.Json(liquid);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

record class LiquidUpdateDTO(string Name, string Type, double Price);

public class Liquid
{
    public string Name { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }
}