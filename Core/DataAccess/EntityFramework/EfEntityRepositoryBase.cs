using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new() 
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext vt = new TContext())
            {
                var addEntity = vt.Entry(entity);
                addEntity.State = EntityState.Added;
                vt.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext vt = new TContext())
            {
                var deletedEntity = vt.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                vt.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filtre)
        {
            using (TContext vt = new TContext())
            {
                return vt.Set<TEntity>().SingleOrDefault(filtre);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filtre = null)
        {
            using (TContext vt = new TContext())
            {
                return filtre == null ? vt.Set<TEntity>().ToList() : vt.Set<TEntity>().Where(filtre).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext vt = new TContext())
            {
                var updatedEntity = vt.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                vt.SaveChanges();
            }
        }
    }
}
