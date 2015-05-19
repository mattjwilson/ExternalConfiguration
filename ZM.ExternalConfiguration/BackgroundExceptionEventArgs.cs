namespace ZM.ExternalConfiguration
{
    using System;

    /// <summary>
    /// Class representing the args passed through an event when an unhandled exception is observed on the background cache process.
    /// </summary>
    public class BackgroundExceptionEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to an object representing the exception that was realized from a background process.
        /// </summary>
        /// <value>An <see cref="Exception"/> based object.</value>
        public Exception CaughtException
        {
            get;
            protected set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="BackgroundExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="ex">An <see cref="Exception"/> based object representing the exception that was observed on the background process.</param>
        public BackgroundExceptionEventArgs(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            this.CaughtException = ex;
        }

        #endregion
    }
}
