namespace ItemRarities
{
    internal class Utilities
    {
        // use this to ensure that names are properly named
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name)
        {
            if (name == null) return null;
            else return name.Replace("(Clone)", "").Trim();
        }
    }
}