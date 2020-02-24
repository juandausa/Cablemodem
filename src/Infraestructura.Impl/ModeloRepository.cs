using Entidades;
using JsonFlatFileDataStore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infraestructura.Impl
{
    public class ModeloRepository : IModeloRepository, IDisposable
    {
        protected IDataStore store;
        protected bool disposed = false;

        public ILogger<ModeloRepository> Logger { get; }

        public ModeloRepository(IAppSettings appSettings, ILogger<ModeloRepository> logger)
        {
            this.store = new DataStore(appSettings.JsonStorageFilePath);
            this.Logger = logger;
        }

        public bool Any(Expression<Func<Modelo, bool>> expression)
        {
            return store.GetCollection<Modelo>(GetCollectionName()).Find(ConvertExpressionToPredicate(expression)).Any();
        }

        public void Delete(Modelo entity)
        {
            this.Logger.LogDebug("Se intentará borrar la entidad {0}", entity.ToString());
            store.GetCollection<Modelo>(GetCollectionName()).DeleteOne(m => m.Nombre == entity.Nombre);
        }

        public Modelo Save(Modelo entity)
        {
            this.Logger.LogDebug("Se intentará guardar la entidad {0}", entity.ToString());
            if (store.GetCollection<Modelo>(GetCollectionName()).InsertOne(entity))
            {
                return entity;
            }

            this.Logger.LogError("La persistencia de datos del modelo {0} ha fallado", entity.ToString());
            throw new RepositoryException(string.Format("La persistencia de datos del modelo {0} ha fallado.", entity.ToString()));
        }

        public IEnumerable<Modelo> Search(Expression<Func<Modelo, bool>> expression)
        {
            return store.GetCollection<Modelo>(GetCollectionName()).Find(ConvertExpressionToPredicate(expression));
        }

        public Modelo Update(Modelo entity)
        {
            this.Logger.LogDebug("Se intentará actualizar la entidad {0}", entity.ToString());
            if (store.GetCollection<Modelo>(GetCollectionName()).UpdateOne(modelo => modelo.Nombre == entity.Nombre, entity))
            {
                return entity;
            }

            this.Logger.LogError("La actualización de datos del modelo {0} ha fallado.", entity.ToString());
            throw new RepositoryException(string.Format("La actualización de datos del modelo {0} ha fallado.", entity.ToString()));
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

        private static string GetCollectionName()
        {
            JsonObjectAttribute jsonAttribute = typeof(Modelo).GetCustomAttribute(typeof(JsonObjectAttribute)) as JsonObjectAttribute;
            if (jsonAttribute is null)
            {
                return "models";
            }

            return jsonAttribute.Id;
        }
    }
}
