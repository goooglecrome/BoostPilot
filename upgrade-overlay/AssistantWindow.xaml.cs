using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BoostPilot.Core;

namespace BoostPilot;

public partial class AssistantWindow : Window
{
    readonly RestoreService _restore;
    List<PlannedChange> _plan = new();
    Dictionary<string, CheckBox> _checks = new(StringComparer.OrdinalIgnoreCase);
    string _goalName = "";

    public AssistantWindow(RestoreService restore)
    {
        InitializeComponent();
        _restore = restore;
        var preferences = AppPreferencesStore.Load();
        if (!string.IsNullOrWhiteSpace(preferences.LastAssistantRequest))
            RequestBox.Text = preferences.LastAssistantRequest;
        Loaded += (_, _) => RequestBox.Focus();
        Closing += (_, _) => SaveAssistantPreferences();
    }

    static string Key(PlannedChange change) => $"{change.Kind}|{change.Target}|{change.NewValue}";

    void SaveAssistantPreferences()
    {
        var selected = _plan.Where(c => _checks.TryGetValue(Key(c), out var box) && box.IsChecked == true)
            .Select(Key).ToList();
        AppPreferencesStore.SaveAssistant(RequestBox.Text, _goalName, selected);
    }

    void GamingExample_Click(object sender, RoutedEventArgs e) =>
        RequestBox.Text = "I want more FPS for gaming, with Windows Game Mode enabled.";

    void CoolExample_Click(object sender, RoutedEventArgs e) =>
        RequestBox.Text = "I want my PC to run quieter, cooler and use less power.";

    void Close_Click(object sender, RoutedEventArgs e) => Close();

