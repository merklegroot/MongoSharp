using MongoDB.Bson.Serialization.Conventions;

namespace mongo_lib
{
    public class MongoBootstrapper
    {
        private static readonly object _locker = new object();

        private static bool _hasRun = false;

        public void Bootstrap()
        {
            if (_hasRun) { return; }

            lock (_locker)
            {
                if (_hasRun) { return; }

                try
                {
                    var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
                    ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);
                }
                finally
                {
                    _hasRun = true;
                }
            }
        }
    }
}
