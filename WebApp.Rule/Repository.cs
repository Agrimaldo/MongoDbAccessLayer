using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApp.Rule.Model;
using WebApp.Rule.Util;

namespace WebApp.Rule
{
    public class Repository : IDisposable
    {
        #region Properties
        IMongoDatabase MongoDatabase { get; set; }
        #endregion

        #region Construtors
        public Repository()
        {
            string _defaultDatabasePath = "mongodb://localhost:27017";
            string _defaultDatabaseName = "AccessLayerForMongoDB";
            this.MongoDatabase = new MongoClient(_defaultDatabasePath).GetDatabase(_defaultDatabaseName);
        }
        #endregion

        #region Generic Methods
        public void Add<T>(T _object)
        {
            try
            {
                IMongoCollection<T> _collection = this.MongoDatabase.GetCollection<T>(typeof(T).Name);
                _collection.InsertOne(_object);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update<T>(IModelDefault _object)
        {
            try
            {
                IMongoCollection<T> _collection = this.MongoDatabase.GetCollection<T>(typeof(T).Name);
                Expression<Func<T, bool>> _conditional = a => ((IModelDefault)a).Id.Equals(_object.Id);
                _collection.ReplaceOne<T>(_conditional, (T)_object);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<T>(IModelDefault _object)
        {
            try
            {
                IMongoCollection<T> _collection = this.MongoDatabase.GetCollection<T>(typeof(T).Name);
                Expression<Func<T, bool>> _conditional = a => ((IModelDefault)a).Id.Equals(_object.Id);
                _collection.DeleteOne<T>(_conditional);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> List<T>(Expression<Func<T, bool>> _conditional = null)
        {
            List<T> _list = new List<T>();
            try
            {
                _conditional = _conditional.TreatNullExpression<T>();
                IMongoCollection<T> _collection = this.MongoDatabase.GetCollection<T>(typeof(T).Name);
                _list = _collection.Find<T>(_conditional).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _list;
        }
        #endregion

        #region Disposable
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            this.Dispose();
        }
        #endregion
    }
}
