using Microsoft.EntityFrameworkCore;
using WalletApi.Domain;
using WalletApi.Infrastructure;
using WalletApi.Infrastructure.Repositories;
using WalletApi.ExentsionsMethods;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region MypartofTheCode
//TradeRequest
builder.Services.AddScoped<ITradeRequestRepository, TradeRequestRepository>();
builder.Services.AddDbContext<TradeRequestContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("walletDataBase"),sqlOption => { });
});
//users
builder.Services.AddDbContext<UsersContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("walletDataBase"),sqlOption =>{});
});
builder.Services.AddDefaultIdentity<IdentityUser>(option =>
{
    //option.SignIn.RequireConfirmedEmail = true; 
}).AddEntityFrameworkStores<UsersContext>();
builder.Services.AddCustomSecurity(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddInjections();
builder.Services.AddScoped<ITradeRequestRepository, TradeRequestRepository>();

//builder.Services.AddControllers(); builder.Services.AddScoped<IUsersRepository, UsersRepositories>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