    async void Analyse_Click(object sender, RoutedEventArgs e)
    {
        var request = RequestBox.Text.Trim();
        if (request.Length < 4)
        {
            MessageBox.Show(this, "Tell BoostPilot what you want your PC to do first.", "Add an instruction", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        AnalyseButton.IsEnabled = ApplyButton.IsEnabled = false;
        StatusText.Text = "Scanning hardware and checking the requested settings…";
        PlanPanel.Children.Clear();
        _checks.Clear();
        _plan.Clear();
        PlanSummary.Text = "Building your plan…";

        try
        {
            var result = await Task.Run(() =>
            {
                var hardware = HardwareScanner.Scan();
                var intent = AssistantIntent.FromRequest(request);
                var plan = WizardPlanner.Build(intent.Goal, hardware);
                var activePower = "Unavailable";
                try { activePower = PowerPlanManager.Active().Name; } catch { }
                List<string> processes;
                try { processes = Metrics.TopProcesses(5); } catch { processes = new List<string>(); }
                return (Hardware: hardware, Intent: intent, Plan: plan, ActivePower: activePower, Processes: processes);
            });

            _goalName = result.Intent.Label;
            _plan = result.Plan.Changes
                .Where(c => c.Kind is "powerplan" or "regdword" || (result.Intent.IncludeStartup && c.Kind == "startup"))
                .ToList();

            var preferenceSnapshot = AppPreferencesStore.Load();
            var remembered = preferenceSnapshot.LastAssistantSelections.ToHashSet(StringComparer.OrdinalIgnoreCase);
            if (remembered.Count > 0)
                foreach (var change in _plan) change.Selected = remembered.Contains(Key(change));

            FindingsText.Text =
                $"CPU: {result.Hardware.Cpu}\n" +
                $"GPU: {result.Hardware.Gpu}\n" +
                $"Memory: {result.Hardware.RamGb:F0} GB · {result.Hardware.Threads} logical processors\n" +
                $"Device: {(result.Hardware.IsLaptop ? (result.Hardware.OnBattery ? "Laptop · battery" : "Laptop · plugged in") : "Desktop")}\n" +
                $"System drive: {result.Hardware.DiskFreeGb:F0} GB free of {result.Hardware.DiskTotalGb:F0} GB\n" +
                $"Current power plan: {result.ActivePower}" +
                (result.Processes.Count > 0 ? "\nTop memory processes: " + string.Join(", ", result.Processes) : "");

            foreach (var advice in result.Plan.Advice)
                AddNote("PC insight", advice);

            if (_plan.Count == 0)
            {
                PlanSummary.Text = "Your current PC settings already match the recommended profile, or there are no safe changes to suggest.";
                PlanPanel.Children.Add(new TextBlock
                {
                    Text = "No changes are needed. You can adjust your request and scan again.",
                    Foreground = new SolidColorBrush(Color.FromRgb(0x9A, 0xAD, 0xC2)),
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 4, 0, 0)
                });
            }
            else
            {
                PlanSummary.Text = $"Profile: {result.Intent.Label} · {_plan.Count} suggested change(s). Review the list and untick anything you don't want.";
                foreach (var change in _plan) AddChange(change);
            }

            AppPreferencesStore.SaveAssistant(request, _goalName, _plan.Where(c => c.Selected).Select(Key));
            ApplyButton.IsEnabled = _plan.Any(c => c.Selected);
            StatusText.Text = "Scan complete · No changes have been applied yet.";
        }
        catch (Exception ex)
        {
            PlanSummary.Text = "The scan did not finish.";
            StatusText.Text = "Scan error: " + ex.Message;
            MessageBox.Show(this, "BoostPilot couldn't finish the scan:\n\n" + ex.Message, "Assistant scan", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally
        {
            AnalyseButton.IsEnabled = true;
        }
    }

    void AddNote(string title, string note)
    {
        var text = new TextBlock
        {
            Text = title.ToUpperInvariant() + "  ·  " + note,
            Foreground = new SolidColorBrush(Color.FromRgb(0x9A, 0xAD, 0xC2)),
            FontSize = 11,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 9)
        };
        PlanPanel.Children.Add(text);
    }

    void AddChange(PlannedChange change)
    {
        var card = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0x0A, 0x14, 0x21)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x2A, 0x40, 0x57)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(11),
            Margin = new Thickness(0, 0, 0, 9)
        };
        var body = new StackPanel();
        var checkbox = new CheckBox
        {
            Content = change.Title,
            IsChecked = change.Selected,
            Foreground = Brushes.White,
            FontWeight = FontWeights.SemiBold,
            FontSize = 12,
            Tag = change,
            Margin = new Thickness(0, 0, 0, 5)
        };
        checkbox.Checked += (_, _) => { change.Selected = true; UpdateApplyButton(); SaveAssistantPreferences(); };
        checkbox.Unchecked += (_, _) => { change.Selected = false; UpdateApplyButton(); SaveAssistantPreferences(); };
        body.Children.Add(checkbox);
        body.Children.Add(new TextBlock
        {
            Text = change.Explain,
            Foreground = new SolidColorBrush(Color.FromRgb(0x9A, 0xAD, 0xC2)),
            TextWrapping = TextWrapping.Wrap,
            FontSize = 11,
            Margin = new Thickness(22, 0, 0, 0)
        });
        card.Child = body;
        PlanPanel.Children.Add(card);
        _checks[Key(change)] = checkbox;
    }

    void UpdateApplyButton() => ApplyButton.IsEnabled = _plan.Any(c => _checks.TryGetValue(Key(c), out var box) && box.IsChecked == true);

    async void Apply_Click(object sender, RoutedEventArgs e)
    {
        var selected = _plan.Where(c => _checks.TryGetValue(Key(c), out var box) && box.IsChecked == true).ToList();
        if (selected.Count == 0)
        {
            MessageBox.Show(this, "Select at least one recommended change.", "Nothing selected", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var summary = string.Join("\n", selected.Select(c => "• " + c.Title));
        var answer = MessageBox.Show(this,
            "BoostPilot is about to apply these settings:\n\n" + summary +
            "\n\nA restore record will be saved for supported reversible changes. Continue?",
            "Review changes before applying", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (answer != MessageBoxResult.Yes) return;

        ApplyButton.IsEnabled = AnalyseButton.IsEnabled = false;
        var results = new List<string>();
        foreach (var change in selected)
        {
            try { results.Add("Applied: " + ChangeApplier.Apply(change, _restore)); }
            catch (Exception ex) { results.Add("Skipped: " + change.Title + " — " + ex.Message); }
        }

        SaveAssistantPreferences();
        PlanPanel.Children.Clear();
        foreach (var line in results)
            PlanPanel.Children.Add(new TextBlock { Text = line, Foreground = Brushes.White, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 9) });
        PlanSummary.Text = "Application results";
        StatusText.Text = "Finished · Your Windows changes remain in place after BoostPilot closes.";
        ApplyButton.IsEnabled = false;
        AnalyseButton.IsEnabled = true;
        MessageBox.Show(this, string.Join("\n", results), "BoostPilot update complete", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
