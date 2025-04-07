using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Auth;
using Functions.Server.Interfaces.Event;
using Functions.Server.Interfaces.Users;
using Functions.Server.Interfaces.Messages;
using Functions.Server.Model;
using Functions.Server.Repsitorys;
using Functions.Server.Services;
using Functions.Server.Services.Auth;
using Functions.Server.UseCases;
using Functions.Server.UseCases.Auth;
using Functions.Server.UseCases.Event;
using Functions.Server.UseCases.Messages;
using Functions.Server.UseCases.Users;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Functions.Server.Interfaces.Participation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FunctionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI Containers
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRepository<Events>, EventsRepository>();
builder.Services.AddScoped<IRepository<Files>, FilesRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Message>, MessageRepository>();
builder.Services.AddScoped<IRepository<EventVisitor>, EventVisitorsRepository>();
builder.Services.AddScoped<IRegistrationUseCase, RegistrationUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<FunctionsControllerBase>();
builder.Services.AddScoped<IGetEventsUseCase, GetEventsUseCase>();
builder.Services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
builder.Services.AddScoped<ICreateEventUseCase, CreateEventUseCase>();
builder.Services.AddScoped<IGetMessagesByEventIdQuery, GetMessagesByEventIdQuery>();
builder.Services.AddScoped<IPostAnnouncementUseCase, PostAnnouncementUseCase>();
builder.Services.AddScoped<IEventVisitorQuery,  EventVisitorQuery>();

builder.Services.AddScoped<IGetUserProfilePicture, GetUserProfilePicture>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

string[]? corsAllowedAddresses = builder.Configuration.GetSection("CORS:Allowed").Get<string[]>() ?? ["*"];
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(corsAllowedAddresses)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
