using System;
using Newegg.EC.Zookeeper.Client.Core;

namespace Newegg.EC.Zookeeper.Client.Impl
{
    public class DataChangeWatcher : IWatcher
    {
        private Action<DataWatchContext> _action;
        public DataWatchContext Context { get; private set; }

        public DataChangeWatcher(IZookeeperClient keeper, string path, Action<DataWatchContext> action)
        {
            Context = new DataWatchContext()
            {
                Client = keeper,
                Path = path
            };

            _action = action;
        }

        public void Process(WatchedEvent wevent)
        {
            if (wevent.State != KeeperState.SyncConnected)
            {
                return;
            }

            if (wevent.Type == EventType.NodeDataChanged)
            {
                //重新watch
                Context.Client.Exists(Context.Path, this);

                _action(Context);
            }
        }
    }
}
