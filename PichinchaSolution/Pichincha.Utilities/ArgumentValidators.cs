namespace Pichincha.Utilities
{
    using System;

    /// <summary>
    /// Validation extensions.
    /// </summary>
    public static class ArgumentValidators
    {
        /// <summary>
        /// Throw argument null exception if value is null.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="parameterName">The parameter.</param>
        public static void ThrowIfNull([ValidatedNotNull] object value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}
