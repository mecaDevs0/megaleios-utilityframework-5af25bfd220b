using System;
using JetBrains.Annotations;

namespace UtilityFramework.Services.Vimeo.Core3.Exceptions
{
    /// <inheritdoc />
    public class VimeoApiException : Exception
    {
        /// <inheritdoc />
        [PublicAPI]
        public VimeoApiException()
        {
        }

        /// <inheritdoc />
        public VimeoApiException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public VimeoApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}