using Content.Client.UserInterface.Controls;
using Content.Shared._NF.Bank;
using Content.Shared.Atmos;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client._NF.Atmos.UI;

[GenerateTypedNameReferences]
public sealed partial class GasSaleMenu : FancyWindow
{
    public Action? RefreshRequested;
    public Action? SellRequested;

    public static readonly string[] GasStrings =
    [
        "gases-oxygen", // 0
        "gases-nitrogen", // 1
        "gases-co2", // 2
        "gases-plasma", // 3
        "gases-tritium", // 4
        "gases-water-vapor", // 5
        "gases-ammonia", // 6
        "gases-n2o", // 7
        "gases-frezon", // 8
    ];

    public string FallbackGasString = "gas-fallback";

    public GasSaleMenu()
    {
        RobustXamlLoader.Load(this);
        RefreshButton.OnPressed += OnRefreshPressed;
        SellButton.OnPressed += OnSellPressed;
    }

    public void SetMixture(GasMixture mixture, int appraisal)
    {
        Gases.Children.Clear();
        GasAmounts.Children.Clear();
        var hasGas = false;
        for (var i = 0; i < Atmospherics.TotalNumberOfGases; i++)
        {
            var gasAmount = mixture.GetMoles(i);
            if (gasAmount <= 0)
                continue;

            Label gasLabel = new();
            if (i < GasStrings.Length)
                gasLabel.Text = Loc.GetString(GasStrings[i]);
            else
                gasLabel.Text = Loc.GetString(FallbackGasString, ("number", i));
            Gases.Children.Add(gasLabel);

            Label amountLabel = new();
            amountLabel.Text = Loc.GetString("gas-sale-menu-quantity", ("value", gasAmount));
            amountLabel.HorizontalAlignment = HAlignment.Right;
            GasAmounts.Children.Add(amountLabel);
            hasGas = true;
        }

        if (!hasGas)
        {
            Label noGasLabel = new();
            noGasLabel.Text = Loc.GetString("gas-sale-menu-no-gases");
            Gases.Children.Add(noGasLabel);
        }

        AppraisalLabel.Text = BankSystemExtensions.ToSpesoString(appraisal);
    }

    public void SetEnabled(bool enabled)
    {
        SellButton.Disabled = !enabled;
    }

    private void OnSellPressed(BaseButton.ButtonEventArgs obj)
    {
        SellRequested?.Invoke();
    }

    private void OnRefreshPressed(BaseButton.ButtonEventArgs obj)
    {
        RefreshRequested?.Invoke();
    }
}
