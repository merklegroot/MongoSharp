namespace mongo_lib
{
    public interface IMongoRepo<TEntity, TId>
    {
        void Insert(MongoCollectionContext context, TEntity entity);        
    }
}
