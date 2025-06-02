using System;
using UtilityFramework.Services.Vimeo.Core3.Enums;

namespace UtilityFramework.Services.Vimeo.Core3
{
    /// <summary>
    /// Verify upload response
    /// </summary>
    [Serializable]
    public class VerifyUploadResponse
    {
        /// <summary>
        /// Status
        /// </summary>
        public UploadStatusEnum Status { get; set; }

        /// <summary>
        /// Bytes written
        /// </summary>
        public long BytesWritten { get; set; }
    }
}