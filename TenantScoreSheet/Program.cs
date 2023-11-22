
// Initializes a new instance of a web application builder.
var builder = WebApplication.CreateBuilder(args);

// Configuring the web application builder to add controllers to the application's services collection.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Configuring the web application builder to add the API Explorer functionality to the application's services collection.
builder.Services.AddEndpointsApiExplorer();

// Configuring the web application builder to add Swagger support to the application's services collection.
builder.Services.AddSwaggerGen();

// Configuring the web application builder to add a database context to the application's services collection


// Configuring the web application builder to add Cross-Origin Resource Sharing (CORS) support to the application's services collection.
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();

    });
});

// Configuring the application's request processing pipeline with Swagger and SwaggerUI middleware.
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/Swagger/v1/Swagger.json", "RCMapi v1");
    c.RoutePrefix = String.Empty;
}
);

//  Enables CORS for the application and specifies the CORS policy to be used.
app.UseCors("default");

// Enables HTTP Strict Transport Security (HSTS) for the application.
app.UseHsts();

// Enables HTTPS redirection for the application.
app.UseHttpsRedirection();

// Adds Authorization middleware to the application's request processing pipeline.
app.UseAuthorization();

//  Adds Routing middleware to the application's request processing pipeline.
app.UseRouting();

// Adds MapControllers middleware to the application's request processing pipeline. 
app.MapControllers();

// Adds the final middleware to the application's request processing pipeline.
app.Run();