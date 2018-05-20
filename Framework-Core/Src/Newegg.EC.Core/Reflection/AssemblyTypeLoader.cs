using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Newegg.EC.Core.Reflection
{
    /// <summary>
    /// Default assembly type loader.
    /// </summary>
    public static class AssemblyTypeLoader
    {
        /// <summary>
        /// Get all types from assemblies.
        /// </summary>
        /// <param name="assembly">Assembly instance.</param>
        /// <returns>Type collection.</returns>
        public static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            return GetTypes(assembly, out _);
        }

        /// <summary>
        /// Get all types from assemblies.
        /// </summary>
        /// <param name="assembly">Assembly instance.</param>
        /// <param name="message">Process message.</param>
        /// <returns>Type collection.</returns>
        public static IEnumerable<Type> GetTypes(Assembly assembly, out string message)
        {
            return GetTypes(assembly, null, out message);
        }

        /// <summary>
        /// Get filted types from assemblies.
        /// </summary>
        /// <param name="assembly">Assembly instance.</param>
        /// <param name="filter">Filter function.</param>
        /// <returns>Type collection.</returns>
        public static IEnumerable<Type> GetTypes(Assembly assembly, Func<Type, bool> filter)
        {
            return GetTypes(assembly, filter, out _);
        }

        /// <summary>
        /// Get filted types from assemblies.
        /// </summary>
        /// <param name="assembly">Assembly instance.</param>
        /// <param name="filter">Filter function.</param>
        /// <param name="message">Process message.</param>
        /// <returns>Type collection.</returns>
        public static IEnumerable<Type> GetTypes(Assembly assembly, Func<Type, bool> filter, out string message)
        {
            IEnumerable<Type> result = Enumerable.Empty<Type>();
            message = string.Empty;

            try
            {
                result = filter != null ? assembly.GetTypes().Where(filter).ToArray() : assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder builder = new StringBuilder();
                if (!ex.Types.IsNullOrEmpty())
                {
                    ex.Types.Where(type => type != null).ForEach(type => builder.AppendFormat("Load type: \"{0}\" fail. ", type.FullName));
                }

                if (!ex.LoaderExceptions.IsNullOrEmpty())
                {
                    ex.LoaderExceptions.Where(x => x != null).ForEach(x => builder.AppendFormat("Load exception: \"{0}\". ", x.Message));
                }

                message = builder.ToString();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return result;
        }
    }
}
