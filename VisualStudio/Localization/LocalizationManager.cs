using System.Text.Json;

namespace ItemRarities
{
    /// <summary>
    /// The LocalizationManager class manages the localization data, 
    /// including loading the data from a resource file and providing 
    /// translations for specific keys and languages.
    /// </summary>
    public class LocalizationManager
    {
        private static LocalizationManager instance = new();
        private Dictionary<string, Dictionary<string, string>>? translations;

        private LocalizationManager()
        {
            LoadLocalizationData(Localization.s_Language);
        }

        #pragma warning disable IDE0074
        public static LocalizationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalizationManager();
                }
                return instance;
            }
        }
        #pragma warning restore IDE0074

        #pragma warning disable IDE0060
        /// <summary>
        /// Loads the localization data from a JSON resource file.
        /// Logs an error if the resource cannot be found or loaded.
        /// </summary>
        /// <param name="language">The language for which to load the localization data.</param>
        public void LoadLocalizationData(string language)
        {
            string resourceName = "ItemRarities.Localization.LocalizationData.json";

            string jsonContent = Main.GetEmbeddedResource(resourceName);
            if (jsonContent == null)
            {
                Logger.LogError("Failed to load localization data from embedded resource.");
                return;
            }

            translations = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
        }
        #pragma warning restore IDE0060

        /// <summary>
        /// Retrieves the translation for a given key and language.
        /// If a translation for the specified language is not found, 
        /// it falls back to the English translation, 
        /// and if that is also not found, returns the key itself.
        /// </summary>
        /// <param name="key">The key for the translation.</param>
        /// <param name="language">The language for the translation.</param>
        /// <returns>The translated string, or the key if no translation is found.</returns>
        public string GetTranslation(string key, string language)
        {
            if (translations != null && translations.ContainsKey(key) && translations[key].ContainsKey(language))
            {
                return translations[key][language];
            }
            else
            {
                return key;
            }
        }
    }
}