namespace mongo_lib
{
    public class MongoConnectionString
    {
        private const int DefaultPort = 27017;
        private const string DefaultHost = "localhost";

        private int _port = DefaultPort;
        public int Port() { return _port; }
        public MongoConnectionString Port(int value) { _port = value; return this; }

        private string _host = DefaultHost;
        public string Host() { return _host; }
        public MongoConnectionString Host(string value) { _host = value; return this; }

        public MongoConnectionString() { }

        public override string ToString()
        {
            return $"mongodb://localhost:{DefaultPort}";
        }

        public static implicit operator string(MongoConnectionString item)
        {
            return item != null ? item.ToString() : null;
        }
    }
}
