using System.Text; // Used for commented out code, which is why it's grey atm.
using System.Text.Json;

namespace ItemRarities
{
    public class Main : MelonMod
    {
        public static readonly Dictionary<string, Rarity> gearRarities = new(StringComparer.OrdinalIgnoreCase);

        public override void OnInitializeMelon()
        {
            string json = GetEmbeddedResource("ItemRarities.Rarities.TLDVanillaGearRarities.json");
            var rarityData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

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
        }

        /// <summary>
        /// Fetches the content of an embedded resource from the currently executing assembly.
        /// </summary>
        /// <param name="resourceName">The name of the embedded resource to fetch.</param>
        /// <exception cref="InvalidOperationException">Thrown when the specified embedded resource is not found.</exception>
        private static string GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is not null)
            {
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }

            throw new InvalidOperationException($"Failed to get embedded resource '{resourceName}'.");
        }

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
            return rarity switch
            {
                Rarity.Common => GetColor("#9da1a4", Color.white),
                Rarity.Uncommon => GetColor("#4fa528", Color.green),
                Rarity.Rare => GetColor("#54b0fd", Color.blue),
                Rarity.Epic => GetColor("#9c4bc1", Color.magenta),
                Rarity.Legendary => GetColor("#fb9b34", Color.yellow),
                Rarity.Mythic => GetColor("#edc643", Color.yellow),
                Rarity.Story => GetColor("#47bcb3", Color.cyan),
                _ => HandleUnrecognizedRarity(),
            };
        }

        private static Color HandleUnrecognizedRarity()
        {
            Logger.LogError("Unrecognized rarity type encountered.");
            return Color.white;
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