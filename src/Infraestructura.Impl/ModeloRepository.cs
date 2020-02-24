using Entidades;
using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infraestructura.Impl
{
    public class ModeloRepository : IModeloRepository, IDisposable
    {
        protected IDataStore store;
        protected bool disposed = false;

        public ModeloRepository(string filePath)
        {
            this.store = new DataStore(filePath);
        }

        public bool Any(Expression<Func<Modelo, bool>> expression)
        {
            return store.GetCollection<Modelo>().Find(ConvertExpressionToPredicate(expression)).Any();
        }

        public void Delete(Modelo entity)
        {
            store.GetCollection<Modelo>().DeleteOne(m => m.Name == entity.Name);
        }

        public Modelo Save(Modelo entity)
        {

            if (store.GetCollection<Modelo>().InsertOne(entity))
            {
                return entity;
            }

            throw new RepositoryException(string.Format("La persistencia de datos del modelo {0} ha fallado.", entity.Name));
        }

        public IEnumerable<Modelo> Search(Expression<Func<Modelo, bool>> expression)
        {
            return store.GetCollection<Modelo>().Find(ConvertExpressionToPredicate(expression));
        }

        public Modelo Update(Modelo entity)
        {
            if (store.GetCollection<Modelo>().UpdateOne(modelo => modelo.Name == entity.Name, entity))
            {
                return entity;
            }

            throw new RepositoryException(string.Format("La actualización de datos del modelo {0} ha fallado.", entity.Name));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                store.Dispose();
                store = null;
            }

            disposed = true;
        }

        private static Predicate<Modelo> ConvertExpressionToPredicate(Expression<Func<Modelo, bool>> expression)
        {
            Func<Modelo, bool> func = expression.Compile();
            return t => func(t);
        }
    }
}
