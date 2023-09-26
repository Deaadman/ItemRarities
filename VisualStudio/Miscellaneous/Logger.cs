namespace ItemRarities
{
    public class Logger
    {
        public static void Log(string message, params object[] parameters)              => Melon<Main>.Logger.Msg($"{message}", parameters);
        public static void LogWarning(string message, params object[] parameters)       => Melon<Main>.Logger.Warning($"{message}", parameters);
        public static void LogError(string message, params object[] parameters)         => Melon<Main>.Logger.Error($"{message}", parameters);
        public static void LogSeperator(params object[] parameters)                     => Melon<Main>.Logger.Msg("==============================================================================", parameters);
        public static void LogStarter()                                                 => Melon<Main>.Logger.Msg($"Mod loaded on version v{BuildInfo.Version}");
    }
}