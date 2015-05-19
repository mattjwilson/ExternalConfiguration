namespace ZM.ExternalConfiguration
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an exception that will be thrown if a requested key is not found in the cached configuration.
    /// </summary>
    [Serializable]
    public class KeyNotFoundException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default constructor for the <see cref="KeyNotFoundException"/> class.
        /// </summary>
        public KeyNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="KeyNotFoundException"/> requiring a message.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        public KeyNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="KeyNotFoundException"/> requiring a message and inner exception objects.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        /// <param name="inner">An <see cref="Exception"/> based object representing the inner exception associated with the throw.</param>
        public KeyNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="KeyNotFoundException"/> class with a provided serialization info and context.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> based object.</param>
        /// <param name="context">A <see cref="StreamingContext"/> based object.</param>
        protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
