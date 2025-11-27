using IdentityExample.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityExample API", Version = "v1" });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
    {
      options.SignIn.RequireConfirmedAccount = false;
      options.SignIn.RequireConfirmedEmail = false;
      options.SignIn.RequireConfirmedPhoneNumber = false;
      options.Lockout.AllowedForNewUsers = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
if (origins is null) throw new InvalidOperationException("No CORS origins configured.");
    
app.UseCors(x => x.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader());
if (app.Environment.IsDevelopment())
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var devScope = scopeFactory.CreateScope())
    {
        var applicationDbContext = devScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await applicationDbContext.Database.EnsureCreatedAsync();
    }

    // Serve Swagger JSON and UI in Development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityExample API v1");
        // Keep the default RoutePrefix ("swagger") so UI is available at /swagger
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapIdentityApi<IdentityUser>();

app.Run();