﻿using SKitLs.Bots.Telegram.Core.Exceptions;
using SKitLs.Bots.Telegram.Stateful.Settings;

namespace SKitLs.Bots.Telegram.Stateful.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an error is related to <c>*.Stateful</c> framework extension.
    /// The exception is derived from the <see cref="SKTgSignedException"/> class, which provides a base for
    /// signed exceptions in the application.
    /// </summary>
    public class StatefulException : SKTgSignedException
    {
        /// <summary>
        /// Gets the extension prefix used for localization keys.
        /// </summary>
        public static string ExtensionPrefix => SKStateSetting.ExtensionPrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatefulException"/> class with the specified parameters.
        /// </summary>
        /// <param name="localKey">A string representing the local key for the exception message.</param>
        /// <param name="originType">The origin type of the exception.</param>
        /// <param name="sender">The object that caused the exception.</param>
        /// <param name="format">Optional. Array of format strings used for the exception message.</param>
        public StatefulException(string localKey, SKTEOriginType originType, object sender, params string?[] format)
            : base($"{ExtensionPrefix}.{localKey}", originType, sender, format) { }
    }
}