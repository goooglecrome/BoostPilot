using System.Text.Json;

namespace BoostPilot.Core;

/// <summary>Per-user BoostPilot preferences. Kept separate from Windows settings and restore history.</summary>
public sealed class AppPreferences
{
    public double WindowWidth { get; set; } = 1180;
    public double WindowHeight { get; set; } = 780;
    public double WindowLeft { get; set; } = -1;
    public double WindowTop { get; set; } = -1;
    public bool WindowWasMaximised { get; set; }
    public string LastAssistantRequest { get; set; } = "";
    public string LastAssistantGoal { get; set; } = "";
    public List<string> LastAssistantSelections { get; set; } = new();
}

public static class AppPreferencesStore
{
    static readonly string DirectoryPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BoostPilot");
    static readonly string FilePath = Path.Combine(DirectoryPath, "preferences.json");
    static readonly object Gate = new();
    static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static AppPreferences Load()
    {
        lock (Gate)
        {
            try
            {
                if (!File.Exists(FilePath)) return new AppPreferences();
                return JsonSerializer.Deserialize<AppPreferences>(File.ReadAllText(FilePath)) ?? new AppPreferences();
            }
            catch
            {
                // A damaged preferences file must never stop the app from starting.
                return new AppPreferences();
            }
        }
    }

    public static void Save(AppPreferences preferences)
    {
        lock (Gate)
        {
            Directory.CreateDirectory(DirectoryPath);
            var temp = FilePath + ".tmp";
            File.WriteAllText(temp, JsonSerializer.Serialize(preferences, JsonOptions));
            File.Move(temp, FilePath, true);
        }
    }

    public static void SaveAssistant(string request, string goal, IEnumerable<string> selections)
    {
        var preferences = Load();
        preferences.LastAssistantRequest = request;
        preferences.LastAssistantGoal = goal;
        preferences.LastAssistantSelections = selections.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        Save(preferences);
    }
}
