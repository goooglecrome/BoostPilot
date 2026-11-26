using System.ComponentModel;
using System.Windows;
using BoostPilot.Core;

namespace BoostPilot;

public partial class MainWindow
{
    void AppWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var preferences = AppPreferencesStore.Load();
        Width = Math.Clamp(preferences.WindowWidth, MinWidth, 1800);
        Height = Math.Clamp(preferences.WindowHeight, MinHeight, 1200);

        // Reuse the last window position only when it still fits on the current desktop.
        var work = SystemParameters.WorkArea;
        if (preferences.WindowLeft >= work.Left && preferences.WindowTop >= work.Top &&
            preferences.WindowLeft + 160 <= work.Right && preferences.WindowTop + 100 <= work.Bottom)
        {
            Left = preferences.WindowLeft;
            Top = preferences.WindowTop;
            WindowStartupLocation = WindowStartupLocation.Manual;
        }
        else
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        if (preferences.WindowWasMaximised)
            WindowState = WindowState.Maximized;
    }

    void AppWindow_Closing(object? sender, CancelEventArgs e)
    {
        var preferences = AppPreferencesStore.Load();
        double width, height, left, top;
        if (WindowState == WindowState.Normal)
        {
            width = Width; height = Height; left = Left; top = Top;
        }
        else
        {
            var bounds = RestoreBounds;
            width = bounds.Width; height = bounds.Height; left = bounds.Left; top = bounds.Top;
        }
        preferences.WindowWidth = Math.Max(MinWidth, width);
        preferences.WindowHeight = Math.Max(MinHeight, height);
        preferences.WindowLeft = left;
        preferences.WindowTop = top;
        preferences.WindowWasMaximised = WindowState == WindowState.Maximized;
        AppPreferencesStore.Save(preferences);
    }
}
