namespace SKitLs.Bots.Telegram.Stateful.Settings
{
    /// <summary>
    /// The <see cref="SKStateSetting"/> class provides settings related to the SKitLs.Bots.Telegram.Stateful extension.
    /// </summary>
    public class SKStateSetting
    {
        /// <summary>
        /// Gets or sets the prefix used for the extension's localizations.
        /// Default value is "state".
        /// </summary>
        public static string ExtensionPrefix { get; set; } = "state";
    }
}