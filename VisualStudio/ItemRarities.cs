using System.Text.Json;

namespace ItemRarities
{
    public class Main : MelonMod
    {
        public static Dictionary<string, Rarity> gearRarities = new Dictionary<string, Rarity>(StringComparer.OrdinalIgnoreCase);

        [Obsolete]
        public override void OnApplicationStart()
        {
            string json = File.ReadAllText("D:/The Long Dark/Mods/ItemRarities/VisualStudio/Rarities/GearRarities.json");
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
                case Rarity.INVALIDRARITY: return Color.red;
                case Rarity.Default: return Color.white;
                default: return Color.white;
            }
        }
    }
}