using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Org.Apache.Zookeeper.Data;
using Newegg.EC.Zookeeper.Client.Core;

namespace Newegg.EC.Zookeeper.Client.Impl
{
    public class ZookeeperClient : IZookeeperClient
    {
        private readonly string _connectionString;
        private readonly int _sessionTimeout;
        private ZooKeeper _zookeeper;
        private ManualResetEvent _connectionEvent;
        private readonly List<Func<Stat>> _watcherActions = new List<Func<Stat>>();

        /// <summary>
        /// Create Zookeeper client instance.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="sessionTimeout">Session timeout.</param>
        internal ZookeeperClient(string connectionString, int sessionTimeout)
        {
            this._connectionString = connectionString;
            this._sessionTimeout = sessionTimeout;
            this.ReConnect();
        }

        /// <summary>
        /// Get data by path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetData(string path)
        {
            var data = string.Empty;
            try
            {
                path = this.GetZooKeeperPath(path);
                var func = new Func<string>(() =>
                {
                    var bytes = this._zookeeper.GetData(path, null, null);
                    return Encoding.UTF8.GetString(bytes);
                });
                data = ExcuteFunc(func);
            }
            catch (KeeperException.NoNodeException)
            {
                return data;
            }

            return data;
        }

        /// <summary>
        /// Get children data by path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<string> GetChildren(string path)
        {
            path = this.GetZooKeeperPath(path);
            var func = new Func<IEnumerable<string>>(() => this._zookeeper.GetChildren(path, null));
            return ExcuteFunc(func);
        }

        /// <summary>
        /// Check node is exists.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="watcher"></param>
        /// <returns></returns>
        public bool Exists(string path, IWatcher watcher = null)
        {
            path = this.GetZooKeeperPath(path);
            var func = new Func<Stat>(() => this._zookeeper.Exists(path, watcher));
            return ExcuteFunc(func) != null;
        }

        /// <summary>
        /// Set node data.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stat SetDate(string path, string data)
        {
            path = this.GetZooKeeperPath(path);
            var func = new Func<Stat>(() =>
            {
                //修改节点上存储的数据，需要提供version，version设为-1表示强制修改
                return this._zookeeper.SetData(path, Encoding.UTF8.GetBytes(data), -1);
            });

            return ExcuteFunc(func);
        }

        /// <summary>
        /// Watch data change.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        public void Watch(string path, Action<DataWatchContext> action)
        {
            path = this.GetZooKeeperPath(path);
            var watcher = new DataChangeWatcher(this, path, action);
            var func = new Func<Stat>(() => this._zookeeper.Exists(path, watcher));
            ExcuteFunc(func);
            _watcherActions.Add(func);
        }

        public void Dispose()
        {
            this._zookeeper.Dispose();
        }

        private void ReConnect()
        {
            this._connectionEvent = new ManualResetEvent(false);
            this._zookeeper = new ZooKeeper(_connectionString, new TimeSpan(0, 0, 0, _sessionTimeout), new ConnectionWatcher(_connectionEvent));
            //等待异步连接完成， 在连接未完成前Return会导致GetData抛出ConnectionLossException
           this. _connectionEvent.WaitOne();
        }

        private T ExcuteFunc<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (KeeperException.SessionExpiredException)
            {
                this._zookeeper.Dispose();
                this.ReConnect();
                Thread.Sleep(100);
                ReWatch();
                return func();
            }
        }

        private void ReWatch()
        {
            foreach (var watcherAction in this._watcherActions)
            {
                ExcuteFunc(watcherAction);
            }
        }

        private string GetZooKeeperPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return "/";
            }

            return $"/{path.Trim('/')}";
        }
    }
}
