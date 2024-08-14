using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using souschef_be.models;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDebug();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"] ??
                                                                throw new InvalidOperationException()))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddCors();

builder.Services.AddDbContext<DbContext, SouschefContext>();
builder.Services.AddScoped<ICrudSvc<Message>, PgCrudSvcComponent<Message>>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<ICrudSvc<Ingredient>, PgCrudSvcComponent<Ingredient>>();
builder.Services.AddScoped<ICrudSvc<Measurement>, PgCrudSvcComponent<Measurement>>();
builder.Services.AddScoped<ICrudSvc<Recipe>, PgCrudSvcComponent<Recipe>>();
builder.Services.AddScoped<ICrudSvc<Tag>, PgCrudSvcComponent<Tag>>();
builder.Services.AddScoped<ThinRecipeService>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}



app.Run();