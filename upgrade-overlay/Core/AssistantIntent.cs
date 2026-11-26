namespace BoostPilot.Core;

public sealed record AssistantIntent(Goal Goal, string Label, bool IncludeStartup)
{
    /// <summary>Maps common natural-language PC goals to the existing, reviewed optimisation profiles.</summary>
    public static AssistantIntent FromRequest(string request)
    {
        var text = request.ToLowerInvariant();
        bool startup = text.Contains("startup") || text.Contains("start-up") ||
                      text.Contains("background app") || text.Contains("background program");

        if (Has(text, "battery", "power saving", "save power", "quieter", "quiet", "cooler", "less heat", "lower temperature", "low heat"))
            return new(Goal.PowerSaving, "Cooler / lower-power PC", startup);
        if (Has(text, "stream", "streaming", "obs", "recording"))
            return new(Goal.Streaming, "Streaming", startup);
        if (Has(text, "editing", "edit video", "render", "content creation", "creator"))
            return new(Goal.ContentCreation, "Content creation", startup);
        if (Has(text, "competitive", "esports", "e-sports", "low latency", "input lag", "tournament"))
            return new(Goal.CompetitiveGaming, "Competitive gaming", startup);
        if (Has(text, "gaming", "game", "fps", "frames", "performance", "faster"))
            return new(Goal.Gaming, "Gaming performance", startup);
        return new(Goal.EverydayUse, "Balanced everyday use", startup);
    }

    static bool Has(string text, params string[] words) => words.Any(text.Contains);
}
