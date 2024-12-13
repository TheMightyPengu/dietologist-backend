using dietologist_backend.Data;
using dietologist_backend.Helpers;
using dietologist_backend.Repository;
using dietologist_backend.Services;
using dietologist_backend.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

// Register FluentValidation with Auto-Validation and Client-Side Support
builder.Services
    .AddFluentValidationAutoValidation()   // Enables automatic validation
    .AddFluentValidationClientsideAdapters();  // Enables client-side validation

// Automatically register all validators in the assembly
builder.Services.AddValidatorsFromAssemblyContaining<ProvidedServicesBaseDtoValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<ImagesBaseDtoValidator>();

builder.Services.AddAutoMapper(typeof(Program));


// Read the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Connection string is null or empty.");
    throw new InvalidOperationException("Database connection string is not configured.");
}
else
{
    Console.WriteLine($"Connection string: {connectionString}");
}

// Register DbContext before other services
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(connectionString));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString: builder.Configuration.GetConnectionString(name: "DefaultConnection"),
            x => x.MigrationsHistoryTable(tableName: "migrations_history",
                schema: builder.Configuration.GetConnectionString(name: "Schema"))).UseSnakeCaseNamingConvention());

// Register application services and repositories AFTER DbContext
builder.Services.AddScoped<IProvidedServicesRepository, ProvidedServicesRepository>();
builder.Services.AddScoped<IProvidedServicesService, ProvidedServicesService>();
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<IContactMessagesService, ContactMessagesService>();
builder.Services.AddScoped<IContactMessagesRepository, ContactMessagesRepository>();
builder.Services.AddScoped<IEbooksService, EbooksService>();
builder.Services.AddScoped<IEbooksRepository, EbooksRepository>();
builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<INewsletterSubscribersService, NewsletterSubscribersService>();
builder.Services.AddScoped<INewsletterSubscribersRepository, NewsletterSubscribersRepository>();
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<IRecipesRepository, RecipesRepository>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<ISocialMediaLinksService, SocialMediaLinksService>();
builder.Services.AddScoped<ISocialMediaLinksRepository, SocialMediaLinksRepository>();



// Add CORS policy if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

// app.UseAuthorization();

app.MapControllers();

app.Run();