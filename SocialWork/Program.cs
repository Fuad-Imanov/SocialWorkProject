using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialWork.DAL;
using SocialWork.DAL.Interfaces;
using SocialWork.DAL.Repository;
using SocialWork.Domain.Entity;
using SocialWork.Service.Impelementations;
using SocialWork.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//men elave etmishem ashagidakilari
var services = builder.Services;
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connection));

services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); 

services.AddControllersWithViews();
services.AddScoped<IBaseRepository<Training>, TrainingRepository>();
services.AddScoped<ITrainingService,TrainingService>();

//men elave etmishem yuxaridakilari

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    try
    {
        var userManager = service.GetRequiredService<UserManager<User>>();
        var rolesManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        var logger = service.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    // autentifikasiyanin qosulmasi
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();