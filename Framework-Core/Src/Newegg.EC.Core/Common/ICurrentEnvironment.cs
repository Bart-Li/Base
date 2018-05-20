namespace Newegg.EC.Core
{
    /// <summary>
    /// Current enviroment interface.
    /// </summary>
    public interface ICurrentEnvironment
    {
        /// <summary>
        /// Gets current machine name.
        /// </summary>
        string MachineName { get; }

        /// <summary>
        /// Gets current machine channel.
        /// </summary>
        string Channel { get; }

        /// <summary>
        /// Gets current machine environment.
        /// </summary>
        string EnvironmentName { get; }

        /// <summary>
        /// Gets current machine location.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Get environment variable.
        /// </summary>
        /// <param name="variable">Environment variable.</param>
        /// <returns>Environment value.</returns>
        string GetEnvironmentVariable(string variable);
    }
}
