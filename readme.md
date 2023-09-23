# Setup steps

In this documentation, we will learn how to add Swagger Documentation to a new or existing application from scratch.

### ✅ Basic Setup

🚩 First of all, Install the nuget package `Swashbuckle.AspNetCore`.

🚩 Then, open your `program.cs` file and add the following service after the `builder` variable -

```csharp
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
});
```

🚩 Next add the following middleware after the `app` variable -

```csharp
app.UseSwagger();
```

🚩 To add the swagger UI add the following middleware after `app.UseSwagger()` middleware. This must be
in sequence -

```csharp
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint("swagger/ProductsApi/swagger.json", "Products Api");
});
```

🚩 By default, Swagger UI will be generated at the following URL `http://localhost:port/swagger/index.html`.
However, we can improve this by customizing the default URL. To do so, add the following configuration to the
UseSwaggerUI middleware.

```csharp
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint("swagger/ProductsApi/swagger.json", "Products Api");

    //To customize url
    setupAction.RoutePrefix = string.Empty;
});
```

Now Swagger UI will be generated into `http://localhost:port/index.html` URL.

### ✅ XML Based Documentation

If we want to generate xml based documentation we can do it easily by following these steps -

🚩 After implementing the above setups, right-click on your project and go to 'Properties.' Under 'Build Options,'
click on the 'Output' tab. Now, locate and check the 'Documentation' checkbox. Add your filename in the input box below
the 'Documentation' checkbox. Optionally, you can specify a folder name if you want to organize your documentation file
in a separate folder. For example, 'Docs/ProjectName.Api.xml.

🚩 Next add this following option into the `AddSwaggerGen` service -

```csharp
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("ProductsApi", new OpenApiInfo()
    {
        Title = "Products Api",
        Version = "1"
    });

    //For XML Setup
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);
    setupAction.IncludeXmlComments(filePath);
});
```

🚩 Alternatively, you can add these two options in the `.csproj` file under the `<PropertyGroup>` -

```csharp
    <PropertyGroup>
        <TargetFramework>net7.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>

        //For XML base swagger documentation
        <GenerateDocumentationFile>True</GenerateDocumentationFile>
        <DocumentationFile>Docs/SwaggerPractice.xml</DocumentationFile>
    </PropertyGroup>
```

🚩 Example of xml base documentation -

```csharp
    /// <summary>
    /// Get All Products
    /// </summary>
    /// <returns>Get All Products</returns>
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return Products;
    }

```

🚩 "Last but not least, you can add the following option to your .csproj file to enable the API Analyzer and receive
warnings in case you forget to add any specifications.-

```csharp
    <IncludeOpenApiAnalyzers>true</IncludeOpenApiAnalyzers>
```

## ✅ Documentation Options

🚩 Add the `[ProducesResponseType()]` attribute over your controller `Action` to generate `ResponseType`

```csharp
    /// <summary>
    /// Get a specific product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns an ActionResult of type Product</returns>
    /// <response code="200">Returns the requested Product</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<OkObjectResult> Get(Guid id)
    {
        return Task.FromResult(Ok(Products.FirstOrDefault(x => x.Id.Equals(id))));
    }
```
