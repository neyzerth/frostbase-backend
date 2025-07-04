using Newtonsoft.Json;

IConfiguration configuration;
//convert from json to classes
string json = File.ReadAllText("Config/config.json");
Config.Configuration = JsonConvert.DeserializeObject<Config>(json);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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
app.UseRouting(); 
app.MapControllers(); 

app.Run();