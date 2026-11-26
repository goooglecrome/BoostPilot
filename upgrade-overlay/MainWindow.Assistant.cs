using System.Windows;
using BoostPilot.Core;

namespace BoostPilot;

public partial class MainWindow
{
    void Assistant_Click(object sender, RoutedEventArgs e)
    {
        var assistant = new AssistantWindow(_r) { Owner = this };
        assistant.ShowDialog();
        RefreshRestore();
        try { ModeText.Text = $"Active power plan: {PowerPlanManager.Active().Name}"; }
        catch { /* Hardware or Windows power-plan details may not be available on every PC. */ }
    }
}
