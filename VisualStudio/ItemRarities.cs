using System.Text; // Used for commented out code, which is why it's grey atm.
using System.Text.Json;

namespace ItemRarities
{
    public class Main : MelonMod
    {
        public static Dictionary<string, Dictionary<string, string>> LocalizationData { get; private set; } = [];
        public static Dictionary<string, Rarity> gearRarities { get; } = new(StringComparer.InvariantCultureIgnoreCase);
        internal static string? VanillaRaritiesData { get; set; }
        internal static UILabel? rarityLabel { get; set; }
        internal static UILabel? RarityLabel
        {
            get { return rarityLabel; }
            set { rarityLabel = value; }
        }

        internal static HashSet<string> excludedNames { get; } =
        [
            "PACKSETTINGS_Pilgrim",
            "NAVIGATION",
            "CAMPCRAFT",
            "FIRST AID",
            "DRINK",
            "LIGHT SOURCES",
            "FOOD",
            "WEAPONS",
            "DROP DECOY",
            "OPEN MAP",
            "ROCK CACHE",
            "STATUS",
            "FIRE",
            "PASS TIME",
            "ICE FISHING HOLE",
            "SNOW SHELTER"
        ];

        #region Colourblind Dictionary Hex Codes
        private static readonly Dictionary<ColorblindMode, Dictionary<Rarity, string>> colorMappings = new()
        {
            {
                ColorblindMode.None, new()
                {
                    {Rarity.Common, "#9ea3a9"},
                    {Rarity.Uncommon, "#53ab01"},
                    {Rarity.Rare, "#0097de"},
                    {Rarity.Epic, "#b147e5"},
                    {Rarity.Legendary, "#e58437"},
                    {Rarity.Mythic, "#d1b450"},

                    {Rarity.Story, "#49dcc0"},
                }
            },
            {
                ColorblindMode.Deuteranope, new()
                {
                    {Rarity.Common, "#9fa4aa"},
                    {Rarity.Uncommon, "#214700"},
                    {Rarity.Rare, "#024bba"},
                    {Rarity.Epic, "#e9b5fd"},
                    {Rarity.Legendary, "#ffb14d"},
                    {Rarity.Mythic, "#d1b051"},

                    {Rarity.Story, "#1d7990"},
                }
            },
            {
                ColorblindMode.Protanope, new()
                {
                    {Rarity.Common, "#9fa4aa"},
                    {Rarity.Uncommon, "#3f8403"},
                    {Rarity.Rare, "#0065ae"},
                    {Rarity.Epic, "#c972fd"},
                    {Rarity.Legendary, "#fca44b"},
                    {Rarity.Mythic, "#e2bf61"},

                    {Rarity.Story, "#21a68b"},
                }
            },
            {
                ColorblindMode.Tritanope, new()
                {
                    {Rarity.Common, "#a2a7ad"},
                    {Rarity.Uncommon, "#1c7302"},
                    {Rarity.Rare, "#27c2ff"},
                    {Rarity.Epic, "#da73ff"},
                    {Rarity.Legendary, "#c56916"},
                    {Rarity.Mythic, "#ad902c"},

                    {Rarity.Story, "#4fe1cc"},
                }
            }
        };
        #endregion

        public override void OnInitializeMelon()
        {
            GetEmbeddedResource("ItemRarities.Data.VanillaRarities.json");
            var rarityData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(VanillaRaritiesData);

            if (rarityData == null)
            {
                Logger.LogError("Failed to deserialize JSON data; rarityData is null.");
                return;
            }

            foreach (var rarityGroup in rarityData)
            {
                if (Enum.TryParse<Rarity>(rarityGroup.Key, out var rarity))
                {
                    foreach (var item in rarityGroup.Value)
                    {
                        gearRarities[item] = rarity;
                    }
                }
                else
                {
                    Logger.LogError($"Failed to parse rarity group key '{rarityGroup.Key}' as a Rarity enum.");
                }
            }

            LoadLocalizations();
            Settings.OnLoad();
        }

        /// <summary>
        /// Fetches the content of an embedded resource from the currently executing assembly.
        /// </summary>
        /// <param name="resourceName">The name of the embedded resource to fetch.</param>
        /// <exception cref="InvalidOperationException">Thrown when the specified embedded resource is not found.</exception>
        public static void GetEmbeddedResource(string resourceName)
        {
            _ = Assembly.GetExecutingAssembly();

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)!;
            using StreamReader reader = new(stream);
            VanillaRaritiesData = reader.ReadToEnd();

            stream.Dispose();
            reader.Dispose();
        }

        #region Rarity Methods
        public static Rarity GetRarity(string itemName)
        {
            if (itemName.StartsWith("GEAR_"))
            {
                if (gearRarities.TryGetValue(itemName, out var rarity))
                {
                    SetRarityLabelVisibility(true);
                    return rarity;
                }
            }

            SetRarityLabelVisibility(false);
            return Rarity.None;
        }

        public static void SetRarityLabelVisibility(bool isVisible)
        {
            ItemDescriptionPage_RarityLabelPatch.RarityLabel?.gameObject.SetActive(isVisible);
            PanelInventoryExamine_RarityLabelPatch.RarityLabel?.gameObject.SetActive(isVisible);
            PanelClothing_RarityLabelPatch.ClothingRarityLabel?.gameObject.SetActive(isVisible);
            PanelCrafting_RarityLabelPatch.RarityLabel?.gameObject.SetActive(isVisible);
            PanelCooking_RarityLabelPatch.RarityLabel?.gameObject.SetActive(isVisible);
            PanelMilling_RarityLabelPatch.RarityLabel?.gameObject.SetActive(isVisible);
            PlayerManager_RarityLabelPatch.RarityLabel?.gameObject?.SetActive(isVisible);
        }

