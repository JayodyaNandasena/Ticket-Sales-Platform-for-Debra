using Debra_API.Data;
using Debra_API.Profiles;
using Debra_API.Repositories.AdminAccountRepositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

void ConfigureServices(IServiceCollection services) {
    services.AddDbContext<AppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //services.AddAutoMapper(Assembly.GetEntryAssembly());

    services.AddControllersWithViews();

    services.AddAutoMapper(typeof(AdminAccountProfile).Assembly);
    //services.AddControllers();

    // Register Repositories with their implementations
    services.AddScoped<IAdminAccountRepository, AdminAccountRepository>();


}

/*builder.Services.AddDbContext<AppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(AdminAccountProfile).Assembly);
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
