using Newtonsoft.Json;

IConfiguration configuration;
//convert from json to classes
string json = File.ReadAllText("Config/config.json");
Config.Configuration = JsonConvert.DeserializeObject<Config>(json);

var builder = WebApplication.CreateBuilder(args);

// listen in all directions, not only localhost
builder.WebHost.UseUrls("http://0.0.0.0:5125");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin() //  Cannot be used together with AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
        //.AllowCredentials(); // Cannot be used together with AllowAnyOrigin()
    });
});

var app = builder.Build();

//enable CORS
app.UseCors("CorsPolicy");

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
