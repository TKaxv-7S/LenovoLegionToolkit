﻿using LenovoLegionToolkit.Lib.Extensions;
using LenovoLegionToolkit.Lib.Utils;
using LenovoLegionToolkit.WPF.Resources;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace LenovoLegionToolkit.WPF.Pages;

public partial class AboutPage
{
    private static string VersionText
    {
        get
        {
            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            if (version is null)
                return string.Empty;
            if (version.IsBeta())
                return "BETA";
            return version.ToString();
        }
    }

    private static string BuildText => Assembly.GetEntryAssembly()?.GetBuildDateTimeString() ?? string.Empty;

    private static string CopyrightText
    {
        get
        {
            var location = Assembly.GetEntryAssembly()?.Location;
            if (location is null)
                return string.Empty;
            return FileVersionInfo.GetVersionInfo(location).LegalCopyright ?? string.Empty;
        }
    }

    public AboutPage()
    {
        InitializeComponent();

        _version.Text += $" {VersionText}";
        _build.Text += $" {BuildText}";
        _copyright.Text = CopyrightText;

        _translationCredit.Visibility = Resource.Culture.Equals(new CultureInfo("en")) ? Visibility.Collapsed : Visibility.Visible;
    }

    private void OpenApplicationDataFolder_Click(object sender, RoutedEventArgs e)
    {
        if (!Directory.Exists(Folders.AppData))
            return;

        Process.Start("explorer", Folders.AppData);
    }

    private void OpenApplicationTempFolder_Click(object sender, RoutedEventArgs e)
    {
        if (!Directory.Exists(Folders.Temp))
            return;

        Process.Start("explorer", Folders.Temp);
    }
    private void OpenLogFolder_Click(object sender, RoutedEventArgs e)
    {
        string logPath = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\LenovoLegionToolkit\\log");

        if (Directory.Exists(logPath))
        {
            Process.Start("explorer.exe", logPath);
        }
    }
}
