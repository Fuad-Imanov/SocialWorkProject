using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialWork.DAL.Interfaces;
using SocialWork.Domain.Entity;
using Microsoft.Extensions.Hosting;

namespace SocialWork.DAL.Repository;

public class TrainingRepository:IBaseRepository<Training>
{
    private readonly ApplicationDbContext _db;

    public TrainingRepository(ApplicationDbContext db)
    {
        _db = db;
       
    }


    public async Task<bool> Create(Training entity)
    {
       await _db.Trainings.AddAsync(entity);
       await _db.SaveChangesAsync();
       return true;
    }

    public async Task<bool> Delete(Training entity)
    {
         _db.Trainings.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Training> Update(Training entity)
    {
        _db.Trainings.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public IQueryable<Training> GetAll()
    {
        return _db.Trainings.AsQueryable();
    }
}