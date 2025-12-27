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
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthToken";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });
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

app.UseCors(x => x.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
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
else
{
    app.UseHttpsRedirection();
}

// Map only register endpoint from Identity API (returns bearer token)
app.MapPost("/register", async (SignUpRequest request, UserManager<IdentityUser> userManager) =>
    {
        var user = new IdentityUser { UserName = request.Email, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Description).ToArray());
        }

        return Results.Ok();
    })
    .Produces<string[]>(400)
    .Produces(200);

// Custom login endpoint that uses cookies
app.MapPost("/login", async (SignInRequest request, SignInManager<IdentityUser> signInManager) =>
    {
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Results.Unauthorized();
        }

        return Results.Ok();
    })
    .Produces(200)
    .Produces(401);

// Custom logout endpoint
app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok(new { message = "Logout successful" });
    });

app.MapGet("/user", async (HttpContext httpContext, UserManager<IdentityUser> userManager) =>
    {
        var identityUser = await userManager.GetUserAsync(httpContext.User);
        if (identityUser is null)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(new { identityUser.UserName });
    })
    .RequireAuthorization();

app.MapGet("/users", async (ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
    {
        var users = await dbContext.Users
            .Select(u => new UserResponse(u.Id, u.UserName!, u.Email!))
            .ToArrayAsync(cancellationToken);
        return users;
    })
    .RequireAuthorization()
    .Produces<UserResponse[]>(200);

app.MapGet("/users/{userId}/claims", async (string userId, ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            return Results.NotFound();
        }

        var userClaims = await dbContext.UserClaims
            .Where(c => c.UserId == userId)
            .Select(c => new UserClaimResponse(c.Id, c.ClaimValue!))
            .ToListAsync(cancellationToken);
        return Results.Ok(userClaims);
    })
    .RequireAuthorization()
    .Produces(404)
    .Produces<UserClaimResponse[]>(200);

app.MapGet("/claims", async (ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
    {
        var claims = await dbContext.Claims
            .ToListAsync(cancellationToken);

        return claims;
    })
    .RequireAuthorization();


app.Run();

// Request/Response DTOs
public record SignUpRequest(string Email, string Password);
public record SignInRequest(string Email, string Password);

public record UserResponse(string Id, string UserName, string Email);

public record UserClaimResponse(int Id, string Value);