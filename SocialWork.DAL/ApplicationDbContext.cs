using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialWork.Domain.Entity;
using System.Data;


namespace SocialWork.DAL;

public class ApplicationDbContext:IdentityDbContext<User>
{
    public DbSet<Training> Trainings { get; set; }
   // public DbSet<User> Users { get; set; }

     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();  
    }

    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=DESKTOP-QVDQ57F\\SQLEXPRESS01;Database=Training;Trusted_Connection=True;Encrypt=False");
    // }

    // "DefaultConnection":"Server=DESKTOP-QVDQ57F\\SQLEXPRESS01;Database=CarShop;Trusted_Connection=True;Encrypt=False"
}