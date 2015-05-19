namespace ZM.ExternalConfiguration
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an exception that will be thrown if the specified configuration source is not found.
    /// </summary>
    [Serializable]
    public class ConfigurationSourceNotFoundException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default constructor for the <see cref="ConfigurationSourceNotFoundException"/> class.
        /// </summary>
        public ConfigurationSourceNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConfigurationSourceNotFoundException"/> requiring a message.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        public ConfigurationSourceNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConfigurationSourceNotFoundException"/> requiring a message and inner exception objects.
        /// </summary>
        /// <param name="message">A <see cref="string"/> value representing the user defined message to attach to the exception.</param>
        /// <param name="inner">An <see cref="Exception"/> based object representing the inner exception associated with the throw.</param>
        public ConfigurationSourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConfigurationSourceNotFoundException"/> class with a provided serialization info and context.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> based object.</param>
        /// <param name="context">A <see cref="StreamingContext"/> based object.</param>
        protected ConfigurationSourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
