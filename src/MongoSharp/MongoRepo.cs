using MongoDB.Driver;
using System;

namespace mongo_lib
{
    public class MongoRepo<TEntity, TId> : IMongoRepo<TEntity, TId>
    {
        public MongoRepo() { new MongoBootstrapper().Bootstrap(); }

        public void Insert(MongoCollectionContext context, TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            PerformCollectionAction(context, collection => collection.InsertOne(entity));
        }

        public void PerformCollectionAction(
            MongoCollectionContext context,
            Action<IMongoCollection<TEntity>> operation)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            var func = new Func<IMongoCollection<TEntity>, int>(collection =>
            {
                operation(collection);
                return 1;
            });

            PerformCollectionFunc(context, func);
        }

        public TResult PerformCollectionFunc<TResult>(
            MongoCollectionContext context,
            Func<IMongoCollection<TEntity>, TResult> operation)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);
            var collection = database.GetCollection<TEntity>(context.CollectionName);

            return operation(collection);
        }
    }
}
