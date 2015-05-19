namespace ZM.ExternalConfiguration
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an exception that occurs when a lock is not aquired when reading from, or writing to the cached configuration.
    /// </summary>
    [Serializable]
    public class CacheBusyException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default constructor for the <see cref="CacheBusyException"/> class.
        /// </summary>
        public CacheBusyException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CacheBusyException"/> requiring a message.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        public CacheBusyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CacheBusyException"/> requiring a message and inner exception objects.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        /// <param name="inner">An <see cref="Exception"/> based object representing the inner exception associated with the throw.</param>
        public CacheBusyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CacheBusyException"/> class with a provided serialization info and context.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> based object.</param>
        /// <param name="context">A <see cref="StreamingContext"/> based object.</param>
        protected CacheBusyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
