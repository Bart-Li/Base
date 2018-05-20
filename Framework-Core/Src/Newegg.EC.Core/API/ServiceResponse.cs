using System;

namespace Newegg.EC.Core
{
    /// <summary>
    /// Service response.
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse"/> class.
        /// </summary>
        private ServiceResponse()
        {
            this.Status = true;
            this.Message = string.Empty;
            this.Exception = null;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether success, default is true.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets message.default is empty.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets service exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Set failed message，status is F.
        /// </summary>
        /// <param name="message">Message content.</param>
        /// <returns>Biz Response.</returns>
        public static ServiceResponse SuccessMessage(string message = "")
        {
            return new ServiceResponse
            {
                Message = message,
                Status = true,
                Exception = null
            };
        }

        /// <summary>
        /// Set exception message，status is false.
        /// </summary>
        /// <param name="message">Message content.</param>
        /// <param name="exception">System exception.</param>
        /// <returns>Biz Response.</returns>
        public static ServiceResponse SetFailedMessage(string message, Exception exception = null)
        {
            return new ServiceResponse
            {
                Message = message,
                Status = false,
                Exception = exception
            };
        }

        /// <summary>
        /// Check service response is success.
        /// </summary>
        /// <param name="response">Service response.</param>
        /// <returns>Check result.</returns>
        public static bool IsSuccess(ServiceResponse response)
        {
            if (response == null)
            {
                return false;
            }

            return response.Status;
        }
    }
}
