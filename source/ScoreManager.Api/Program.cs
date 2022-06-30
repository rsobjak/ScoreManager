using Microsoft.OpenApi.Models;
using ScoreManager;
using ScoreManager.Data;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);
    SerilogExtension.AddSerilogApi(builder.Configuration);
    builder.Host.UseSerilog(Log.Logger);

    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>();

    var type = typeof(CrudBase<>);
    var types2 = AppDomain.CurrentDomain.GetAssemblies()
       .SelectMany(s => s.GetTypes())
       .Where(p => p.FullName.Contains("ScoreManager") && p.Name.EndsWith("DAL"));
    foreach (var t in types2)
    {
        var @interface = t.GetInterfaces().Single(w=> !w.Name.Contains("Base"));
        builder.Services.AddTransient(@interface, t);
    }

    builder.Services.AddControllers()
                    .AddJsonOptions(x =>
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ScoreManager API",
            Description = "An ASP.NET Core Web API for managing ScoreManager solution data",
            Contact = new OpenApiContact
            {
                Name = "Developer",
                Email = "rodrigo.sobjak@gmail.com"
            },
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestSerilLogMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayOperationId();
            options.EnableTryItOutByDefault();
            options.EnablePersistAuthorization();
            options.EnableDeepLinking();
            options.EnableValidator();
            options.DisplayRequestDuration();
            options.ShowExtensions();
            options.ShowCommonExtensions();
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}