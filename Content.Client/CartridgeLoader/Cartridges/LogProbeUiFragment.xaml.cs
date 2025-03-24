﻿using Content.Shared.CartridgeLoader.Cartridges;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.CartridgeLoader.Cartridges;

[GenerateTypedNameReferences]
public sealed partial class LogProbeUiFragment : BoxContainer
{
    /// <summary>
    /// Action invoked when the print button gets pressed.
    /// </summary>
    public Action? OnPrintPressed;

    public LogProbeUiFragment()
    {
        RobustXamlLoader.Load(this);

        PrintButton.OnPressed += _ => OnPrintPressed?.Invoke();
    }

    public void UpdateState(string name, List<PulledAccessLog> logs)
    {
        EntityName.Text = name;
        PrintButton.Disabled = string.IsNullOrEmpty(name);

        ProbedDeviceContainer.RemoveAllChildren();

        var count =  1;
        foreach (var log in logs)
        {
            AddAccessLog(log, count);
            count++;
        }
    }

    private void AddAccessLog(PulledAccessLog log, int numberLabelText)
    {
        var timeLabelText = TimeSpan.FromSeconds(Math.Truncate(log.Time.TotalSeconds)).ToString();
        var accessorLabelText = log.Accessor;
        var entry = new LogProbeUiEntry(numberLabelText, timeLabelText, accessorLabelText);

        ProbedDeviceContainer.AddChild(entry);
    }
}
