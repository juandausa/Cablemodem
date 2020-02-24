using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infraestructura.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> entity;
        protected CablemodemContext context;
        protected ILogger Logger;

        public BaseRepository(CablemodemContext context, Microsoft.Extensions.Logging.ILogger logger)
        {
            this.context = context;
            this.Logger = logger;
            this.entity = context.Set<T>();
        }

        public virtual IEnumerable<T> Search(Expression<Func<T, bool>> expression)
        {
            return entity.Where(expression).ToList();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return entity.Any(expression);
        }

        public T Save(T entity)
        {
            this.Logger.LogDebug("Se intentará guardar la entidad {0}", entity.ToString());
            this.entity.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            this.Logger.LogDebug("Se intentará actualizar la entidad {0}", entity.ToString());
            this.entity.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            this.Logger.LogDebug("Se intentará remover la entidad {0}", entity.ToString());
            this.entity.Remove(entity);
            context.SaveChanges();
        }
    }
}
