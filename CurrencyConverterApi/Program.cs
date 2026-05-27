using CurrencyConverterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// look for Controllers
builder.Services.AddControllers();

// Register HttpClient for CurrencyService
builder.Services.AddHttpClient<CurrencyService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

// CORS = Allow Angular (running on port 4200) to talk to C# API (port 5072)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAngular");        // Actually use the CORS policy
app.MapControllers();               // Look for API endpoints in controllers



app.Run();