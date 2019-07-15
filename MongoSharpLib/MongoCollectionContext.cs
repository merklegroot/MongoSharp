using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace mongo_lib
{
    public class MongoCollectionContext : MongoDatabaseContext, IMongoCollectionContext
    {
        public string CollectionName { get; set; }

        public MongoCollectionContext() { }

        public MongoCollectionContext(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }

        public MongoCollectionContext(IMongoDatabaseContext databaseContext, string collectionName)
        {
            ConnectionString = databaseContext.ConnectionString;
            DatabaseName = databaseContext.DatabaseName;
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

        public List<TEntity> GetAll<TEntity>()
        {
            return GetCollection<TEntity>().Find(_ => true).ToList();
        }

        public void DropCollection()
        {
            GetDatabase().DropCollection(CollectionName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<TEntity>(CollectionName);

            return collection;
        }

        public TEntity GetLast<TEntity>()
        {
            var collection = GetCollection<BsonDocument>();

            var value = collection.Find(_ => true)
                .SortByDescending(item => item["_id"])
                .FirstOrDefault();

            return value != null
                ? BsonSerializer.Deserialize<TEntity>(value)
                : default(TEntity);
        }
    }
}
