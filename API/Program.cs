using API.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/cotacao/{id}",(int id, IMessageProducer _messagePublisher) =>
    {
        _messagePublisher.SendMessage(new List<string>{"AEIOU"});
        return Results.Accepted();
    })
    .WithName("GetCotacao")
    .WithOpenApi();

app.Run();