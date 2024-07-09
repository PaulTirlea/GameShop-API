using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SummerPracticePaul.App_Start;
using SummerPracticePaul.Mapping;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;
using SummerPracticePaul.Services;
using SummerPracticePaul.Services.Interface;
using System.Reflection;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);


// Configure services and add them to the container.
builder.Services.AddDbContext<InMemoryDbContext>(options =>
{
    options.UseInMemoryDatabase("Playopolis");
});
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseInMemoryDatabase("UserDatabase");
});
//builder.Services.AddScoped<GameService>();
//builder.Services.AddScoped<GameCategoryService>();
builder.Services.AddScoped<IGameRepository, InMemoryGameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameCategoryRepository, InMemoryGameCategoryRepository>();
builder.Services.AddScoped<IGameCategoryService, GameCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, InMemoryRoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IControllPanelService, ControllPanelService>();
builder.Services.AddScoped<ControllPanelService>();
builder.Services.AddScoped<IDiscountRepository, InMemoryDiscountRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IReviewRepository, InMemoryReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();




builder.Services.AddAutoMapper(typeof(MappingProfile));


// Configuration for JWT authentication
var key = GenerateRandomKey();

static byte[] GenerateRandomKey()
{
    var key = new byte[32];
    using (var rng = RandomNumberGenerator.Create())
    {
        rng.GetBytes(key);
    }
    return key;
}
builder.Services.AddSingleton(new SymmetricSecurityKey(key));
builder.Services.AddScoped<AuthService>(sp => new AuthService(
    sp.GetRequiredService<UserManager<User>>(),
    sp.GetRequiredService<SignInManager<User>>(),
    sp.GetRequiredService<RoleManager<Role>>(),
    sp.GetRequiredService<SymmetricSecurityKey>(),
    sp.GetRequiredService<IMapper>()
));

builder.Services.AddControllers();

builder.Services.AddIdentity<User, Role>(options =>
{
    // Configure identity options if needed
})
.AddEntityFrameworkStores<UserDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Add SwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Playopolis GameStore API",
        Description = "API for managing games in an online store.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Paul Tirlea",
            Email = "tirlea.paul1@gmail.com",
            Url = new Uri("https://paultirlea.github.io/obsidosoft/#")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v2", new OpenApiInfo { Title = "MyAPI", Version = "v2" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.CreateDbIfNotExists();

    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Playopolis GameStore API V1");
        c.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
    });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();  // Add authorization middleware

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    RouteConfig.ConfigureRoutes(endpoints);
});


app.Run();
