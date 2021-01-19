using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace mongo_lib
{
    [Obsolete("Use MongoCollectionContext instead")]
    public class MongoContext : IMongoContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public MongoContext() { }

        public MongoContext(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }

        public void Insert<TEntity>(TEntity entity)
        {
            var collection = GetCollection<TEntity>();
            collection.InsertOne(entity);
        }

        public List<TEntity> Get<TEntity>()
        {
            return GetCollection<TEntity>().Find(_ => true).ToList();
        }

        public void DropCollection()
        {
            GetDatabase().DropCollection(CollectionName);
        }

        private MongoClient GetClient()
        {
            return new MongoClient(ConnectionString);
        }

        private IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase(DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<TEntity>(CollectionName);

            return collection;
        }
    }
}
