using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Newegg.EC.Core.Reflection
{
    /// <summary>
    /// Assembly loader.
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// Initializes the <see cref="T:Newegg.EC.Reflection.AssemblyLoader"/> class.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Assembly> DicAssemblies = new ConcurrentDictionary<string, Assembly>();

        /// <summary>
        /// Initializes the <see cref="T:Newegg.EC.Reflection.AssemblyLoader"/> class.
        /// </summary>
        static AssemblyLoader()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.GetAssemblies().ForEach(TryAdd);
            Initialize(new DirectoryInfo(currentDomain.BaseDirectory));
        }

        /// <summary>
        /// Load the specified assemblyName.
        /// </summary>
        /// <returns>The load.</returns>
        /// <param name="assemblyName">Assembly name.</param>
        public static Assembly Load(AssemblyName assemblyName)
        {
            if (DicAssemblies.TryGetValue(assemblyName.Name, out Assembly result))
            {
                return result;
            }

            return default(Assembly);
        }

        /// <summary>
        /// Loads all.
        /// </summary>
        /// <returns>The all.</returns>
        public static IEnumerable<Assembly> LoadAll()
        {
            return DicAssemblies.Values.ToArray();
        }

        /// <summary>
        /// Initialize the specified directoryInfo.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="directoryInfo">Directory info.</param>
        private static void Initialize(DirectoryInfo directoryInfo)
        {
            var assembliesDll = directoryInfo.GetFiles("*.dll");
            if (assembliesDll.Any())
            {
                assembliesDll.ForEach(Initialize);
            }
        }

        /// <summary>
        /// Initialize the specified fileInfo.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="fileInfo">File info.</param>
        private static void Initialize(FileInfo fileInfo)
        {
            try
            {
                TryAdd(Assembly.LoadFrom(fileInfo.FullName));
            }
            catch (Exception e)
            {
                //todo log.debug(e)
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Tries the add.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        private static void TryAdd(Assembly assembly)
        {
            DicAssemblies.TryAdd(assembly.FullName, assembly);
        }
    }
}
