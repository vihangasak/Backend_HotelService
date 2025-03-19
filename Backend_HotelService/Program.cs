using Backend_HotelService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Simplified CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



builder.Services.AddDbContext<HotelServiceDbContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HotelServiceDbContext>();

    // Find users with empty salt
    var usersToUpdate = context.Users
        .Where(u => string.IsNullOrEmpty(u.Salt))
        .ToList();

    foreach (var user in usersToUpdate)
    {
        // Use a default password
        string defaultPassword = "1234"; // You might want a better default

        // Generate new salt and hash
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            user.Salt = Convert.ToBase64String(hmac.Key);
            user.PasswordHash = Convert.ToBase64String(
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(defaultPassword))
            );
        }
    }

    // Save changes
    if (usersToUpdate.Any())
    {
        context.SaveChanges();
        Console.WriteLine($"Updated password hashing for {usersToUpdate.Count} users");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Place CORS before HttpsRedirection and Authorization
app.UseCors("AllowReactApp");

// Comment out HttpsRedirection for local testing
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();