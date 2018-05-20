namespace Newegg.EC.Zookeeper.Client
{
    public class DataWatchContext
    {
        public IZookeeperClient Client { get; set; }
        public string Path { get; set; }

        public string GetData()
        {
            return Client.GetData(Path);
        }
    }
}
