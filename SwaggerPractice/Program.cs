using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("ProductsApi", new OpenApiInfo()
    {
        Title = "Products Api",
        Version = "1",
        Description = "This is a sample api documentation",
        Contact = new OpenApiContact()
        {
            Name = "AN Rajin",
            Email = "an.rajin@gmail.com",
            Url = new Uri("https://anrajin.github.com/me")
        },
        License = new OpenApiLicense()
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/licenses/MIT")
        },
        TermsOfService = new Uri("https://anrajin.github.com/me"),
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);

    setupAction.IncludeXmlComments(filePath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint("swagger/ProductsApi/swagger.json", "Products Api");
    setupAction.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();