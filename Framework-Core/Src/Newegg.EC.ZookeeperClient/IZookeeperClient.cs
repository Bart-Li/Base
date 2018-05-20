using System;
using System.Collections.Generic;
using Newegg.EC.Zookeeper.Client.Core;
using Org.Apache.Zookeeper.Data;

namespace Newegg.EC.Zookeeper.Client
{
    public interface IZookeeperClient : IDisposable
    {
        /// <summary>
        /// Get data by path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetData(string path);

        /// <summary>
        /// Get children data by path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<string> GetChildren(string path);

        /// <summary>
        /// Check node is exists.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="watcher">Watcher.</param>
        /// <returns></returns>
        bool Exists(string path, IWatcher watcher = null);

        /// <summary>
        /// Set node data.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Stat SetDate(string path, string data);

        /// <summary>
        /// Watch data change.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        void Watch(string path, Action<DataWatchContext> action);
    }
}
