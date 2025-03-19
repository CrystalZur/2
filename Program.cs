using System.Runtime.ExceptionServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

var alphabet = new List<Letters>()
{
    new() {First = "a", Second = "b", Third = "c", Fourth = "d", Fifth = "e", HowManyLeft = 999}
};

app.MapGet("alphabet", () => alphabet);

app.MapPost("alphabet", (Letters l) => alphabet.Add(l));

app.MapPut("alphabet", (AlphabetUpdateDTO dto) =>
{
    var lett = alphabet.FirstOrDefault(o => o.First == dto.First);
    if (lett != null)
    {
        lett.First = dto.First;
        lett.Second = dto.Second;
        lett.Third = dto.Third;
        lett.Fourth = dto.Fourth;
        lett.Fifth = dto.Fifth;
        lett.HowManyLeft = dto.HowManyLeft;
        return Results.Json(lett);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("alphabet", (string firstLetter) =>
{
    Letters lett = alphabet.FirstOrDefault(o => o.First == firstLetter);
    if (lett != null)
    {
        alphabet.Remove(lett);
        return Results.Json(lett);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

record class AlphabetUpdateDTO(string First, string Second, string Third, string Fourth, string Fifth, int HowManyLeft);

public class Letters
{
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public string Fourth { get; set; }
    public string Fifth { get; set; }
    public int HowManyLeft { get; set; }
}