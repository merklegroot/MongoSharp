using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace mongo_lib
{
    public interface IMongoCollectionContext : IMongoDatabaseContext
    {
        string CollectionName { get; set; }

        void Insert<TEntity>(TEntity entity);

        [Obsolete]
        List<TEntity> Get<TEntity>();

        List<TEntity> GetAll<TEntity>();

        TEntity GetLast<TEntity>();

        IMongoCollection<TEntity> GetCollection<TEntity>();

        void DropCollection();
    }
}
