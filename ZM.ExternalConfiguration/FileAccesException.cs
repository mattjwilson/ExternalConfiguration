namespace ZM.ExternalConfiguration
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception class representing an error with file access or load from the remote source via <see cref="IFileLoader"/> classes.
    /// </summary>
    [Serializable]
    public class FileAccessException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default constructor for the <see cref="FileAccessException"/> class.
        /// </summary>
        public FileAccessException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FileAccessException"/> requiring a message.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        public FileAccessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FileAccessException"/> requiring a message and inner exception objects.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        /// <param name="inner">An <see cref="Exception"/> based object representing the inner exception associated with the throw.</param>
        public FileAccessException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FileAccessException"/> class with a provided serialization info and context.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> based object.</param>
        /// <param name="context">A <see cref="StreamingContext"/> based object.</param>
        protected FileAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
