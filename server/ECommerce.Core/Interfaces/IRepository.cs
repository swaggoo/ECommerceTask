using System.Linq.Expressions;
using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces;

public interface IRepository
{
    IQueryable<T> GetAll<T>() where T : BaseEntity;
    void Insert<T>(T objModel) where T : BaseEntity;
    Task<T> GetAsync<T>(Expression<Func<T, bool>>? expression) where T : BaseEntity;
    IQueryable<T> Fetch<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    void Update<T>(T entity) where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : BaseEntity;
}