using System.Dynamic;
using SocialWork.Domain.Entity;

namespace SocialWork.DAL.Interfaces;

public interface IBaseRepository<T>
{
     //Task<Training?> Get(int id);
     //Task<List<Training>> GetAll();
     Task<bool> Create(T entity);
     
     Task<bool> Delete(T entity);
     
     Task<Training> Update(T entity);

     IQueryable<T> GetAll();

}