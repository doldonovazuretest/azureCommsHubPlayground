using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using azureCommsHubPlayground.persistance.database;
using azureCommsHubPlayground.persistance.interfaces;

namespace azureCommsHubPlayground.persistance.repositories
{
    /// <summary>
    /// database entity repository implementing CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">database entity (table)</typeparam>
    public class entityRepository<TEntity> : IPocoRepository<TEntity> where TEntity : class, IPocoEntity
    {
        internal dbContext context;
        internal DbSet<TEntity> dbSet;

        public entityRepository(dbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int Count()
        {
            return dbSet.Count();

        }

        public int CountSelectively(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();

        }

        public bool CheckIfExists(object id)
        {
            TEntity o = dbSet.Find(id);
            if (o != null)
            {
                return true;
            }
            return false;
        }

        public IQueryable<TChild> GetSubType<TChild>(Expression<Func<TChild, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.OfType<TChild>().Where(filter);
            }
            return query.OfType<TChild>();
        }

        public IQueryable<TChild> GetSubTypeAsNoTracking<TChild>(Expression<Func<TChild, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.AsNoTracking().OfType<TChild>().Where(filter);
            }
            return query.AsNoTracking().OfType<TChild>();
        }

        public IQueryable<TEntity> GetChunksOf(int skip, int pageSize, Expression<Func<TEntity, int>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            return query.OrderBy(filter).Skip(skip).Take(pageSize);

        }

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.Any(filter);
            }
            return query.Any();
        }

        // async functions

        public async virtual Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetQueryable(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public virtual IEnumerable<TEntity> GetAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.AsNoTracking().Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public virtual IQueryable<TEntity> GetQueryableAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.AsNoTracking().Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking();
            }
            else
            {
                return query.AsNoTracking();
            }
        }


        public DbSet<TEntity> getDbSet()
        {
            return dbSet;
        }

        public IOrderedQueryable<TEntity> orderBy(Expression<Func<TEntity, int>> filter = null)
        {
            return dbSet.OrderBy(filter);
        }

        public IOrderedQueryable<TEntity> orderByDesceding(Expression<Func<TEntity, int>> filter = null)
        {
            return dbSet.OrderByDescending(filter);
        }

        public IOrderedQueryable<TEntity> orderBy(Expression<Func<TEntity, string>> filter = null)
        {
            return dbSet.OrderBy(filter);
        }

        public IOrderedQueryable<TEntity> orderByDesceding(Expression<Func<TEntity, string>> filter = null)
        {
            return dbSet.OrderByDescending(filter);
        }
    }
}