        /// <summary>
        /// Gets the color associated with a specific item rarity.
        /// </summary>
        /// <param name="rarity">The rarity of the item.</param>
        public static Color GetRarityColor(Rarity rarity)
        {
            Color originalColor = GetOriginalColor(rarity);
            Color colorblindColor = GetColorblindAdjustedColor(rarity);

            float t = Settings.Instance.ColorblindnessStrength / 10f;
            return Color.Lerp(originalColor, colorblindColor, t);
        }
        #endregion

        #region Colour Methods
        /// <summary>
        /// Retrieves the original color associated with a specific item rarity.
        /// </summary>
        /// <param name="rarity">The rarity of the item.</param>
        private static Color GetOriginalColor(Rarity rarity)
        {
            if (colorMappings.TryGetValue(ColorblindMode.None, out var rarityMappings) &&
                rarityMappings.TryGetValue(rarity, out var hexColor))
            {
                return GetColor(hexColor, Color.white);
            }
            else
            {
                return Color.white;
            }
        }

        /// <summary>
        /// Retrieves the color associated with a specific item rarity, adjusted for the current colorblind setting.
        /// </summary>
        /// <param name="rarity">The rarity of the item.</param>
        private static Color GetColorblindAdjustedColor(Rarity rarity)
        {
            var currentMode = Settings.Instance.ColorblindSetting;

            if (!colorMappings[currentMode].TryGetValue(rarity, out var hexColor) || hexColor == null)
            {
                hexColor = "#FFFFFF";
            }
            return GetColor(hexColor, Color.white);
        }

        /// <summary>
        /// Attempts to parse a color from an HTML color string.
        /// </summary>
        /// <param name="htmlColor">The HTML color string to parse.</param>
        /// <param name="defaultColor">The default color to return if parsing fails.</param>
        /// <returns>The parsed color, or the default color if parsing fails.</returns>
        private static Color GetColor(string htmlColor, Color defaultColor)
        {
            if (ColorUtility.TryParseHtmlString(htmlColor, out var color))
            {
                return color;
            }
            else
            {
                Logger.LogWarning($"Failed to parse color string '{htmlColor}'. Returning default color.");
                return defaultColor;
            }
        }
        #endregion

        #region Localization Methods
        /// <summary>
        /// Loads the localization data from the embedded resource into the application.
        /// </summary>
        public static void LoadLocalizations()
        {
            string JSONfile = "ItemRarities.Data.LocalizationData.json";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(JSONfile);
            if (stream == null)
            {
                Logger.LogError($"Failed to load resource '{JSONfile}'.");
                return;
            }

            using StreamReader reader = new(stream);
            string results = reader.ReadToEnd();

            if (string.IsNullOrEmpty(results))
            {
                Logger.LogError("Loaded JSON content is empty.");
                return;
            }

            LocalizationManager.LoadJsonLocalization(results);

            try
            {
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(results);
                if (jsonData != null)
                {
                    LocalizationData = jsonData;
                }
            }
            catch (JsonException ex)
            {
                Logger.LogError($"Failed to parse localization JSON. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the localized string for a given rarity based on the selected language.
        /// </summary>
        /// <param name="key">The key for the item rarity.</param>
        /// <param name="language">The desired language for the localization. Defaults to "English".</param>
        public static string? GetTranslation(string key, string language = "English")
        {
            if (LocalizationData.TryGetValue(key, out var languageData))
            {
                if (languageData.TryGetValue(language, out var localizedString) && !string.IsNullOrEmpty(localizedString))
                {
                    return localizedString;
                }
                if (language != "English" && languageData.TryGetValue("English", out localizedString) && !string.IsNullOrEmpty(localizedString))
                {
                    return localizedString;
                }
            }
            return null;
        }
        #endregion
    }
}

// This code helps me understand the GEAR_ names with what Display Name for easier readability.
/* public override void OnSceneWasInitialized(int buildIndex, string sceneName)
{
    ListGear();
}

/// <summary>
/// Lists the gear names and their respective display names, logging them to the console and writing them to a file.
/// </summary>
private static void ListGear()
{
    SortedSet<string> sortedUniqueGear = new SortedSet<string>();
    foreach (string gearName in ConsoleManager.m_SearchStringToGearNames.Values)
    {
        if (!gearName.StartsWith("GEAR_")) continue;
        sortedUniqueGear.Add(gearName.Substring("GEAR_".Length));
    }

    // Specifying the path to the output file -- it outputs to the My Documents folder.
    string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GearItems.txt");

    StringBuilder logMessages = new StringBuilder();
    foreach (string gearName in sortedUniqueGear)
    {
        // Using the GetGearDisplayName method to get the display name
        string displayName = GearItem.GetGearDisplayName("GEAR_" + gearName);

        // Constructing the log message with the desired format
        string logMessage = $"Gear Name: {gearName} \\nDisplay Name: {displayName} \\n---\\n";

        // Logging to console
        Logger.Log(logMessage);

        // Adding log message to StringBuilder
        logMessages.AppendLine(logMessage);
    }

    // Writing all log messages to file at once
    try
    {
        File.WriteAllText(outputPath, logMessages.ToString());
    }
    catch (Exception ex)
    {
        Logger.LogError($"An error occurred while writing to the file: {ex.Message}");
    }
} */