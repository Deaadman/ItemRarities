using System.Text.Json;

namespace ItemRarities
{
    public class Main : MelonMod
    {
        public static Dictionary<string, Rarity> gearRarities = new Dictionary<string, Rarity>(StringComparer.OrdinalIgnoreCase);

        [Obsolete]
        public override void OnApplicationStart()
        {
            string json = GetEmbeddedResource("ItemRarities.Rarities.GearRarities.json");
            var rarityData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

            if (rarityData != null)
            {
                foreach (var rarityGroup in rarityData)
                {

                    Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), rarityGroup.Key);
                    foreach (string item in rarityGroup.Value)
                    {
                        gearRarities[item] = rarity;
                    }

                }
            }
        }

        private static string GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        public static Rarity GetRarity(string itemName)
        {
            if (itemName.StartsWith("GEAR_"))
            {
                if (gearRarities.ContainsKey(itemName))
                {
                    return gearRarities[itemName];
                }
            }

            return Rarity.INVALID;
        }

        public static Color GetColorForRarity(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return ColorUtility.TryParseHtmlString("#9da1a4", out Color commonColor) ? commonColor : Color.white;
                case Rarity.Uncommon:
                    return ColorUtility.TryParseHtmlString("#4fa528", out Color uncommonColor) ? uncommonColor : Color.green;
                case Rarity.Rare:
                    return ColorUtility.TryParseHtmlString("#54b0fd", out Color rareColor) ? rareColor : Color.blue;
                case Rarity.Epic:
                    return ColorUtility.TryParseHtmlString("#9c4bc1", out Color epicColor) ? epicColor : Color.magenta;
                case Rarity.Legendary:
                    return ColorUtility.TryParseHtmlString("#fb9b34", out Color legendaryColor) ? legendaryColor : Color.yellow;
                case Rarity.Mythic:
                    return ColorUtility.TryParseHtmlString("#edc643", out Color mythicColor) ? mythicColor : Color.yellow;
                case Rarity.Story:
                    return ColorUtility.TryParseHtmlString("#47bcb3", out Color storyColor) ? storyColor : Color.cyan;
                case Rarity.INVALID: return Color.red;
                case Rarity.Default: return Color.white;
                default: return Color.white;
            }
        }
    }
}

// This code helps me understand the GEAR_ names with what Display Name for easier readability.
/* public override void OnSceneWasInitialized(int buildIndex, string sceneName)
{
    ListGear();
}

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

    foreach (string gearName in sortedUniqueGear)
    {
        // Using the GetGearDisplayName method to get the display name
        string displayName = GearItem.GetGearDisplayName("GEAR_" + gearName);

        // Constructing the log message with the desired format
        string logMessage = "Gear Name: " + gearName + " \nDisplay Name: " + displayName + "\n---\n";

        // Logging to console
        Logger.Log(logMessage);

        // Writing to file
        File.AppendAllText(outputPath, logMessage + Environment.NewLine);
    }
} */