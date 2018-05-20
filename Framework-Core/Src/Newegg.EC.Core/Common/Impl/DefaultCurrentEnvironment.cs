using System;

namespace Newegg.EC.Core.Common.Impl
{
    /// <summary>
    /// Default current environment.
    /// </summary>
    [AutoSetupService(typeof(ICurrentEnvironment))]
    public class DefaultCurrentEnvironment : ICurrentEnvironment
    {
        /// <summary>
        /// Gets current machine name.
        /// </summary>
        public string MachineName => Environment.MachineName;

        /// <summary>
        /// Gets current machine channel.
        /// </summary>
        public string Channel
        {
            get
            {
                var value = GetEnvironmentVariable("Channel");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                return "TestChannel";
            }
        }

        /// <summary>
        /// Gets current machine environment(GDEV,GQC,PRE,PRD).
        /// </summary>
        public string EnvironmentName
        {
            get
            {
                var value = GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets current machine location(WH7,E4,E11).
        /// </summary>
        public string Location
        {
            get
            {
                var value = GetEnvironmentVariable("Location");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                return EnvironmentName;
            }
        }

        /// <summary>
        /// Get environment variable.
        /// </summary>
        /// <param name="variable">Environment variable.</param>
        /// <returns>Environment value.</returns>
        public string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}
