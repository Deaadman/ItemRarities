namespace ItemRarities
{
    internal class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        #region Accessibility
        [Section("Accessibility")]

        [Name("Color Blind Mode")]
        [Description("Choose between the colorblind modes of Deuteranope, Protanope, Tritanope to suit your needs.")]
        public ColorblindMode ColorblindSetting = ColorblindMode.None;

        [Name("Color Blind Strength")]
        [Description("Adjust the intensity of the colorblind mode on a scale from 0 (weakest) to 10 (strongest).")]
        [Slider(0, 10)]
        public int ColorblindnessStrength = 0;
        #endregion

        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}