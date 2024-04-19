using System.Linq.Expressions;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Repositories;

public class Repository(ECommerceDbContext context) : IRepository
{
    public IQueryable<T> GetAll<T>() where T : BaseEntity
    {
        var query = context.Set<T>().AsQueryable();

        return query;
    }
    
    public void Insert<T>(T objModel) where T : BaseEntity
    {
        context.Set<T>().Add(objModel);
    }

    public async Task<T> GetAsync<T>(Expression<Func<T, bool>>? expression = null) where T : BaseEntity
    {
        var dbSet = context.Set<T>().AsQueryable();

        var result = expression == null
            ? await dbSet.FirstOrDefaultAsync()
            : await dbSet.FirstOrDefaultAsync(expression);

        return result;
    }

    public IQueryable<T> Fetch<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
    {
        return context.Set<T>().Where(expression);
    }

    public void Update<T>(T entity) where T : BaseEntity
    {
        var entityEntry = context.Entry(entity);

        if (entityEntry.State == EntityState.Detached)
        {
            context.Set<T>().Attach(entity);
        }

        entityEntry.State = EntityState.Modified;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
    
    public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : BaseEntity
    {
        var query = context.Set<T>().FromSqlRaw(sql);

        return query;
    }
}