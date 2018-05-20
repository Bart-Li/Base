using System.Threading;
using Newegg.EC.Zookeeper.Client.Core;

namespace Newegg.EC.Zookeeper.Client.Impl
{
    internal sealed class ConnectionWatcher : IWatcher
    {
        private readonly ManualResetEvent _connectionEvent;

        public ConnectionWatcher(ManualResetEvent connectionEvent)
        {
            _connectionEvent = connectionEvent;
        }

        public void Process(WatchedEvent @event)
        {
            if (KeeperState.SyncConnected == @event.State)
            {
                _connectionEvent.Set();
            }
        }
    }
}
