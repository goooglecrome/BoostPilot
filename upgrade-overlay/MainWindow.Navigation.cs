using System.Windows;
using BoostPilot.Core;

namespace BoostPilot;

public partial class MainWindow
{
    void NavigateToPage(string tag, bool persist)
    {
        if (tag is not ("wiz" or "dash" or "game" or "hw" or "opt" or "ai" or "rest"))
            tag = "wiz";

        WizPanel.Visibility = tag == "wiz" ? Visibility.Visible : Visibility.Collapsed;
        DashPanel.Visibility = tag == "dash" ? Visibility.Visible : Visibility.Collapsed;
        GamePanel.Visibility = tag == "game" ? Visibility.Visible : Visibility.Collapsed;
        HwPanel.Visibility = tag == "hw" ? Visibility.Visible : Visibility.Collapsed;
        OptPanel.Visibility = tag == "opt" ? Visibility.Visible : Visibility.Collapsed;
        AiPanel.Visibility = tag == "ai" ? Visibility.Visible : Visibility.Collapsed;
        RestPanel.Visibility = tag == "rest" ? Visibility.Visible : Visibility.Collapsed;
        HwActive(tag == "hw");

        NavWizard.IsChecked = tag == "wiz";
        NavDash.IsChecked = tag == "dash";
        NavGame.IsChecked = tag == "game";
        NavHw.IsChecked = tag == "hw";
        NavOpt.IsChecked = tag == "opt";
        NavAi.IsChecked = tag == "ai";
        NavRest.IsChecked = tag == "rest";

        (PageTitle.Text, PageSubtitle.Text) = tag switch
        {
            "wiz" => ("Setup Wizard", "Build a profile that fits the way you use your PC."),
            "dash" => ("Dashboard", "Live hardware readings and system activity."),
            "game" => ("Game Profiles", "Apply game-specific profiles and restore normal settings when the game closes."),
            "hw" => ("Hardware", "Explore your processor, graphics, memory and connected devices."),
            "opt" => ("Optimise", "Manage performance profiles, startup apps and temporary files."),
            "ai" => ("AI Assistant", "Describe your goal and review a tailored optimisation plan."),
            "rest" => ("Restore Centre", "Review and undo supported Windows changes."),
            _ => ("Setup Wizard", "Build a profile that fits the way you use your PC.")
        };

        if (persist)
        {
            var preferences = AppPreferencesStore.Load();
            preferences.LastPage = tag;
            AppPreferencesStore.Save(preferences);
        }
    }

    void RestoreLastPage()
    {
        var lastPage = AppPreferencesStore.Load().LastPage;
        NavigateToPage(string.IsNullOrWhiteSpace(lastPage) ? "wiz" : lastPage, false);
    }
}
