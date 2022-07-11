using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace azureCommsHubPlayground.persistance.interfaces
{
    public interface IPocoRepository<TEntity> where TEntity : class, IPocoEntity
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        int Count();

        int CountSelectively(Expression<Func<TEntity, bool>> filter = null);

        bool CheckIfExists(object id);

        IQueryable<TChild> GetSubType<TChild>(Expression<Func<TChild, bool>> filter = null);

        IQueryable<TChild> GetSubTypeAsNoTracking<TChild>(Expression<Func<TChild, bool>> filter = null);

        IQueryable<TEntity> GetChunksOf(int skip, int pageSize, Expression<Func<TEntity, int>> filter);

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        // async functions

        Task<IEnumerable<TEntity>> GetAsync(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");

        Task<TEntity> GetByIDAsync(object id);

        IQueryable<TEntity> GetQueryable(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       string includeProperties = "");


        IEnumerable<TEntity> GetAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

        IQueryable<TEntity> GetQueryableAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");


        DbSet<TEntity> getDbSet();

        IOrderedQueryable<TEntity> orderBy(Expression<Func<TEntity, int>> filter = null);

        IOrderedQueryable<TEntity> orderByDesceding(Expression<Func<TEntity, int>> filter = null);

        IOrderedQueryable<TEntity> orderBy(Expression<Func<TEntity, string>> filter = null);

        IOrderedQueryable<TEntity> orderByDesceding(Expression<Func<TEntity, string>> filter = null);
    }
}
